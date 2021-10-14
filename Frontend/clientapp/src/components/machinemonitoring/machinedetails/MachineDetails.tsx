import { useState } from 'react';
import { GrStatusGoodSmall as StatusDot } from 'react-icons/gr';
import MachineStatus from '../machinestatus/MachineStatus';
import Modal from 'react-modal';
import './MachineDetails.scss';
import { Component } from '../../../globalTypes';
import { useHistory } from 'react-router-dom';

interface IMachineDetails {
    status: boolean,
    productionLine: string,
    product: string,
    uptime: boolean[],
    components?: Component[]
}

export default function MachineDetails(props: IMachineDetails) {
    const [show, setShow] = useState(false);
    const history = useHistory();

    function ToComponents(componentId: number) {
        
    }

    return (
        <>
            <Modal
                className='MM-Modal'
                isOpen={show}
                onRequestClose={() => setShow(false)}
                contentLabel={props.productionLine}
                shouldCloseOnOverlayClick={true}
            >
                <h1>Production line {props.productionLine}</h1>
                {
                    props.components && props.components.length ? 
                    props.components.map((component, index) => {
                        return (
                            <h2 className="redirect-component" onClick={() => history.push({
                                pathname: "/chealth", 
                                state: {componentId: component.id}})}>{component.description}</h2>
                        )
                    })
                    : <h2>No components found</h2>
                }
                <button onClick={() => setShow(false)}>Close</button>
            </Modal>
            <tr className='MM-Data' onClick={() => setShow(true)}>

                <td className={props.status ? 'Good' : 'Bad'}><StatusDot /></td>
                <td>{props.productionLine}</td>
                <td>{props.product}</td>
                <td><MachineStatus name={props.productionLine} uptime={props.uptime} /></td>
                <td>{props.components?.length || 0}</td>
            </tr>
        </>
    )
}
