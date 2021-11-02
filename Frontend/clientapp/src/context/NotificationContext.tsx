import { createContext, useEffect, useState } from "react";
import { MaintenanceNotification } from "../globalTypes";
import { io, Socket } from "socket.io-client";

interface INotificationContext {
  notifications: MaintenanceNotification[];
  getComponentNotifications: (componentId: number) => MaintenanceNotification[];
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

  const socket: Socket = io("http://localhost:5300");

  useEffect(() => {
    socket.on("Add Notification List", (data: MaintenanceNotification[]) => {
      setNotifications(data);
    });

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
