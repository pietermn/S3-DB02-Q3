import * as d3 from "d3";
import { useEffect } from "react";
import { Uptime } from "../../../globalTypes";
import "./MachineStatus.scss";

interface IMachineStatus {
    uptime: Uptime[];
    name: string;
}

export default function MachineStatus(props: IMachineStatus) {
    useEffect(() => {
        drawUptime();
    }, [props.uptime]);

    function calcDifference(begin: Date, end: Date) {
        let b = new Date(begin).getTime() / 1000;
        let e = new Date(end).getTime() / 1000;
        return e - b;
    }

    function drawUptime() {
        if (props.uptime && props.uptime.length) {
            const svgDocument = document.querySelector(`#${props.name}`)?.clientWidth;
            const svgWidth = svgDocument ? svgDocument : 0;
            const xDiff =
                new Date(props.uptime[props.uptime.length - 1].end).getTime() / 1000 -
                new Date(props.uptime[0].begin).getTime() / 1000;
            const scale = svgWidth / xDiff;

            d3.select(`#${props.name}`).select("svg").remove();

            const svg = d3
                .select(`#${props.name}`)
                .append("svg")
                // .attr("height", "2rem")
                .attr("width", "100%")
                .attr("margin", 0);

            let scaleBand: number[] = [];
            let begin = 0;
            for (let i = 0; i < props.uptime.length; i++) {
                const u = props.uptime[i];
                scaleBand.push(begin);
                begin += calcDifference(u.begin, u.end) * scale;
            }

            svg.selectAll("mybar")
                .data(props.uptime)
                .join("rect")
                .attr("key", (u) => u.id)
                .attr("x", (u) => scaleBand[u.id])
                .attr("width", (u) => {
                    return calcDifference(u.begin, u.end) * scale;
                })
                .attr("height", 32)
                .attr("fill", (u) => (u.active ? "rgb(126, 211, 33)" : "rgb(229, 50, 18)"));
        }
    }

    return <div id={props.name} className="MM-Uptime" />;
}
