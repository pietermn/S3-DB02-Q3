import ComponentsTable from "../../components/lifespan/componentstable/ComponentsTable";
import Modal from "react-modal";
import "./LifespanPage.scss";
import RemoveYesNo from "../../components/popover/RemoveYesNo";
import { Component } from "../../globalTypes";
import { useCallback, useContext, useEffect, useState } from "react";
import { ImCheckmark } from "react-icons/im";
import { NotificationContext } from "../../context/NotificationContext";
import { GetComponents, PredictMaintenance as ApiPredictMaintenance } from "../../api/requests/components";
import { MaintenanceContext } from "../../context/MaintenanceContext";
import { useHistory, useLocation } from "react-router";
import { UpdaterContext } from "../../context/UpdaterContext";
import { useTranslation } from "react-i18next";
import { CircularProgress, LinearProgress } from "@mui/material";

type IComponentId = {
    componentId: number;
};

export default function LifespanPage() {
    const [components, setComponents] = useState<Component[]>([]);
    const [selectedComponent, setSelectedComponent] = useState<Component>();
    const [maxActionsInput, setMaxActionsInput] = useState(selectedComponent?.maxActions || 0);
    const [description, setDescription] = useState("");
    const [removePopover, setRemovePopover] = useState("");
    const [anchorEl, setAnchorEl] = useState<SVGElement | null>(null);
    const location = useLocation();
    const { setMaxActions } = useContext(NotificationContext);
    const { addMaintenance, finishMaintenance, getComponentMaintenance, removeMaintenance } =
        useContext(MaintenanceContext);
    const state = location.state as IComponentId;
    const history = useHistory();
    const { t } = useTranslation();
    const { bool } = useContext(UpdaterContext);
    const [predictedMaintenance, setPredictedMaintenance] = useState("");

    function findSelectedComponent(components: Component[]) {
        if (state && state.componentId && components) {
            setSelectedComponent(components.filter((c) => c.id === state.componentId)[0]);
        }
    }

    function handleSelectedComponent(component: Component) {
        setSelectedComponent(component);
        PredictMaintenance(component.id);
        setMaxActionsInput(component.maxActions);
    }
    async function GetComponentsAsync() {
        let components: Component[] = await GetComponents();

        setComponents(components);
        findSelectedComponent(components);
    }

    if (bool) {
        GetComponentsAsync();
    }

    useEffect(() => {
        if (state) {
            let c = components.find((c) => c.id === state.componentId);
            if (c) {
                handleSelectedComponent(c);
            }
        }
        // eslint-disable-next-line
    }, [state && state.componentId]);

    useEffect(() => {
        GetComponentsAsync();

        return () => {
            history.replace({ state: undefined });
        };
        // eslint-disable-next-line
    }, [components.length, state && state.componentId]);

    function ClearData() {
        setMaxActionsInput(0);
        setDescription("");
    }

    async function PredictMaintenance(componentId: number) {
        setPredictedMaintenance("loading");
        let date = await ApiPredictMaintenance(componentId);

        if (selectedComponent?.maxActions === 1) {
            setPredictedMaintenance("Cannot predict if no max actions is set");
            return;
        }

        if (new Date(date).toLocaleDateString() === "01/01/1") {
            setPredictedMaintenance("Cannot predict this component");
        } else if (new Date(date).toLocaleDateString() === "01/06/2021") {
            setPredictedMaintenance("Has already hit its max actions");
        } else {
            setPredictedMaintenance(new Date(date).toLocaleDateString());
        }
    }

    return (
        <div className="Lifespan-page">
            {selectedComponent && (
                <Modal
                    className="MM-Modal Lifespan-Modal"
                    isOpen={selectedComponent ? true : false}
                    ariaHideApp={false}
                    onRequestClose={() => {
                        setSelectedComponent(undefined);
                        ClearData();
                    }}
                    contentLabel={selectedComponent?.description}
                    shouldCloseOnOverlayClick={true}
                >
                    <h1>{selectedComponent?.description}</h1>
                    <div className="MaxActions">
                        <h3>{t("maxactions.label")}</h3>
                        <form
                            onSubmit={async (e) => {
                                e.preventDefault();
                                setMaxActions(selectedComponent.id, maxActionsInput);
                                const timeout = setTimeout(async () => {
                                    setComponents(await GetComponents());
                                    clearTimeout(timeout);
                                }, 500);
                                setSelectedComponent(undefined);
                                ClearData();
                            }}
                        >
                            <input
                                placeholder="Max actions..."
                                type="number"
                                value={maxActionsInput}
                                onChange={({ target }) => setMaxActionsInput(parseInt(target.value))}
                            />
                            <div className="MaxActions-Container">
                                <p>
                                    <b>{t("predictedto.label")}</b>
                                    <br />
                                    {predictedMaintenance === "loading" ? (
                                        <LinearProgress color="primary" className="LS-LinearProgress" />
                                    ) : (
                                        predictedMaintenance
                                    )}
                                </p>
                                <button type="submit">
                                    <ImCheckmark />
                                </button>
                            </div>
                        </form>
                    </div>
                    <div className="Planner">
                        <h3>
                            {t("plan.label")} {t("maintenance.label").toLowerCase()}
                        </h3>
                        <form
                            onSubmit={(e) => {
                                e.preventDefault();
                                addMaintenance(selectedComponent.id, description);
                            }}
                        >
                            <textarea
                                placeholder={`${t("maintenance.label")} ${t("description.label")}... `}
                                value={description}
                                onChange={(e) => setDescription(e.target.value)}
                            />
                            <button type="submit">
                                <ImCheckmark />
                            </button>
                        </form>
                    </div>
                    <div className="Maintenance">
                        <h3>{t("maintenance.label")}</h3>
                        <div className="Notification-Container">
                            {selectedComponent &&
                                getComponentMaintenance(selectedComponent.id).map((maintenance) => {
                                    return (
                                        <div key={maintenance.id}>
                                            <p>{maintenance.description}</p>
                                            <ImCheckmark
                                                aria-describedby={`Popover-${maintenance.id}`}
                                                onClick={(e) => {
                                                    setRemovePopover(maintenance.id.toString());
                                                    setAnchorEl(e.currentTarget);
                                                }}
                                            />
                                            <RemoveYesNo
                                                openState={removePopover === maintenance.id.toString()}
                                                setOpenState={setRemovePopover}
                                                anchorId={`Popover-${maintenance.id}`}
                                                anchor={anchorEl}
                                                setAnchor={setAnchorEl}
                                                message={<>{t("resetactions.label")}</>}
                                                remove={() => {
                                                    finishMaintenance(maintenance.id);
                                                }}
                                                cancel={() => {
                                                    removeMaintenance(maintenance.id);
                                                }}
                                            />
                                        </div>
                                    );
                                })}
                        </div>
                    </div>
                    <button
                        onClick={() => {
                            setSelectedComponent(undefined);
                            ClearData();
                        }}
                    >
                        {t("close.label")}
                    </button>
                </Modal>
            )}
            <div className="center-table">
                {components && (
                    <ComponentsTable
                        components={components}
                        setSelectedComponent={handleSelectedComponent}
                        getComponentNotifications={getComponentMaintenance}
                    />
                )}
            </div>
        </div>
    );
}
