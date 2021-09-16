import './MachineDetails.scss';
import MachineStatus from '../machinestatus/MachineStatus';
import { GrStatusGoodSmall as StatusDot} from 'react-icons/gr'

interface IMachineDetails {
    status: boolean,
    machine: string,
    product: string,
    uptime: boolean[],
    amount: number
}

export default function MachineDetails(props: IMachineDetails) {
    return (
        <tr className='MM-Data'>
            <td style={{ color: props.status ? '#99CB0E' : 'red' }}><StatusDot /></td>
            <td>{props.machine}</td>
            <td>{props.product}</td>
            <td><MachineStatus uptime={props.uptime}/></td>
            <td>{props.amount}</td>
        </tr>
    )
}
