import { createContext, useContext, useEffect, useState } from "react";
import { GetProductionLines } from "../api/requests/productionlines";
import { ProductionLine } from "../globalTypes";
import { SocketContext } from "./SocketContext";

interface IUpdaterContext {
    bool: Boolean;
}

const defaultState: IUpdaterContext = {
    bool: false,
};

export const UpdaterContext = createContext(defaultState);

interface IUpdaterProvider {
    children?: React.ReactNode;
}

export function UpdaterProvider(props: IUpdaterProvider) {
    const { socket } = useContext(SocketContext);
    const [bool, setBool] = useState(defaultState.bool);

    useEffect(() => {
        socket.on("Update Components", () => {
            setBool(true);
            setBool(false);
        });
    }, []);

    return <UpdaterContext.Provider value={{ bool }}>{props.children}</UpdaterContext.Provider>;
}
