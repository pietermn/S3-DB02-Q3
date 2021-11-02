import { createContext, useEffect, useState } from "react";
import { MaintenanceNotification } from "../globalTypes";
import { io, Socket } from "socket.io-client";
const socket = io("http://localhost:5300");

interface INotificationContext {
    notifications: MaintenanceNotification[];
    getComponentNotifications: (
        componentId: number
    ) => MaintenanceNotification[];
    removeNotification: (notificationId: number) => void;
    setMaxActions: (componentId: number, maxActions: number) => void;
}

const defaultState: INotificationContext = {
    notifications: [],
    getComponentNotifications: (componentId: number) => [],
    removeNotification: (notificationId: number) => {},
    setMaxActions: (componentId: number, maxActions: number) => {},
};

export const NotificationContext = createContext(defaultState);

interface INotificationProvider {
    children?: React.ReactNode;
}

export function NotificationProvider(props: INotificationProvider) {
    const [notifications, setNotifications] = useState(
        defaultState.notifications
    );

    useEffect(() => {
        socket.on(
            "Add Notification List",
            (data: MaintenanceNotification[]) => {
                setNotifications(data);
            }
        );

        return () => {
            socket.disconnect();
        };
    }, []);

    function getComponentNotifications(componentId: number) {
        return notifications.filter((n) => n.ComponentId === componentId);
    }

    function removeNotification(notificationId: number) {
        socket.emit("Remove Notification", {
            id: notificationId,
        });
    }

    function setMaxActions(componentId: number, maxActions: number) {
        socket.emit("Set Max Actions", {
            componentId,
            maxActions,
        });
    }

    return (
        <NotificationContext.Provider
            value={{
                notifications,
                getComponentNotifications,
                removeNotification,
                setMaxActions,
            }}
        >
            {props.children}
        </NotificationContext.Provider>
    );
}
