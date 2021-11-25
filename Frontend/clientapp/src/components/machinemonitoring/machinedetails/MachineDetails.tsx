import { useContext, useEffect, useState } from "react";
import { FaTimes as OfflineIcon, FaCog as OnlineIcon } from "react-icons/fa";
import MachineStatus from "../machinestatus/MachineStatus";
import Modal from "react-modal";
import "./MachineDetails.scss";
import { Component, Uptime } from "../../../globalTypes";
import { useHistory } from "react-router-dom";
import { getUptimesFromLastDayById } from "../../../api/requests/uptime";
import { UpdaterContext } from "../../../context/UpdaterContext";
import { useTranslation } from "react-i18next";
import { Tooltip } from "@material-ui/core";

interface IMachineDetails {
    id: number;
    status: boolean;
    productionLine: string;
    product: string;
    components?: Component[];
}

export default function MachineDetails(props: IMachineDetails) {
    const { bool } = useContext(UpdaterContext);
    const [show, setShow] = useState(false);
    const [uptime, setUptime] = useState<Uptime[]>([]);
    const history = useHistory();
    const { t } = useTranslation();

    if (bool) {
        getUptime();
    }

    async function getUptime() {
        setUptime(await getUptimesFromLastDayById(props.id));
    }

    useEffect(() => {
        async function getData() {
            await getUptime();
        }

        getData();
    }, [props.id]);

    return (
        <>
            <Modal
                className="MM-Modal"
                isOpen={show}
                onRequestClose={() => setShow(false)}
                contentLabel={props.productionLine}
                shouldCloseOnOverlayClick={true}
                ariaHideApp={false}
            >
                <h1>
                    {t("productionline.label")} {props.productionLine}
                </h1>
                {props.components && props.components.length ? (
                    props.components.map((component, index) => {
                        return (
                            <h2
                                className="redirect-component"
                                key={index}
                                onClick={() =>
                                    history.push({
                                        pathname: "/chealth",
                                        state: { componentId: component.id },
                                    })
                                }
                            >
                                {component.description}
                            </h2>
                        );
                    })
                ) : (
                    <h2>{t("nocomponentsfound.label")}</h2>
                )}
                <button onClick={() => setShow(false)}>{t("close.label")}</button>
            </Modal>
            <tr className="MM-Data" onClick={() => setShow(true)}>
                <td
                    className={uptime && uptime.length ? (uptime[uptime.length - 1].active ? "Good" : "Bad") : "Bad"}
                    style={{ fontSize: "2rem" }}
                >
                    {uptime && uptime.length ? (
                        uptime[uptime.length - 1].active ? (
                            <OnlineIcon className="IconSpin" />
                        ) : (
                            <OfflineIcon />
                        )
                    ) : (
                        <OfflineIcon />
                    )}
                </td>
                <td>{props.productionLine}</td>
                <td>
                    <MachineStatus name={props.productionLine} uptime={uptime} />
                </td>
                <td>
                    {props.components?.length ? (
                        props.components.length === 1 ? (
                            props.components[0].description.length > 10 ? (
                                <Tooltip title={props.components[0].description}>
                                    <div>{props.components[0].description.substr(0, 9)}...</div>
                                </Tooltip>
                            ) : (
                                props.components[0].description
                            )
                        ) : (
                            props.components.length
                        )
                    ) : (
                        0
                    )}
                </td>
            </tr>
        </>
    );
}
