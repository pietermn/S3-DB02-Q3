import { useContext, useEffect, useState } from "react";
import MachineDetails from "../../components/machinemonitoring/machinedetails/MachineDetails";
import "./MachineMonitoringPage.scss";
import { UpdaterContext } from "../../context/UpdaterContext";
import { GetProductionLines } from "../../api/requests/productionlines";
import { ProductionLine } from "../../globalTypes";

export default function MachineMonitoringPage() {
    const { bool } = useContext(UpdaterContext);
    const [productionLines, setProductionLines] = useState<ProductionLine[]>();

    if (bool) {
        AsyncGetProductionLines();
    }

    useEffect(() => {
        AsyncGetProductionLines();
    }, []);

    async function AsyncGetProductionLines() {
        setProductionLines(await GetProductionLines());
    }

    return (
        <div className="MM-Page">
            <table className="MM-Table">
                <thead>
                    <tr>
                        <th>Status</th>
                        <th>Machine</th>
                        <th>Uptime</th>
                        <th>Components</th>
                    </tr>
                </thead>
                <tbody>
                    {productionLines &&
                        productionLines.map((productionLine, index) => {
                            return (
                                <MachineDetails
                                    key={index}
                                    id={productionLine.id}
                                    components={productionLine.components}
                                    status={true}
                                    productionLine={productionLine.name}
                                    product=""
                                />
                            );
                        })}
                </tbody>
            </table>
        </div>
    );
}
