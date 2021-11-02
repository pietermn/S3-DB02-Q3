import axios, { AxiosResponse } from "axios"
import { Component } from "./types"

export default class ActionsChecker {
    static componentNeedsNotification = async (componentId: number) => {
        const res: AxiosResponse<Component> = await axios.get("https://localhost:5001/component/read?component_id=" + componentId);
        const component = res.data;

        return component.percentageMaintenance > 100
    }
}