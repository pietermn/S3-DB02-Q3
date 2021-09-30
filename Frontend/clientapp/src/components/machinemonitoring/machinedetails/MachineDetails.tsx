import { useState } from 'react';
import { GrStatusGoodSmall as StatusDot } from 'react-icons/gr';
import MachineStatus from '../machinestatus/MachineStatus';
import Modal from 'react-modal';
import './MachineDetails.scss';

interface IMachineDetails {
    status: boolean,
    machine: string,
    product: string,
    uptime: boolean[],
    amount: number
}

export default function MachineDetails(props: IMachineDetails) {
    const [show, setShow] = useState(false);

    return (
        <>
            <Modal
                className='MM-Modal'
                isOpen={show}
                onRequestClose={() => setShow(false)}
                contentLabel={props.machine}
                shouldCloseOnOverlayClick={true}
            >
                <h1>Machine {props.machine}</h1>
                <h2>Component #1</h2>
                <h2>Component #2</h2>
                <button onClick={() => setShow(false)}>Close</button>
            </Modal>
            <tr className='MM-Data' onClick={() => setShow(true)}>

                <td className={props.status ? 'Good' : 'Bad'}><StatusDot /></td>
                <td>{props.machine}</td>
                <td>{props.product}</td>
                <td><MachineStatus name={props.machine} uptime={props.uptime} /></td>
                <td>{props.amount}</td>
            </tr>
        </>
    )
}
