import { useTranslation } from "react-i18next";
import { Component } from "../../../globalTypes";
import "../HistoryTable/HistoryTable.scss";
import "./ComponentsTable.scss";

interface ITableProps {
    components: Component[];
    selectedComponentId: number;
    SetComponent: (component: Component) => void;
}

export default function ComponentsTable(props: ITableProps) {
    const { t } = useTranslation();

    return (
        <div className="Components-Table">
            <main>
                <div className="row header">
                    <p>{t("name.label")}</p>
                    <p>{t("totalactions.label")}</p>
                </div>
                {props.components.map((component: Component, index: number) => {
                    return (
                        <div
                            key={index}
                            onClick={() => props.SetComponent(component)}
                            className={props.selectedComponentId == component.id ? "row selected" : "row"}
                        >
                            <p>{component.description}</p>
                            <p>{component.totalActions}</p>
                        </div>
                    );
                })}
            </main>
        </div>
    );
}
