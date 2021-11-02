import ComponentsTable from "../../components/lifespan/componentstable/ComponentsTable";
import Modal from "react-modal";
import "./LifespanPage.scss";
import { Component } from "../../globalTypes";
import { useContext, useState } from "react";
import { ImCheckmark } from "react-icons/im";
import { NotificationContext } from "../../context/NotificationContext";

export default function LifespanPage() {
  const [show, setShow] = useState(false);
  const [selectedComponent, setSelectedComponent] = useState<Component>();
  const [maxActions, setMaxActions] = useState(0);
  const [description, setDescription] = useState("");

  const { getComponentNotifications, removeNotification } =
    useContext(NotificationContext);

  function handleSelectedComponent(component: Component) {
    setSelectedComponent(component);
    setShow(true);
  }

  return (
    <div className="Lifespan-page">
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
            onSubmit={(e) => {
              e.preventDefault();
              console.log({ maxActions, description });
            }}
          >
            <input
              placeholder="Max actions..."
              type="number"
              value={maxActions}
              onChange={({ target }) => setMaxActions(parseInt(target.value))}
            />
            <button type="submit">
              <ImCheckmark />
            </button>
          </form>
        </div>
        <div className="Planner">
          <h3>Plan onderhoud in</h3>
          <form>
            <textarea placeholder="Onderbeschrijving..." />
            <button type="submit">
              <ImCheckmark />
            </button>
          </form>
        </div>
        <div className="Maintenance">
          <h3>Maintenance</h3>
          <div className="Notification-Container">
            {selectedComponent &&
              getComponentNotifications(selectedComponent.id).map(
                (notification) => {
                  return (
                    <div key={notification.Id}>
                      <p>{notification.Message}</p>
                      <ImCheckmark
                        onClick={() => removeNotification(notification.Id)}
                      />
                    </div>
                  );
                }
              )}
          </div>
        </div>
        <button onClick={() => setShow(false)}>Close</button>
      </Modal>
      <h1>
        Components <i>Sort by total actions</i>
      </h1>
      <div className="center-table">
        <ComponentsTable setSelectedComponet={handleSelectedComponent} />
      </div>
    </div>
  );
}
