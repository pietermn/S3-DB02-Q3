import { useContext, useEffect, useState } from "react";
import ActionsGraph from "../../components/componenthealth/ActionsGraph/ActionsGraph";
import ComponentsTable from "../../components/componenthealth/componentstable/ComponentsTable";
import HistoryTable from "../../components/componenthealth/HistoryTable/HistoryTable";
import { Component } from "../../globalTypes";
import { GetComponents, GetPreviousActions } from "../../api/requests/components";
import "./ComponentHealthPage.scss";
import { useLocation } from "react-router-dom";
import { UpdaterContext } from "../../context/UpdaterContext";

export default function ComponentHealthPage() {
    const { bool } = useContext(UpdaterContext);

    const [components, setComponents] = useState<Component[]>([]);
    const location = useLocation();
    const [selectedComponent, setSelectedComponent] = useState<Component>();
    const [key, setKey] = useState<number>(0);

    async function AsyncGetComponents() {
        setComponents(await GetComponents());
    }

    function HandleSelectedComponent(component: Component) {
        if (component) {
            setSelectedComponent(component);
            setKey(component.id);
        }
    }

    if (bool) {
        AsyncGetComponents();
    }

    function FindSelectedComponent(state: IComponentId) {
        if (!selectedComponent) {
            if (state) {
                for (let i = 0; i < components.length; i++) {
                    if (components[i].id === state.componentId) {
                        HandleSelectedComponent(components[i]);
                    }
                }
            } else {
                HandleSelectedComponent(components[0]);
            }
        }
    }

    useEffect(() => {
        if (!components.length) {
            AsyncGetComponents();
        }
        if (components) {
            const state = location.state as IComponentId;
            FindSelectedComponent(state);
        }
    }, [components.length, FindSelectedComponent]);

    type IComponentId = {
        componentId: number;
    };

    return (
        <div className="Components-Full-Page">
            <section className="Component-Overview">
                <div className="center-table">
                    <h1>Components</h1>
                    {components && <ComponentsTable SetComponent={HandleSelectedComponent} components={components} />}
                </div>
            </section>

            {selectedComponent && (
                <section className="Component-Graph">
                    <h1>History {selectedComponent.description} </h1>
                    <ActionsGraph component_id={selectedComponent.id} />
                </section>
            )}

            {selectedComponent && (
                <section className="Component-History-Overview">
                    <HistoryTable HistoryMachines={selectedComponent.history} />
                </section>
            )}
        </div>
    );
}
