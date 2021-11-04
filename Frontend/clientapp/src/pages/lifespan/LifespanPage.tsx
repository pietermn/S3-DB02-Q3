import ComponentsTable from "../../components/lifespan/componentstable/ComponentsTable";
import Modal from "react-modal";
import "./LifespanPage.scss";
import { Component } from "../../globalTypes";
import { useContext, useEffect, useState } from "react";
import { ImCheckmark } from "react-icons/im";
import { NotificationContext } from "../../context/NotificationContext";
import { GetComponents } from "../../api/requests/components";
import { MaintenanceContext } from "../../context/MaintenanceContext";

export default function LifespanPage() {
  const [show, setShow] = useState(false);
  const [components, setComponents] = useState<Component[]>([]);
  const [selectedComponent, setSelectedComponent] = useState<Component>();
  const [maxActionsInput, setMaxActionsInput] = useState(0);
  const [description, setDescription] = useState("");

  const { setMaxActions } = useContext(NotificationContext);

  const { addMaintenance, finishMaintenance, getComponentMaintenance } =
    useContext(MaintenanceContext);

  function handleSelectedComponent(component: Component) {
    setSelectedComponent(component);
    setShow(true);
  }

  useEffect(() => {
    async function GetComponentsAsync() {
      setComponents(await GetComponents());
    }

    GetComponentsAsync();
  }, []);

  return (
    <div className="Lifespan-page">
      {selectedComponent && (
        <Modal
          className="MM-Modal Lifespan-Modal"
          isOpen={show}
          ariaHideApp={false}
          onRequestClose={() => setShow(false)}
          contentLabel={selectedComponent?.description}
          shouldCloseOnOverlayClick={true}
        >
          <h1>{selectedComponent?.description}</h1>
          <div className="MaxActions">
            <h3>Max Actions:</h3>
            <form
              onSubmit={async (e) => {
                e.preventDefault();
                setMaxActions(selectedComponent.id, maxActionsInput);
                const timeout = setTimeout(async () => {
                  setComponents(await GetComponents());
                  clearTimeout(timeout);
                }, 500);
                setShow(false);
              }}
            >
              <input
                placeholder="Max actions..."
                type="number"
                value={
                  !maxActionsInput
                    ? selectedComponent.maxActions
                    : maxActionsInput
                }
                onChange={({ target }) =>
                  setMaxActionsInput(parseInt(target.value))
                }
              />
              <button type="submit">
                <ImCheckmark />
              </button>
            </form>
          </div>
          <div className="Planner">
            <h3>Plan onderhoud in</h3>
            <form
              onSubmit={(e) => {
                e.preventDefault();
                addMaintenance(selectedComponent.id, description);
              }}
            >
              <textarea
                placeholder="Onderhoud uitleg..."
                value={description}
                onChange={(e) => setDescription(e.target.value)}
              />
              <button type="submit">
                <ImCheckmark />
              </button>
            </form>
          </div>
          <div className="Maintenance">
            <h3>Maintenance</h3>
            <div className="Notification-Container">
              {selectedComponent &&
                getComponentMaintenance(selectedComponent.id).map(
                  (maintenance) => {
                    console.log(selectedComponent.id);

                    console.log(maintenance);

                    return (
                      <div key={maintenance.id}>
                        <p>{maintenance.description}</p>
                        <ImCheckmark
                          onClick={() => finishMaintenance(maintenance.id)}
                        />
                      </div>
                    );
                  }
                )}
            </div>
          </div>
          <button onClick={() => setShow(false)}>Close</button>
        </Modal>
      )}
      <h1>
        Components <i>Sort by total actions</i>
      </h1>
      <div className="center-table">
        {components && (
          <ComponentsTable
            components={components}
            setSelectedComponet={handleSelectedComponent}
          />
        )}
      </div>
    </div>
  );
}
