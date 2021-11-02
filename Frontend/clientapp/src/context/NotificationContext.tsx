import { createContext, useEffect, useState } from "react";
import { MaintenanceNotification } from "../globalTypes";
import { io, Socket } from "socket.io-client";

interface INotificationContext {
    notifications: MaintenanceNotification[];
    getComponentNotifications: (
        componentId: number
    ) => MaintenanceNotification[];
    removeNotification: (notificationId: number) => void;
}

const defaultState: INotificationContext = {
    notifications: [],
    getComponentNotifications: (componentId: number) => [],
    removeNotification: (notificationId: number) => {},
};

export const NotificationContext = createContext(defaultState);

interface INotificationProvider {
    children?: React.ReactNode;
}

export function NotificationProvider(props: INotificationProvider) {
    const [notifications, setNotifications] = useState(
        defaultState.notifications
    );

    let socket: Socket;

    useEffect(() => {
        socket = io("http://localhost:5300");

        socket.on(
            "Add Notification List",
            (data: MaintenanceNotification[]) => {
                setNotifications(data);
                console.log(data);
            }
        );
    }, []);

    function getComponentNotifications(componentId: number) {
        return notifications.filter((n) => n.ComponentId === componentId);
    }

    function removeNotification(notificationId: number) {
        socket.emit("Add Notification", {
            id: notificationId,
        });
    }

    return (
        <NotificationContext.Provider
            value={{
                notifications,
                getComponentNotifications,
                removeNotification,
            }}
        >
            {props.children}
        </NotificationContext.Provider>
    );
}
