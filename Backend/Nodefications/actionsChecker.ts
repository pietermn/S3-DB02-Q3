import axios, { AxiosResponse } from "axios";
import { Component } from "./types";
import sql from "./sql";
require("dotenv").config({ path: "../../.env" });

console.log(process.env.telnyx_api_url);

let connectionString = process.env.api_url ? process.env.api_url : "https://localhost:5001";
export default class ActionsChecker {
    static componentNeedsNotification = async (componentId: number) => {
        const res: AxiosResponse<Component> = await axios
            .get(`${connectionString}/component/read?component_id=` + componentId)
            .catch(() => {
                throw new Error("Backend is down");
            });
        const component = res.data;

        return component.percentageMaintenance >= 100;
    };

    static allComponentsNeedNotification = async () => {
        const components = await sql.getAllComponents();

        components.forEach(async (c) => {
            if (c.currentActions >= c.maxActions && c.maxActions != 1) {
                if (await sql.componentHasNoNotification(c.id)) {
                    axios
                        .post(`${process.env.telnyx_api_url}/telnyx/create`, {
                            to: "+31627909540",
                            text: `Component: ${c.description} (Id: ${c.id}) reached its max uses`,
                        })
                        .catch(() => {
                            console.log("Sms backend down");
                        });
                }
            }
        });
    };
}
