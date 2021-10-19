import API from '../Instance'
import { Uptime } from '../../globalTypes'

export const getUptimesFromLastDay = async () => {
    let uptimes: Uptime[] = []
    await API.get<Uptime[]>("uptime/readall")
        .then((res) => {
            uptimes = res.data
        })
    return uptimes
}

export const getUptimesFromLastDayById = async (productionLineId: number) => {
    let uptimes: Uptime[] = []
    await API.get<Uptime[]>("uptime/read/?productionLine_id=" + productionLineId)
        .then((res) => {
            uptimes = res.data
        })
    return uptimes
}
