import CircularProgress from "@material-ui/core/CircularProgress";
import * as d3 from "d3";
import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { GetPreviousActions } from "../../../api/requests/components";
import "./ActionsGraph.scss";

interface IActionsGraph {
  component_id: number;
}

export default function ActionsGraph(props: IActionsGraph) {
  const [actions, setActions] = useState<number[]>([]);
  let maxValue = Math.max(...actions.map((w) => w), 0);
  const [myWidth] = useState((window.innerWidth / 3) * 2 * 0.75);
  const [myHeight] = useState((window.innerHeight / 2) * 0.6);
  const [isLoading, setIsLoading] = useState(false);
  const [timespan, setTimespan] = useState("weeks");
  const [amountTimespan, setAmountTimespan] = useState("4");

  // set the dimensions and margins of the graph
  function DrawGraph() {
    const margin = { top: 10, right: 30, bottom: 90, left: 40 };
    d3.select("#Actions-Graph").select("svg").remove();

    // append the svg object to the body of the page
    const svg = d3
      .select("#Actions-Graph")
      .append("svg")
      .append("g")
      .attr("transform", `translate(${margin.left},${margin.top})`);

    // Parse the Data

    // X axis
    const x = d3
      .scaleBand()
      .range([0, myWidth])
      .domain(
        actions
          .slice(0)
          .reverse()
          .map((d, i) => (timespan == "months" ? t("month.label") + " " + (i + 1) : t("week.label") + " " + (i + 1)))
      )

      .padding(0.2);

    svg
      .append("g")
      .attr("transform", `translate(0,${myHeight})`)
      .call(d3.axisBottom(x))
      .selectAll("text")
      .attr("transform", "translate(-10,0)rotate(-45)")
      .style("text-anchor", "end");

    // Add Y axis
    const y = d3
      .scaleLinear()
      .domain(maxValue ? [0, maxValue] : [0, 1, 0])
      .range([myHeight, 0]);
    svg.append("g").call(d3.axisLeft(y));

    // Bars
    svg
      .selectAll("mybar")
      .data(actions)
      .join("rect")
      .attr(
        "x",
        (d, i) => x(timespan == "months" ? t("month.label") + " " + (i + 1) : t("week.label") + " " + (i + 1)) || 0
      )
      .attr("width", x.bandwidth())
      .attr("fill", "#69b3a2")
      // no bar at the beginning thus:
      .attr("height", (d) => myHeight - y(0)) // always equal to 0
      .attr("y", (d) => y(0));

    // Animation
    svg
      .selectAll("rect")
      .data(actions)
      .transition()
      .duration(800)
      .attr("y", (d) => y(d) || 0)
      .attr("height", (d) => myHeight - y(d))
      .delay((d, i) => {
        return i * 100;
      });
  }

  const { t } = useTranslation();

  useEffect(() => {
    async function AsyncGetActions() {
      setIsLoading(true);
      setActions(await GetPreviousActions(props.component_id, timespan, amountTimespan));
      setIsLoading(false);
    }
    AsyncGetActions();
  }, [props.component_id]);

  useEffect(() => {
    if (actions.length) {
      DrawGraph();
    }
  }, [JSON.stringify(actions), timespan]);

  async function handleInput(e: React.ChangeEvent<HTMLSelectElement>) {
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
      <div id="Actions-Graph" className={isLoading ? "invisible" : ""}>
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
      <CircularProgress className={isLoading ? "loading" : "loading invisible"} />
    </div>
  );
}
