import { CircularProgress } from "@mui/material";
import { FaArrowUp as ArrowUpIcon, FaArrowRight as ArrowRightIcon } from "react-icons/fa";
import * as d3 from "d3";
import { useEffect, useState } from "react";
import i18n from "i18next";
import { useTranslation } from "react-i18next";
import { GetPredictedActions, GetPreviousActions } from "../../../api/requests/components";
import { ProductionDate } from "../../../globalTypes";
import "./ActionsGraph.scss";
import ActionsGraphSkeleton from "./ActionsGraphSkeleton";
import { useD3 } from "./useD3Hook";
import axios, { CancelTokenSource } from "axios";

interface IActionsGraph {
    componentId: number;
}

export default function ActionsGraph(props: IActionsGraph) {
    const [actions, setActions] = useState<ProductionDate[]>([]);
    let maxValue = calculateMaxValue();
    const [myWidth] = useState((window.innerWidth / 3) * 2 * 0.75);
    const [myHeight] = useState((window.innerHeight / 2) * 0.6);
    const [isLoading, setIsLoading] = useState(false);
    const [loadingPredictive, setloadingPredictive] = useState(false);
    const [beginDate, setBeginDate] = useState(dateInput(new Date("2021-05-01")));
    const [endDate, setEndDate] = useState(dateInput(new Date("2021-06-01")));
    const [xKey, setXKey] = useState("first");
    const { t } = useTranslation();
    const [cancelToken, setCancelToken] = useState<CancelTokenSource>(axios.CancelToken.source());

    function calculateMaxValue() {
        let productions: number[] = [];
        let foundFirstPredicted = false;

        for (let i = 0; i < actions.length; i++) {
            if (actions[i].isPredicted && i != 0 && !foundFirstPredicted) {
                foundFirstPredicted = true;
                if (actions[i].currentTimespan === actions[i - 1].currentTimespan) {
                    productions.push(actions[i].productions + actions[i - 1].productions);
                }
            } else {
                productions.push(actions[i].productions);
            }
        }
        return Math.max(...productions.map((w) => w), 0);
    }

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

    function getTimespan(currentTimespan: string, timespanIndicator: string) {
        if (timespanIndicator === "Month") {
            return t(currentTimespan.toLowerCase() + ".label");
        }
        return currentTimespan;
    }

    function calculateOverlap(d: ProductionDate) {
        if (d.isPredicted) {
            let overlap = [...actions].find((a) => !a.isPredicted && a.currentTimespan == d.currentTimespan);
            if (overlap) {
                return overlap.productions;
            }
        }
        return 0;
    }

    ref = useD3(
        (svg: d3.Selection<SVGElement, {}, HTMLElement, any>) => {
            const margin = { top: 20, right: 30, bottom: 30, left: 40 };

            const x = d3
                .scaleBand()
                .range([60, myWidth])
                .domain(actions.slice(0).map((d) => `${getTimespan(d.currentTimespan, d.timespanIndicator)}`))
                .padding(0.2);

            const y = d3
                .scaleLinear()
                .domain(maxValue ? [0, maxValue] : [0, 1, 0])
                .range([myHeight, 0]);

            const xAxis = (g: any) => {
                g.attr("transform", `translate(0,${myHeight + 5})`)
                    .call(d3.axisBottom(x))
                    .selectAll("text");
            };

            const yAxis = (g: any) => {
                g.attr("transform", `translate(${margin.left + 20}, 5)`).call(d3.axisLeft(y));
            };

            svg.select(".x-axis-label")
                .attr("display", "absolute")
                .attr("transform", "translate(" + (myWidth - 13) + " ," + (myHeight + margin.top + 20) + ")")
                .style("text-anchor", "end");

            svg.select(".y-axis-label")
                .attr("display", "absolute")
                .attr("transform", "translate(0, 20)rotate(-90)")
                .style("text-anchor", "end");

            svg.select(".arrow-right")
                .attr("display", "absolute")
                .attr("transform", "translate(" + (myWidth - 10) + " ," + (myHeight + margin.top + 8) + ")")
                .attr("fontSize", "1rem");

            svg.select(".arrow-up")
                .attr("display", "absolute")
                .attr("transform", "translate(-12, 0)")
                .attr("fontSize", "1rem");

            svg.select(".x-axis")
                .transition()
                .duration(xKey === "first" ? 800 : 0)
                .call(xAxis);
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
                .attr("stroke", "#69b3a2")
                .attr("stroke-dasharray", (d) => (d.isPredicted ? 6 : 0))
                .attr("x", (d, i) => x(`${getTimespan(d.currentTimespan, d.timespanIndicator)}`) || 0)
                .attr("width", x.bandwidth())
                .attr("y", (d) => y(d.productions + calculateOverlap(d)))
                .attr("height", (d) => y(0) - y(d.productions))
                .style("transform", "translateY(5px)");
            console.log("drawing graph");
        },
        [actions.length, JSON.stringify(actions), xKey]
    );

    useEffect(() => {
        async function CancelToken(cancel: CancelTokenSource) {
            if (typeof cancelToken != typeof undefined && cancelToken) {
                cancelToken.cancel("Operation canceled due to new request.");
            }
            //Save the cancel token for the current request
            setCancelToken(cancel);
        }

        async function AsyncGetActions() {
            let cancelToken = axios.CancelToken.source();
            setIsLoading(true);
            await CancelToken(cancelToken);
            let newActions = await GetPreviousActions(props.componentId, beginDate, endDate, cancelToken.token);
            setActions(newActions);
            setIsLoading(false);
            setloadingPredictive(true);
            let newPredictedActions = await GetPredictedActions(
                props.componentId,
                beginDate,
                endDate,
                cancelToken.token
            );
            setloadingPredictive(false);
            setActions(newActions.concat(newPredictedActions));
        }
        setIsLoading(false);
        AsyncGetActions();
        // eslint-disable-next-line
    }, [props.componentId, beginDate, endDate]);

    useEffect(() => {
        setXKey(i18n.language);
    }, [t]);

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
                <>Previous actions</>
                <svg className="legend" width="3rem" height="1rem">
                    <rect fill="#fff" strokeDasharray={6} stroke="#69b3a2" height="1rem" width="3rem" />
                </svg>
                <>Predicted actions</>
                {loadingPredictive && (
                    <>
                        <CircularProgress className="Loading-Icon" size="1rem" />
                        <>Loading predicted actions</>
                    </>
                )}
            </div>
            {isLoading && <ActionsGraphSkeleton isLoading={isLoading} />}
            <div id="Actions-Graph" className={isLoading ? "invisible" : ""}>
                <svg ref={ref}>
                    <text className="x-axis-label">
                        {t("time.label")} (
                        {actions.length ? t(actions[0].timespanIndicator.toLowerCase() + "s.label") : ""})
                    </text>
                    <text className="y-axis-label">{t("actions.label")}</text>
                    <g className="arrow-up">
                        <ArrowUpIcon />
                    </g>
                    <g className="arrow-right">
                        <ArrowRightIcon />
                    </g>
                    <g className="plot-area" />
                    <g className="x-axis" key={xKey}></g>
                    <g className="y-axis"></g>
                    <g className="tooltip-area">
                        <text className="tooltip-area__text"></text>
                    </g>
                </svg>
            </div>
        </div>
    );
}
