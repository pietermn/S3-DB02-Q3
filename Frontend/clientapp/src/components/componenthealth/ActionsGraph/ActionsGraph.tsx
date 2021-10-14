import * as d3 from "d3";
import { useEffect, useState } from "react";
import "./ActionsGraph.scss"

interface IActionsGraph {
    actions: number[]
}

export default function ActionsGraph(props: IActionsGraph) {
    type Week = {
        week: string,
        actions: number
    }

    const weeks: Week[] = []

    for (var i: number = 0; i < props.actions.length; i++) {
        weeks.push(
            {
                week: `Week ${i + 1}`,
                actions: props.actions[i]
            })
    }

    let maxValue = Math.max(...weeks.map((w) => (w as Week).actions), 0);
    const [myWidth, setMyWidth] = useState(((window.innerWidth / 3) * 2) * .75);
    const [myHeight, setMyHeight] = useState(((window.innerHeight / 2)) * .60);

    useEffect(() => {
        DrawGraph();
    }, [])

    // set the dimensions and margins of the graph
    function DrawGraph() {
        const margin = { top: 10, right: 30, bottom: 90, left: 40 }

        // append the svg object to the body of the page
        const svg = d3.select("#Actions-Graph")
            .append("svg")
            .append("g")
            .attr("transform", `translate(${margin.left},${margin.top})`);

        // Parse the Data

        // X axis
        const x = d3.scaleBand()
            .range([0, myWidth])
            .domain(weeks.map(d => d.week))
            .padding(0.2);
        svg.append("g")
            .attr("transform", `translate(0,${myHeight})`)
            .call(d3.axisBottom(x))
            .selectAll("text")
            .attr("transform", "translate(-10,0)rotate(-45)")
            .style("text-anchor", "end");

        // Add Y axis
        const y = d3.scaleLinear()
            .domain(maxValue ? [0, maxValue] : [0, 1, 0])
            .range([myHeight, 0]);
        svg.append("g")
            .call(d3.axisLeft(y));


        // Bars
        svg.selectAll("mybar")
            .data(weeks)
            .join("rect")
            .attr("x", d => x(d.week) || 0)
            .attr("width", x.bandwidth())
            .attr("fill", "#69b3a2")
            // no bar at the beginning thus:
            .attr("height", d => myHeight - y(0)) // always equal to 0
            .attr("y", d => y(0))

        // Animation
        svg.selectAll("rect")
            .data(weeks)
            .transition()
            .duration(800)
            .attr("y", d => y(d.actions) || 0)
            .attr("height", d => myHeight - y(d.actions))
            .delay((d, i) => { return i * 100 })
    }

    return (
        <div id="Actions-Graph">

        </div>
    )
}