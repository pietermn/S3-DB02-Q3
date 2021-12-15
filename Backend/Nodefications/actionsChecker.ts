import axios, { AxiosResponse } from "axios";
import { Component } from "./types";
import sql from "./sql";

let connectionString = process.env.api_url ? process.env.api_url : "https://localhost:5001";
export default class ActionsChecker {
    static componentNeedsNotification = async (componentId: number) => {
        const res: AxiosResponse<Component> = await axios.get(
            `${connectionString}/component/read?component_id=` + componentId
        );
        const component = res.data;

        return component.percentageMaintenance >= 100;
    };

    static allComponentsNeedNotification = async () => {
        // const notifications = await sql.getNotifications();
        const components = await sql.getAllComponents();

        components.forEach(async (c) => {
            if (c.currentActions >= c.maxActions && c.maxActions != 1) {
                if (await sql.componentHasNoNotification(c.id)) {
                    sql.addNotification(c.id, "");
                    axios
                        .post(`http://q3-sms:5100/telnyx/create`, {
                            to: "+31638458923",
                            text: `Component: ${c.description} (Id: ${c.id}) reached its max uses`,
                        })
                        .catch(() => {
                            console.log("Sms backend down");
                        });
                }
            } catch {
                console.log("Sms backend is down");
            }
        });
    };
}
