import { createContext, useEffect, useState } from "react";
import { io } from "socket.io-client";

interface IUpdaterContext {
    date: Date;
}

const defaultState: IUpdaterContext = {
    date: new Date()
}

export const UpdaterContext = createContext(defaultState);

interface IUpdaterProvider {
    children?: React.ReactNode;
}

export function UpdaterProvider(props: IUpdaterProvider) {
    const [date, setDate] = useState(defaultState.date)

    useEffect(() => {
        const socket = io("http://localhost:5300")

        socket.on("New Current Date", (data: Date) => {
            setDate(data)
        })
    }, [])

    return (
        <UpdaterContext.Provider
            value={{ date }}>
            {props.children}
        </UpdaterContext.Provider>
    )
}