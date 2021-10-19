import { createContext, useEffect, useState } from "react";
import { MaintenanceNotification } from "../globalTypes";
import { io } from "socket.io-client";

interface INotificationContext {
  notifications: MaintenanceNotification[];
}

const defaultState: INotificationContext = {
  notifications: [],
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
    const socket = io("http://localhost:5300");

    socket.on("Add Notification List", (data: MaintenanceNotification[]) => {
        setNotifications(data)
        console.log(data)
    });

  }, []);

  return (
    <NotificationContext.Provider
      value={{ notifications }}
    >
      {props.children}
    </NotificationContext.Provider>
  );
}
