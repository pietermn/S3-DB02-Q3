import { useEffect, useState } from "react";
import ActionsGraph from "../../components/componenthealth/ActionsGraph/ActionsGraph";
import ComponentsTable from "../../components/componenthealth/componentstable/ComponentsTable";
import HistoryTable from "../../components/componenthealth/HistoryTable/HistoryTable";
import { Component } from "../../globalTypes";
import { GetComponents, GetPreviousActions } from '../../Api/requests/components';
import "./ComponentHealthPage.scss";
import { useLocation } from "react-router-dom";

export default function ComponentHealthPage() {

    const [components, setComponents] = useState<Component[]>([])
    const location = useLocation();
    const [selectedComponent, setSelectedComponent] = useState<Component>();
    const [actions, setActions] = useState<number[]>();
    const [key, setKey] = useState<number>(0);

    async function AsyncGetComponents() {
        setComponents(await GetComponents());
    }

    function HandleSelectedComponent(component: Component) {
        if (component) {
            setSelectedComponent(component);
            AsyncGetPreviousActions(component.id);
        }
    }

    function FindSelectedComponent(state: IComponentId) {
        if (!selectedComponent) {
            if (state) {
                for (let i = 0; i < components.length; i++) {
                    if (components[i].id === state.componentId) {
                        HandleSelectedComponent(components[i])
                    }
                }
            } else {
                HandleSelectedComponent(components[0])
            }
        }
    }

    async function AsyncGetPreviousActions(id: number) {
        setActions(await GetPreviousActions(id));
        setKey(id);
    }

    useEffect(() => {
        if (!components.length) {
            AsyncGetComponents();
        }
        if (components) {
            const state = location.state as IComponentId
            FindSelectedComponent(state);
        }
    }, [components.length, FindSelectedComponent])

    type IComponentId = {
        componentId: number
    }

    return (
        <div className="Components-Full-Page">
            <section className="Component-Overview">
                <div className="center-table">
                    <h1>Components</h1>
                    {components && <ComponentsTable SetComponent={HandleSelectedComponent} components={components} />}
                </div>
            </section>

            {selectedComponent && actions && <section className="Component-Graph">
                <h1>History {selectedComponent.description}</h1>
                <ActionsGraph key={key} actions={actions} />
            </section>}

            {
                selectedComponent && <section className="Component-History-Overview">
                    <HistoryTable HistoryMachines={selectedComponent.history} />
                </section>
            }
        </div >
    )
}