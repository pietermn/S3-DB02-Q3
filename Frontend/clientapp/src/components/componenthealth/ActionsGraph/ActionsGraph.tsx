import * as d3 from "d3";
import { useEffect, useState } from "react";
import { GetPredictedActions, GetPreviousActions } from "../../../api/requests/components";
import { ProductionDate } from "../../../globalTypes";
import "./ActionsGraph.scss";
import ActionsGraphSkeleton from "./ActionsGraphSkeleton";
import { useD3 } from "./useD3Hook";

interface IActionsGraph {
    componentId: number;
}

export default function ActionsGraph(props: IActionsGraph) {
    const [actions, setActions] = useState<ProductionDate[]>([]);
    let maxValue = Math.max(...actions.map((w) => w.productions), 0);
    const [myWidth] = useState((window.innerWidth / 3) * 2 * 0.75);
    const [myHeight] = useState((window.innerHeight / 2) * 0.6);
    const [isLoading, setIsLoading] = useState(false);
    const [loadingPredictive, setloadingPredictive] = useState(false);
    const [beginDate, setBeginDate] = useState(dateInput(new Date("2021-05-01")));
    const [endDate, setEndDate] = useState(dateInput(new Date("2021-06-01")));

    function dateInput(date: Date) {
        const d = new Date(date);
        const dayZero = d.getDate() < 10 ? true : false;
        const monthZero = d.getMonth() + 1 < 10 ? true : false;

        return (
            d.getFullYear() +
            "-" +
            (monthZero ? "0" : "") +
            (d.getMonth() + 1) +
            "-" +
            (dayZero ? "0" : "") +
            d.getDate()
        );
    }

    // set the dimensions and margins of the graph
    let ref: any;

    var tooltip = d3.select(".tooltip-area").style("opacity", 0);

    function mouseOver() {
        tooltip.style("opacity", 1);
    }

    function mouseLeave() {
        tooltip.style("opacity", 0);
    }

    function mouseMove(event: MouseEvent, d: ProductionDate) {
        const text = d3.select(".tooltip-area__text").attr("z-index", 100);
        text.text(`Actions: ${d.productions}`);
        const [x, y] = d3.pointer(event);

        tooltip.attr("transform", `translate(${x}, ${y - 10})`);
    }

    ref = useD3(
        (svg: d3.Selection<SVGElement, {}, HTMLElement, any>) => {
            const margin = { top: 20, right: 30, bottom: 30, left: 40 };

            const x = d3
                .scaleBand()
                .range([60, myWidth])
                .domain(
                    actions
                        .slice(0)
                        .reverse()
                        .map((d) => `${d.currentTimespan}`)
                )

                .padding(0.2);
            const y = d3
                .scaleLinear()
                .domain(maxValue ? [0, maxValue] : [0, 1, 0])
                .range([myHeight, 0]);

            const xAxis = (g: any) => {
                g.attr("transform", `translate(0,${myHeight})`)
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .attr("transform", "translate(-10,0)rotate(-45)")
                    .style("text-anchor", "end");
            };

            const yAxis = (g: any) => {
                g.attr("transform", `translate(${margin.left + 20},-10)`).call(d3.axisLeft(y));
            };

            svg.select(".x-axis-label")
                .attr("transform", "translate(" + (myWidth - 10) + " ," + (myHeight + margin.top + 20) + ")")
                .style("text-anchor", "middle");

            svg.select(".y-axis-label")
                .attr("transform", "rotate(-90)")
                .attr("y", -5)
                .attr("x", -50)
                .attr("dy", "1em")
                .style("text-anchor", "middle");

            svg.select(".x-axis").transition().duration(800).call(xAxis);
            svg.select(".y-axis").transition().duration(800).call(yAxis);

            svg.select(".plot-area")
                .selectAll(".bar")
                .data(actions)
                .join("rect")
                .attr("class", "bar")
                .on("mousemove", mouseMove)
                .on("mouseleave", mouseLeave)
                .on("mouseover", mouseOver)
                .transition()
                .duration(800)
                .attr("fill", (d) => (d.isPredicted ? "#fff" : "#69b3a2"))
                .attr("stroke", (d) => (d.isPredicted ? "#69b3a2" : ""))
                .attr("stroke-dasharray", (d) => (d.isPredicted ? 6 : 0))
                .attr("x", (d, i) => x(`${d.currentTimespan}`) || 0)
                .attr("width", x.bandwidth())
                .attr("y", (d) => y(d.productions))
                .attr("height", (d) => y(0) - y(d.productions));
        },
        [actions.length, JSON.stringify(actions)]
    );

    useEffect(() => {
        async function AsyncGetActions() {
            setIsLoading(true);
            let newActions = (await GetPreviousActions(props.componentId, beginDate, endDate)).reverse();
            setActions(newActions);
            setIsLoading(false);
            setloadingPredictive(true);
            let newPredictedActions = (await GetPredictedActions(props.componentId, beginDate, endDate)).reverse();
            setloadingPredictive(false);
            setActions(newPredictedActions.concat(newActions));
        }

        AsyncGetActions();
    }, [props.componentId, beginDate, endDate]);

    return (
        <div className="actionsgraph-container">
            <div>
                <input
                    min="2010-01-01"
                    max={dateInput(new Date())}
                    type="date"
                    value={beginDate}
                    onChange={(e) => setBeginDate(e.target.value)}
                />
                <input type="date" value={endDate} onChange={(e) => setEndDate(e.target.value)} />
                <svg className="legend" width="3rem" height="1rem">
                    <rect fill="#69b3a2" height="1rem" width="3rem" />
                </svg>
                <>Actual actions</>
                <svg className="legend" width="3rem" height="1rem">
                    <rect fill="#fff" strokeDasharray={6} stroke="#69b3a2" height="1rem" width="3rem" />
                </svg>
                <>Predicted actions</>
                {loadingPredictive && (
                    <>
                        <CircularProgress className="Loading-Icon" size="1rem" />
                        <>Loading predictive maintenance</>
                    </>
                )}
            </div>
            {isLoading && <ActionsGraphSkeleton isLoading={isLoading} />}
            <div id="Actions-Graph" className={isLoading ? "invisible" : ""}>
                <svg ref={ref}>
                    <text className="x-axis-label">
                        Time ({actions.length ? actions[0].timespanIndicator + "s" : ""}) {"->"}
                    </text>
                    <text className="y-axis-label">Actions {"->"}</text>
                    <g className="plot-area" />
                    <g className="x-axis"></g>
                    <g className="y-axis"></g>
                    <g className="tooltip-area">
                        <text className="tooltip-area__text"></text>
                    </g>
                </svg>
            </div>
        </div>
    );
}
