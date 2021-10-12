import { useEffect, useState } from "react";
import ActionsGraph from "../../components/componenthealth/ActionsGraph/ActionsGraph";
import ComponentsTable from "../../components/componenthealth/componentstable/ComponentsTable";
import HistoryTable from "../../components/componenthealth/HistoryTable/HistoryTable";
import { Component, ComponentType } from "../../globalTypes";
import {GetComponents} from '../../Api/requests/components';
import "./ComponentHealthPage.scss";

export default function ComponentHealthPage() {

    const [components, setComponents] = useState<Component[]>([])

    async function AsyncGetComponents() {
        setComponents(await GetComponents());
    }

    useEffect(() => {
        AsyncGetComponents();
    }, [])

    const [selectedComponent, setSelectedComponent] = useState<Component>();

    return (
        <div className="Components-Full-Page">
            <section className="Component-Overview">
                <div className="center-table">
                    <h1>Components</h1>
                    {components && <ComponentsTable SetComponent={setSelectedComponent} components={components} />}
                </div>
            </section>

            {selectedComponent && <section className="Component-Graph">
                <h1>History {selectedComponent.description}</h1>
                <ActionsGraph />
            </section>}

            {
                selectedComponent && <section className="Component-History-Overview">
                    <HistoryTable HistoryMachines={selectedComponent.history} />
                </section>
            }
        </div >
    )
}