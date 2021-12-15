import API from "../Instance";
import { Uptime } from "../../globalTypes";
import { CancelToken } from "axios";

export const getUptimesFromLastDay = async () => {
    let uptimes: Uptime[] = [];
    await API.get<Uptime[]>("uptime/readall").then((res) => {
        uptimes = res.data;
    });
    return uptimes;
};

export const getUptimesFromLastDayById = async (productionLineId: number, cancelToken: CancelToken) => {
    let uptimes: Uptime[] = [];
    await API.get<Uptime[]>("uptime/read/?productionLine_id=" + productionLineId, { cancelToken }).then((res) => {
        uptimes = res.data;
    });
    return uptimes;
};
