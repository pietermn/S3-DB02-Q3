import { useEffect, useState } from "react";
import MachineDetails from "../../components/machinemonitoring/machinedetails/MachineDetails";
import { ProductionLine } from "../../globalTypes";
import { GetProductionLines } from "../../api/requests/productionlines";
import "./MachineMonitoringPage.scss";
import { useTranslation } from "react-i18next";
import { FaInfoCircle as InfoIcon } from "react-icons/fa";
import { IconButton } from "@material-ui/core";
import { styled } from "@mui/material/styles";
import Tooltip, { TooltipProps, tooltipClasses } from "@mui/material/Tooltip";
import axios from "axios";

export default function MachineMonitoringPage() {
    const [productionLines, setProductionLines] = useState<ProductionLine[]>([]);
    const [source] = useState(axios.CancelToken.source());
    const { t } = useTranslation();
    const uptimeTooltip = t("uptimetooltip.label");

    async function AsyncGetProductionLines() {
        setProductionLines(await GetProductionLines());
    }

    useEffect(() => {
        AsyncGetProductionLines();

        return () => {
            source.cancel("Left MM page");
        };
        // eslint-disable-next-line
    }, []);

    const StatusTooltip = styled(({ className, ...props }: TooltipProps) => (
        <Tooltip {...props} classes={{ popper: className }} />
    ))(({ theme }) => ({
        [`& .${tooltipClasses.tooltip}`]: {
            fontSize: 14,
        },
    }));

    return (
        <div className="MM-Page">
            <table className="MM-Table">
                <thead>
                    <tr>
                        <th>{t("status.label")}</th>
                        <th>{t("productionline.label")}</th>
                        <th>
                            {t("uptime.label")}
                            <StatusTooltip title={uptimeTooltip}>
                                <IconButton disableRipple>
                                    <InfoIcon />
                                </IconButton>
                            </StatusTooltip>
                        </th>
                        <th>{t("components.label")}</th>
                    </tr>
                </thead>
                <tbody>
                    {productionLines &&
                        productionLines.map((productionLine, index) => {
                            return (
                                <MachineDetails
                                    source={source}
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
