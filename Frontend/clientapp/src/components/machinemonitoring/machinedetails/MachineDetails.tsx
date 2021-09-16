interface IMachineDetails {
    status: boolean,
    machine: string,
    product: string,

    amount: number
}

export default function MachineDetails(props: IMachineDetails) {
    return (
        <tr>
            <td>{props.status}</td>
            <td>{props.machine}</td>
            <td>{props.product}</td>
            <td><div></div></td>
            <td>{props.amount}</td>
        </tr>
    )
}
