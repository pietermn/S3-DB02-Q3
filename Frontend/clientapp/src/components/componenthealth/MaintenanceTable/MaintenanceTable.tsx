import { Maintenance } from "../../../globalTypes";
import "./MaintenanceTableStyle.scss";
import { FaCheck as CheckmarkIcon } from "react-icons/fa";

interface IMaintenanceTable {
    maintenance: Maintenance[];
    finishMaintenance: (id: number) => void;
}

export default function MaintenanceTable({ maintenance, finishMaintenance }: IMaintenanceTable) {
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
                                finish now:{" "}
                                <CheckmarkIcon
                                    onClick={() => {
                                        finishMaintenance(maintenance.id);
                                    }}
                                />
                            </p>
                        ) : (
                            <p>{maintenance.timeDone}</p>
                        )}
                    </div>
                );
            })}
        </div>
    );
}
