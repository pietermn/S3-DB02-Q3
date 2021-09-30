import { Component } from "../../../globalTypes";
import "../HistoryTable/HistoryTable.scss";
import "./ComponentsTable.scss";

interface ITableProps {
    components: Component[],
    SetComponent: (component: Component) => void;
}

export default function ComponentsTable(props: ITableProps) {
    return (
        <table className="Components-Table">
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
                            <div onClick={() => props.SetComponent(component)} className="row">
                                <p>{component.Description}</p>
                                <p>{component.TotalActions}</p>
                            </div>
                        )
                    })
                }
            </div>
        </table>
    )
}