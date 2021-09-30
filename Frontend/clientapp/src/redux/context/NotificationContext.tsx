import { createContext, useState } from "react";
import { MaintenanceNotification } from "../../globalTypes";

interface INotificationContext {
    addNotification: (component: string, maintenance: string) => void,
    removeNotification: (component: string) => void,
    notifications: MaintenanceNotification[]
}

const defaultState: INotificationContext = {
    addNotification: () => { },
    removeNotification: () => { },
    notifications: []
}

export const NotificationContext = createContext(defaultState)

interface INotificationProvider {
    children?: React.ReactNode
}

export function NotificationProvider(props: INotificationProvider) {
    const [notifications, setNotifications] = useState(defaultState.notifications)

    function addNotification(component: string, maintenance: string) {
        setNotifications([...notifications, { component, maintenance }])
    }

    function removeNotification(component: string) {
        let notis = [...notifications]
        notis.splice(notis.findIndex(n => n.component === component) - 1)
        setNotifications(notis)
    }

    return (
        <NotificationContext.Provider value={{notifications, addNotification, removeNotification}}>
            {props.children}
        </NotificationContext.Provider>
    )
}