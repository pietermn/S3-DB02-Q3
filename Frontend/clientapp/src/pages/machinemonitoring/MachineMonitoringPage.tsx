import { useEffect, useState } from 'react';
import MachineDetails from '../../components/machinemonitoring/machinedetails/MachineDetails'
import {ProductionLine} from '../../globalTypes';
import {GetProductionLines} from '../../api/requests/productionlines';
import './MachineMonitoringPage.scss'

export default function MachineMonitoringPage() {

    
    const [productionLines, setProductionLines] = useState<ProductionLine[]>([])

    async function AsyncGetProductionLines() {
        setProductionLines(await GetProductionLines());

    }

    useEffect(() => {
        AsyncGetProductionLines();
    }, [])

    return (
        <div className='MM-Page'>
            <table className='MM-Table'>
                <thead>
                    <tr>
                    <th>Status</th>
                    <th>Machine</th>
                    <th>Product</th>
                    <th>Uptime</th>
                    <th>Components</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        productionLines && productionLines.map((productionLine, index) => {
                            return (
                                <MachineDetails key={index} uptime={[true, false, true, true, false, true, false, true, true, true]} components={productionLine.components} status={true} productionLine={productionLine.name} product=''/>
                            )
                        })
                    }
                </tbody>
            </table>
        </div>
    )
}
