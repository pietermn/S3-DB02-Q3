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
        // const notifications = await sql.getNotifications();
        const components = await sql.getAllComponents();

        components.forEach(async (c) => {
            // if (!(await this.componentNeedsNotification(n.componentId))) {
            //     sql.removeNotification(n.id);
            // }

            if (c.currentActions >= c.maxActions && c.maxActions != 1) {
                if (await sql.componentHasNoNotification(c.id)) {
                    sql.addNotification(c.id, "");
                }
            } else {
                sql.removeNotificationFromComponent(c.id);
            }
        });
    };
}
