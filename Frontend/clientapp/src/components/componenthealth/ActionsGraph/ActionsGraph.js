import CircularProgress from "@material-ui/core/CircularProgress";
import * as d3 from "d3";
import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { GetPreviousActions } from "../../../api/requests/components";
import "./ActionsGraph.scss";
import { useD3 } from "./useD3Hook";

export default function ActionsGraph(props) {
    const [actions, setActions] = useState([]);
    let maxValue = Math.max(...actions.map((w) => w), 0);
    const [myWidth] = useState((window.innerWidth / 3) * 2 * 0.75);
    const [myHeight] = useState((window.innerHeight / 2) * 0.6);
    const [isLoading, setIsLoading] = useState(false);
    const [timespan, setTimespan] = useState("weeks");
    const [amountTimespan, setAmountTimespan] = useState("4");

    // set the dimensions and margins of the graph
    let ref;

    var tooltip = d3.select(".tooltip-area").style("opacity", 0)

    function mouseOver() {
        tooltip.style("opacity", 1);
    }

    function mouseLeave() {
        tooltip.style("opacity", 0);
    }

    function mouseMove(event, d) {
        const text = d3.select('.tooltip-area__text').attr("z-index", 100)
        text.text(`Productions: ${d}`);
        const [x, y] = d3.pointer(event);

        tooltip
            .attr('transform', `translate(${x}, ${y - 10})`);
    }

    ref = useD3((svg) => {
        const margin = { top: 20, right: 30, bottom: 30, left: 40 };

        const x = d3
            .scaleBand()
            .range([40, myWidth])
            .domain(
                actions
                    .slice(0)
                    .reverse()
                    .map((d, i) =>
                        timespan == "months" ? t("month.label") + " " + (i + 1) : t("week.label") + " " + (i + 1)
                    )
            )

            .padding(0.2);
        const y = d3
            .scaleLinear()
            .domain(maxValue ? [0, maxValue] : [0, 1, 0])
            .range([myHeight, 0]);


        const xAxis = (g) => {
            g.attr("transform", `translate(0,${myHeight})`)
                .call(d3.axisBottom(x))
                .selectAll("text")
                .attr("transform", "translate(-10,0)rotate(-45)")
                .style("text-anchor", "end")

        }

        const yAxis = (g) => {
            g
                .attr("transform", `translate(${margin.left},0)`)
                .call(d3.axisLeft(y))
        }

        svg.select(".x-axis").transition().duration(800).call(xAxis);
        svg.select(".y-axis").transition().duration(800).call(yAxis)

        svg.select(".plot-area").selectAll(".bar")
            .data(actions)
            .join("rect")
            .attr("class", "bar")
            .on("mousemove", mouseMove)
            .on("mouseleave", mouseLeave)
            .on("mouseover", mouseOver)
            .transition()
            .duration(800)
            .attr("fill", "#69b3a2")
            .attr(
                "x",
                (d, i) =>
                    x(timespan == "months" ? t("month.label") + " " + (i + 1) : t("week.label") + " " + (i + 1)) || 0
            )
            .attr("width", x.bandwidth())
            .attr("y", (d) => y(d))
            .attr("height", (d) => y(0) - y(d))
    }, [actions.length, JSON.stringify(actions)]);

    const { t } = useTranslation();

    useEffect(() => {
        async function AsyncGetActions() {
            setIsLoading(true);
            setActions(await GetPreviousActions(props.component_id, timespan, amountTimespan));
            setIsLoading(false);
        }
        AsyncGetActions();
    }, [props.component_id]);

    async function handleInput(e) {
        setIsLoading(true);
        switch (e.target.name) {
            case "timespan":
                setTimespan(e.target.value);
                setActions(await GetPreviousActions(props.component_id, e.target.value, amountTimespan));
                break;
            case "timespan-amount":
                setAmountTimespan(e.target.value);
                setActions(await GetPreviousActions(props.component_id, timespan, e.target.value));
                break;
        }
        setIsLoading(false);
    }

    return (
        <div className="actionsgraph-container">
            <div id="Actions-Graph">
                <div>
                    <select id="timespan-select" name="timespan" value={timespan} onChange={handleInput}>
                        <option value="weeks">{t("weeks.label")}</option>
                        <option value="months">{t("months.label")}</option>
                    </select>
                    <select value={amountTimespan} name="timespan-amount" id="timespan-select" onChange={handleInput}>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                    </select>
                </div>
                <svg ref={ref}>
                    <g className="plot-area" />
                    <g className="x-axis"></g>
                    <g className="y-axis"></g>
                    <g className="tooltip-area">
                        <text className="tooltip-area__text"></text>
                    </g>
                </svg>
            </div>
            <CircularProgress className={isLoading ? "loading" : "loading invisible"} />
        </div>
    );
}
