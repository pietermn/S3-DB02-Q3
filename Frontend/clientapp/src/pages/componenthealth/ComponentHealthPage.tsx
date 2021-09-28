import { useState } from "react";
import ActionsGraph from "../../components/componenthealth/ActionsGraph/ActionsGraph";
import ComponentsTable from "../../components/componenthealth/componentstable/ComponentsTable";
import HistoryTable from "../../components/componenthealth/HistoryTable/HistoryTable";
import { Component, ComponentType } from "../../globalTypes";
import "./ComponentHealthPage.scss";

export default function ComponentHealthPage() {

    function GenerateComponents(totalComponents: number): Component[] {
        let Components: Component[] = [];

        for (var i: number = 0; i < totalComponents; i++) {
            const actions = Math.floor(Math.random() * 1000);
            Components.push({
                Id: i,
                Name: "Matrijs",
                Type: ComponentType.Coldhalf,
                Description: "Vork",
                Port: 0,
                Board: 0,
                TotalActions: actions,
                History: [
                    {
                        ProductionLine: {
                            Id: 1,
                            Name: `A01 #${i}`,
                            Description: "NE 280 ton",
                            Active: true,
                            Port: 0,
                            Board: 0,
                        },
                        StartDate: new Date("05-01-2000"),
                        EndDate: new Date("06-01-2000")
                    },
                    {
                        ProductionLine: {
                            Id: 2,
                            Name: "A01",
                            Description: "NE 280 ton",
                            Active: true,
                            Port: 0,
                            Board: 0,
                        },
                        StartDate: new Date("05-01-2000"),
                        EndDate: new Date("06-01-2000")
                    },
                    {
                        ProductionLine: {
                            Id: 3,
                            Name: "A01",
                            Description: "NE 280 ton",
                            Active: true,
                            Port: 0,
                            Board: 0,
                        },
                        StartDate: new Date("05-01-2000"),
                        EndDate: new Date("06-01-2000")
                    },
                    {
                        ProductionLine: {
                            Id: 4,
                            Name: "A01",
                            Description: "NE 280 ton",
                            Active: true,
                            Port: 0,
                            Board: 0,
                        },
                        StartDate: new Date("05-01-2000"),
                        EndDate: new Date("06-01-2000")
                    },
                ]
            })
        }

        return Components;
    }

    const [selectedComponent, setSelectedComponent] = useState<Component>();

    return (
        <div className="Components-Full-Page">
            <section className="Component-Overview">
                <div className="center-table">
                    <h1>Components</h1>
                    <ComponentsTable SetComponent={setSelectedComponent} components={GenerateComponents(50)} />
                </div>
            </section>

            {selectedComponent && <section className="Component-Graph">
                <h1>History {selectedComponent.Description}</h1>
                <ActionsGraph />
            </section>}

            {
                selectedComponent && <section className="Component-History-Overview">
                    <HistoryTable HistoryMachines={selectedComponent.History} />
                </section>
            }
        </div >
    )
}