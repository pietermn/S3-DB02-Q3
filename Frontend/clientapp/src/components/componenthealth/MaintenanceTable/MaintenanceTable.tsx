import { Maintenance } from "../../../globalTypes";
import "./MaintenanceTableStyle.scss";
import { FaCheck as CheckmarkIcon } from "react-icons/fa";

interface IMaintenanceTable {
    maintenance: Maintenance[];
    finishMaintenance: (id: number) => void;
}

export default function MaintenanceTable({ maintenance, finishMaintenance }: IMaintenanceTable) {
    function dateInput(date: Date) {
        const d = new Date(date);
        const dayZero = d.getDate() < 10 ? true : false;
        const monthZero = d.getMonth() + 1 < 10 ? true : false;
        const hourZero = d.getHours() < 10 ? true : false;
        const minuteZero = d.getMinutes() < 10 ? true : false;
        const secondZero = d.getSeconds() < 10 ? true : false;

        return (
            d.getFullYear() +
            "-" +
            (monthZero ? "0" : "") +
            (d.getMonth() + 1) +
            "-" +
            (dayZero ? "0" : "") +
            d.getDate() +
            " " +
            (hourZero ? "0" : "") +
            d.getHours() +
            ":" +
            (minuteZero ? "0" : "") +
            d.getMinutes() +
            ":" +
            (secondZero ? "0" : "") +
            d.getSeconds()
        );
    }

    return (
        <div className="MaintenanceTable">
            <div className="row">
                <p>Maintenance description</p>
                <p>Time finished</p>
            </div>
            {maintenance.map((maintenance) => {
                return (
                    <div key={maintenance.id} className="row">
                        <p>{maintenance.description}</p>
                        {maintenance.timeDone.toLocaleString() === "0001-01-01 00:00:00" ? (
                            <p>
                                finish now:
                                <CheckmarkIcon
                                    onClick={() => {
                                        finishMaintenance(maintenance.id);
                                    }}
                                />
                            </p>
                        ) : (
                            <p>{dateInput(maintenance.timeDone)}</p>
                        )}
                    </div>
                );
            })}
        </div>
    );
}
