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
            const svgWidth = svgDocument || 0;
            const xDiff =
                new Date(props.uptime[props.uptime.length - 1].end).getTime() / 1000 -
                new Date(props.uptime[0].begin).getTime() / 1000;
            const scale = svgWidth / xDiff;

            d3.select(`#${props.name}`).select("svg").remove();

            const svg = d3
                .select(`#${props.name}`)
                .append("svg")
                .attr("height", 50)
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

            let hours = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24];

            // if minutes are 27, hour bar needs to go back 33 minutes
            let beginM = new Date(props.uptime[0].begin).getMinutes();
            let beginS = new Date(props.uptime[0].begin).getSeconds();
            let diffM = 60 - beginM;
            let diffS = 60 - beginS;
            let diffTotalS = diffS + diffM * 60;
            let diffTotalH = diffTotalS / 3600;

            // width of one hour
            let hourScale = svgWidth / 24;

            svg.selectAll("mybar")
                .data(hours)
                .join("rect")
                .attr("key", (h) => `Hour ${h}`)
                .attr("x", (h) => h * hourScale - diffTotalH * hourScale)
                .attr("width", 0.5)
                .attr("height", 32)
                .attr("fill", "rgb(0, 0, 0)");
            // .append("g")
            // .append("text")
            // .text((h) => hourText(h))
            // .attr("x", (h) => h * hourScale - diffTotalH * hourScale)

            // .attr("width", 30)
            // .attr("height", 10);

            // .append("p")
            // .attr("x", (h) => h * hourScale - diffTotalH * hourScale)
            // .attr("width", "10ch")

            // .attr("height", "1rem");
        }
        function hourText(hour: number) {
            return new Date(new Date(props.uptime[0].begin).getHours() + hour, 0, 0).toLocaleTimeString();
        }
    }

    return <div id={props.name} className="MM-Uptime" />;
}
