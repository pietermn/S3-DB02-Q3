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
                    <MachineDetails uptime={[true, false, true, true, false, true, false, true, true, true]} amount={5} status={true} machine='A1' product='Mes'/>
                    <MachineDetails uptime={[true, true, true, true, false, true, false, true, true, false]} amount={3} status={false} machine='B1' product='Vork'/>
                    <MachineDetails uptime={[true, false, false, false, false, true, false, true, true, true]} amount={2} status={true} machine='A2' product='Lepel'/>
                </tbody>
            </table>
        </div>
    )
}
