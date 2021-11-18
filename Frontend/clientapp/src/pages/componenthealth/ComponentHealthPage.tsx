import { useContext, useEffect, useState } from "react";
import ActionsGraph from "../../components/componenthealth/ActionsGraph/ActionsGraph";
import ComponentsTable from "../../components/componenthealth/componentstable/ComponentsTable";
import HistoryTable from "../../components/componenthealth/HistoryTable/HistoryTable";
import { Component, Maintenance, MaintenanceNotification } from "../../globalTypes";
import { GetComponents, GetAllMaintenance } from "../../api/requests/components";
import "./ComponentHealthPage.scss";
import { useLocation } from "react-router-dom";
import { UpdaterContext } from "../../context/UpdaterContext";
import { useTranslation } from "react-i18next";
import MaintenanceTable from "../../components/componenthealth/MaintenanceTable/MaintenanceTable";
import { MaintenanceContext } from "../../context/MaintenanceContext";

export default function ComponentHealthPage() {
    const { bool } = useContext(UpdaterContext);
    const [components, setComponents] = useState<Component[]>([]);
    const location = useLocation();
    const [selectedComponent, setSelectedComponent] = useState<Component>();
    const [selectedMaintenance, setSelectedMaintenance] = useState<Maintenance[]>([]);
    const { t } = useTranslation();
    const { finishMaintenance } = useContext(MaintenanceContext);

    if (bool) {
        AsyncGetComponents();
    }

    async function AsyncGetComponents() {
        setComponents(await GetComponents());
    }

    async function GetMaintenance(id: number) {
        setSelectedMaintenance(await GetAllMaintenance(id));
    }

    function HandleSelectedComponent(component: Component) {
        if (component) {
            setSelectedComponent(component);
            GetMaintenance(component.id);
        }
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

    function handleFinshMaintenace(id: number) {
        finishMaintenance(id);
        let temp = [...selectedMaintenance];
        temp.forEach((maintenance) => {
            if (maintenance.id === id) {
                maintenance.done = true;
                maintenance.timeDone = new Date();
            }
        });
        setSelectedMaintenance(temp);
    }

    useEffect(() => {
        if (!components.length) {
            AsyncGetComponents();
        }
        if (components && components.length) {
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
                    <h1>
                        <b>{t("components.label")}</b>
                    </h1>
                    {components && (
                        <ComponentsTable
                            selectedComponentId={selectedComponent?.id || 0}
                            SetComponent={HandleSelectedComponent}
                            components={components}
                        />
                    )}
                </div>
            </section>

            {selectedComponent && (
                <section className="Component-History-Side">
                    <section className="Component-Graph">
                        <h1>
                            <b>{t("history.label")}:</b> {selectedComponent.description}
                        </h1>
                        <h3>
                            <b>{t("totalactions.label")}:</b> {selectedComponent.totalActions}
                        </h3>
                        <ActionsGraph component_id={selectedComponent.id} />
                    </section>

                    <section className="Component-History-Overview">
                        <HistoryTable HistoryMachines={selectedComponent.history} />
                    </section>

                    {selectedMaintenance.length && selectedMaintenance ? (
                        <section className="Component-Maintenance-Table">
                            <MaintenanceTable
                                finishMaintenance={handleFinshMaintenace}
                                maintenance={selectedMaintenance}
                            />
                        </section>
                    ) : null}
                </section>
            )}
        </div>
    );
}
