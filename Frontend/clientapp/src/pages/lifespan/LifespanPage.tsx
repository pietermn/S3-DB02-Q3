import ComponentsTable from "../../components/lifespan/componentstable/ComponentsTable";
import Modal from "react-modal";
import "./LifespanPage.scss";
import { Component } from "../../globalTypes";
import { useContext, useEffect, useRef, useState } from "react";
import { ImCheckmark } from "react-icons/im";
import { NotificationContext } from "../../context/NotificationContext";
import { GetComponents } from "../../api/requests/components";
import { MaintenanceContext } from "../../context/MaintenanceContext";
import { useHistory, useLocation } from "react-router";
import { useTranslation } from "react-i18next";

type IComponentId = {
  componentId: number;
};

export default function LifespanPage() {
  const [components, setComponents] = useState<Component[]>([]);
  const [selectedComponent, setSelectedComponent] = useState<Component>();
  const [maxActionsInput, setMaxActionsInput] = useState(selectedComponent?.maxActions || 0);
  const [description, setDescription] = useState("");
  const location = useLocation();
  const { setMaxActions } = useContext(NotificationContext);
  const { addMaintenance, finishMaintenance, getComponentMaintenance } = useContext(MaintenanceContext);
  const state = location.state as IComponentId;
  const history = useHistory();
  const { t } = useTranslation();

  function findSelectedComponent(components: Component[]) {
    if (state && state.componentId && components) {
      setSelectedComponent(components.filter((c) => c.id == state.componentId)[0]);
    }
  }

  function handleSelectedComponent(component: Component) {
    setSelectedComponent(component);
    setMaxActionsInput(component.maxActions);
  }

  useEffect(() => {
    async function GetComponentsAsync() {
      let components: Component[] = await GetComponents();

      setComponents(components);
      findSelectedComponent(components);
    }

    GetComponentsAsync();

    return () => {
      history.replace({ state: undefined });
    };
  }, [components.length, state && state.componentId]);

  function ClearData() {
    setMaxActionsInput(0);
    setDescription("");
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
              <button type="submit">
                <ImCheckmark />
              </button>
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
                      <ImCheckmark onClick={() => finishMaintenance(maintenance.id)} />
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
      <h1>
        {t("components.label")} <i>{t("sortbytotalactions.label")}</i>
      </h1>
      <div className="center-table">
        {components && <ComponentsTable components={components} setSelectedComponet={handleSelectedComponent} />}
      </div>
    </div>
  );
}
