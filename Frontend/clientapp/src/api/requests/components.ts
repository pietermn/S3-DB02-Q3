import { Component, Maintenance, MaintenanceNotification } from "../../globalTypes";
import Api from "../Instance";

export const GetComponents = async () => {
    let components: Component[] = [];
    await Api.get<Component[]>("component/readall").then((res) => {
        components = res.data;
    });

    return components;
};

export const GetPreviousActions = async (component_id: number, timespan: string, timespanAmount: string) => {
    let actions: number[] = [];
    await Api.get<number[]>(`component/previousactions/${component_id}/${timespanAmount}/${timespan}`).then((res) => {
        actions = res.data;
    });

    return actions;
};

export const GetAllMaintenance = async (component_id: number) => {
    let maintenance: Maintenance[] = [];

    await Api.get<Maintenance[]>(`maintenance/read?component_id=${component_id}`).then((res) => {
        maintenance = res.data;
    });

    return maintenance.sort((a, b) => new Date(a.timeDone).getTime() - new Date(b.timeDone).getTime());
};
