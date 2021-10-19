import { Component } from "../../../globalTypes";
import "../HistoryTable/HistoryTable.scss";
import "./ComponentsTable.scss";

interface ITableProps {
    components: Component[],
    SetComponent: (component: Component) => void;
}

export default function ComponentsTable(props: ITableProps) {
    return (
        <div className="Components-Table">
            <div className="thead">
                <div className="row">
                    <p>Name</p>
                    <p>Total Actions</p>
                </div>
            </div>
            <div className="tbody">
                {
                    props.components.map((component: Component, index: number) => {
                        return (
                            <div key={index} onClick={() => props.SetComponent(component)} className="row">
                                <p>{component.description}</p>
                                <p>{component.totalActions}</p>
                            </div>
                        )
                    })
                }
            </div>
        </div>
    )
}