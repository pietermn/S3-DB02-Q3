import axios, { AxiosResponse } from "axios";
import { Component } from "./types";
import sql from "./sql";

export default class ActionsChecker {
    static componentNeedsNotification = async (componentId: number) => {
        const res: AxiosResponse<Component> = await axios.get(
            "https://localhost:5001/component/read?component_id=" + componentId
        );
        const component = res.data;

        return component.percentageMaintenance >= 100;
    };

    static allComponentsNeedNotification = async () => {
        const notifications = await sql.getNotifications();

        notifications.forEach(async (n) => {
            if (!(await this.componentNeedsNotification(n.componentId))) {
                sql.removeNotification(n.id);
            }
        });
    };
}
