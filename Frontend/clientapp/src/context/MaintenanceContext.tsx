import { createContext, useContext, useEffect, useState } from "react";
import { MaintenanceNotification } from "../globalTypes";
import { SocketContext } from "./SocketContext";

interface IMaintenanceContext {
    maintenance: MaintenanceNotification[];
    getComponentMaintenance: (component_id: number) => MaintenanceNotification[];
    addMaintenance: (component_id: number, description: string) => void;
    finishMaintenance: (maintenanceId: number) => void;
    removeMaintenance: (maintenanceId: number) => void;
}

const defaultState: IMaintenanceContext = {
    maintenance: [],
    getComponentMaintenance: (component_id: number) => [],
    addMaintenance: (component_id: number, description: string) => {},
    finishMaintenance: (maintenanceId: number) => {},
    removeMaintenance: (maintenanceId: number) => {},
};

export const MaintenanceContext = createContext(defaultState);

interface IMaintenanceProvider {
    children?: React.ReactNode;
}

export function MaintenanceProvider(props: IMaintenanceProvider) {
    const [maintenance, setMaintenance] = useState(defaultState.maintenance);

    const { socket } = useContext(SocketContext);

    useEffect(() => {
        socket.on("Add Maintenance List", (data: MaintenanceNotification[]) => {
            setMaintenance(data);
        });

        return () => {
            socket.disconnect();
        };
        // eslint-disable-next-line
    }, []);

    function getComponentMaintenance(componentId: number) {
        return maintenance.filter((n) => n.componentId === componentId);
    }

    function finishMaintenance(maintenanceId: number) {
        socket.emit("Finish Maintenance", {
            maintenanceId,
        });
    }

    function removeMaintenance(maintenanceId: number) {
        socket.emit("Remove Maintenance", { maintenanceId });
    }

    function addMaintenance(component_id: number, description: string) {
        socket.emit("Add Maintenance", {
            componentId: component_id,
            description: description,
        });
    }

    return (
        <MaintenanceContext.Provider
            value={{
                maintenance,
                addMaintenance,
                finishMaintenance,
                getComponentMaintenance,
                removeMaintenance,
            }}
        >
            {props.children}
        </MaintenanceContext.Provider>
    );
}
