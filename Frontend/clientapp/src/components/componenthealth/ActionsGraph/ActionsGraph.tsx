import * as d3 from "d3";
import { useEffect, useMemo, useRef, useState } from "react";
import { NodeGroup } from "react-move";
import { GetPreviousActions } from '../../../api/requests/components';
import "./ActionsGraph.scss"

interface IActionsGraph {
    component_id: number
}

class Actions {
    id: number;
    value: number;
    name: string;

    constructor(id: number, value: number) {
        this.id = id;
        this.value = value;
        this.name = `week ${id + 1}`
    }
}

let barHeight = 25;
let barPadding = 2;
let barColour = "#348AA7";
let widthScale = (d: number) => d * 5;

interface IBarGroup {
    state: {
        value: number,
        y: number,
        opacity: number
    },
    data: Actions,
    maxValue: number
}

function TransformActions(actions: number[]): Actions[] {
    let newActions: Actions[] = [];
    console.log(actions);
    for (var i = 0; i < actions.length; i++) {
        newActions.push(new Actions(i, actions[i]));
    }

    return newActions;
}

function BarGroup(props: IBarGroup) {
    console.log(props.maxValue);
    let screenwidth = ((window.innerWidth / 3) * 2) * .75;
    let percentage = screenwidth / props.maxValue;
    let barWidth = (d: number) => d * percentage;
    let width = barWidth(props.state.value);
    let yMid = barHeight * 0.5;

    return (
        <g className="bar-group" transform={`translate(0, ${props.state.y})`}>
            <rect
                y={barPadding * 0.5}
                width={width >= props.maxValue ? props.maxValue : width}
                height={barHeight - barPadding}
                style={{ fill: barColour, opacity: props.state.opacity }}
            />
            <text
                className="value-label"
                x={width - 6}
                y={yMid}
                alignmentBaseline="middle"
            >
                {props.state.value.toFixed(0)}
            </text>
            <text
                className="name-label"
                x="-6"
                y={yMid}
                alignmentBaseline="middle"
                style={{ opacity: props.state.opacity }}
            >
                {props.data.name}
            </text>
        </g>
    );
}

export default function ActionsGraph(props: IActionsGraph) {

    const [actions, setActions] = useState<Actions[]>([])
    const [maxValue, setMaxValue] = useState(0)

    useEffect(() => {
        async function AsyncGetPreviousActions() {
            let oldActions: number[] = await GetPreviousActions(props.component_id);
            setMaxValue(Math.max(...oldActions));
            setActions(TransformActions(oldActions));
        }
        AsyncGetPreviousActions();
    }, [props.component_id])

    function startTransition(d: Actions, i: number) {
        return { value: 0, y: i * barHeight, opacity: 0 };
    }

    function enterTransition({ value }: Actions) {
        return { value: [value], opacity: [1], timing: { duration: 2000 } };
    }

    function updateTransition({ value }: Actions, i: number) {
        return { value: [value], y: [i * barHeight], timing: { duration: 2000 } };
    }

    function leaveTransition(d: Actions) {
        return { y: [-barHeight], opacity: [0], timing: { duration: 2000 } };
    }

    return (
        <svg className="chart-svg">
            <g className="chart">
                {useMemo(() => <NodeGroup
                    data={actions}
                    keyAccessor={(d: Actions) => d.name}
                    start={startTransition}
                    enter={enterTransition}
                    update={updateTransition}
                    leave={leaveTransition}
                >
                    {
                        nodes => (
                            <g>
                                {nodes.map(({ key, data, state }) => (
                                    <BarGroup maxValue={maxValue} key={key} data={data} state={state} />
                                ))}
                            </g>
                        )
                    }
                </NodeGroup>, [JSON.stringify(actions)])}
            </g>
        </svg>
    )
}