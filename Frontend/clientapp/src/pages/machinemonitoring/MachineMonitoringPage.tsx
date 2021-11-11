import { useEffect, useState } from "react";
import MachineDetails from "../../components/machinemonitoring/machinedetails/MachineDetails";
import { ProductionLine } from "../../globalTypes";
import { GetProductionLines } from "../../api/requests/productionlines";
import "./MachineMonitoringPage.scss";
import { useTranslation } from "react-i18next";

export default function MachineMonitoringPage() {
    const [productionLines, setProductionLines] = useState<ProductionLine[]>([]);
    const { t } = useTranslation();

    async function AsyncGetProductionLines() {
        setProductionLines(await GetProductionLines());
    }

    useEffect(() => {
        AsyncGetProductionLines();
    }, []);

    return (
        <div className="MM-Page">
            <table className="MM-Table">
                <thead>
                    <tr>
                        <th>{t("status.label")}</th>
                        <th>{t("productionline.label")}</th>
                        <th>{t("uptime.label")}</th>
                        <th>{t("components.label")}</th>
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
                    {/* {productionLines &&
                        <MachineDetails key={1} id={381} components={[]} status={true} productionLine='test' product='' />
                    } */}
                </tbody>
            </table>
        </div>
    );
}
