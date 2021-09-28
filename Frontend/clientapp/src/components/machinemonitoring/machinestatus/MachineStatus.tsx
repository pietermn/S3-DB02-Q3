import './MachineStatus.scss'

interface IMachineStatus {
    uptime: boolean[]
}

export default function MachineStatus(props: IMachineStatus) {
    return (
        <div className='MM-Uptime'>
            {props.uptime.map((value: boolean, index: number) => {
                return (
                    <div key={index} className={ value ? 'good' : 'bad' } />
                )
            })}
        </div>
    )
}
