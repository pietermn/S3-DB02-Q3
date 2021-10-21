import ComponentsTable from '../../components/lifespan/componentstable/ComponentsTable'
import Modal from 'react-modal';
import './LifespanPage.scss'
import { Component } from '../../globalTypes';
import { useState } from 'react';

export default function LifespanPage() {

    const [show, setShow] = useState(false)
    const [selectedComponent, setSelectedComponent] = useState<Component>();

    function handleSelectedComponent(component: Component) {
        setSelectedComponent(component);
        setShow(true);
    }

    return (
        <div className="Lifespan-page">
            <Modal
                className="Modal"
                isOpen={show}
                ariaHideApp={false}
                onRequestClose={() => setShow(false)}
                contentLabel={selectedComponent?.description}
                shouldCloseOnOverlayClick={true}
            >
                <h1>Production line {selectedComponent?.description}</h1>
                <h2>No components found</h2>
                <button onClick={() => setShow(false)}>Close</button>
            </Modal>
            <h1>Components <i>Sort by total actions</i></h1>
            <div className="center-table">
                <ComponentsTable setSelectedComponet={handleSelectedComponent} />
            </div>
        </div>
    )
}
