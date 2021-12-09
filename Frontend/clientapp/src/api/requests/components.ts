import axios, { Canceler, CancelTokenSource } from "axios";
import { Component, Maintenance, ProductionDate } from "../../globalTypes";
import Api from "../Instance";

const CancelToken = axios.CancelToken;
let cancel: Canceler;

export const GetComponents = async () => {
    let components: Component[] = [];
    await Api.get<Component[]>("component/readall").then((res) => {
        components = res.data;
    });

    return components;
};

export const GetPreviousActions = async (component_id: number, beginDate: string, endDate: string) => {
    let actions: ProductionDate[] = [];

    // if (cancel !== undefined) {
    //     cancel();
    // }

    await Api.get<ProductionDate[]>(`component/previousactions/${component_id}/${beginDate}/${endDate}`, {
        // cancelToken: new CancelToken(function executor(c) {
        //     cancel = c;
        // }),
    })
        .then((res) => {
            actions = res.data;
        })
        .catch(function (thrown) {
            console.log(thrown);
        });
    return actions;
};

export const GetAllMaintenance = async (component_id: number) => {
    let maintenance: Maintenance[] = [];

    await Api.get<Maintenance[]>(`maintenance/read?component_id=${component_id}`).then((res) => {
        maintenance = res.data;
    });

    if (maintenance) {
        return maintenance.sort((a, b) => new Date(a.timeDone).getTime() - new Date(b.timeDone).getTime());
    }
    return maintenance;
};
