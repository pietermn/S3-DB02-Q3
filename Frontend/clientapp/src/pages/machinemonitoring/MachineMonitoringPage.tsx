import MachineDetails from '../../components/machinemonitoring/machinedetails/MachineDetails'
import './MachineMonitoringPage.scss'

export default function MachineMonitoringPage() {
    return (
        <div className='MM-Page'>
            <table className='MM-Table'>
                <thead>
                    <th>Status</th>
                    <th>Machine</th>
                    <th>Product</th>
                    <th>Uptime</th>
                    <th>Components</th>
                </thead>
                <tbody>
                    {/* incommingData.map() */}
                    <MachineDetails amount={521} status={true} machine="A1" product="Mes"/>
                    <MachineDetails amount={3654} status={false} machine="B1" product="Vork"/>
                    <MachineDetails amount={1247} status={true} machine="A2" product="Lepel"/>
                </tbody>
            </table>
        </div>
    )
}
