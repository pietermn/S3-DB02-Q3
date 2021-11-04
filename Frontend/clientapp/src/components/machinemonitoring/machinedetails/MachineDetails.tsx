import { useContext, useEffect, useState } from "react";
import { GrStatusGoodSmall as StatusDot } from "react-icons/gr";
import MachineStatus from "../machinestatus/MachineStatus";
import Modal from "react-modal";
import "./MachineDetails.scss";
import { Component, Uptime } from "../../../globalTypes";
import { useHistory } from "react-router-dom";
import { getUptimesFromLastDayById } from "../../../api/requests/uptime";
import { UpdaterContext } from "../../../context/UpdaterContext";

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

    // function ToComponents(componentId: number) {

    // }
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
                <h1>Production line {props.productionLine}</h1>
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
                    <h2>No components found</h2>
                )}
                <button onClick={() => setShow(false)}>Close</button>
            </Modal>
            <tr className="MM-Data" onClick={() => setShow(true)}>
                <td className={uptime && uptime.length ? (uptime[uptime.length - 1].active ? "Good" : "Bad") : "Bad"}>
                    <StatusDot />
                </td>
                <td>{props.productionLine}</td>
                <td>
                    <MachineStatus name={props.productionLine} uptime={uptime} />
                </td>
                <td>{props.components?.length || 0}</td>
            </tr>
        </>
    );
}
