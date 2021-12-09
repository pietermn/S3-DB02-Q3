import { createContext } from "react";
import { io, Socket } from "socket.io-client";

interface ISocketContext {
    socket: Socket;
}

const defaultState: ISocketContext = {
    socket: io(`http://localhost:5300`),
};

export const SocketContext = createContext(defaultState);

interface ISocketProvider {
    children?: React.ReactNode;
}

export function SocketProvider(props: ISocketProvider) {
    const socket = defaultState.socket;

    return (
        <SocketContext.Provider
            value={{
                socket,
            }}
        >
            {props.children}
        </SocketContext.Provider>
    );
}
