import { Component } from '../../globalTypes';
import Api from '../Instance';

export const GetComponents = async () => {
    let components: Component[] = [];
    await Api.get<Component[]>("component/readall")
    .then((res) => {
        components = res.data;
    })
    
    return components;
}

export const GetPreviousActions = async (component_id: number, timespan: string, timespanAmount: string) => {
    let actions: number[] = [];
    await Api.get<number[]>(`component/previousactions/${component_id}/${timespanAmount}/${timespan}`)
    .then((res) => {
        actions = res.data;
    })

    return actions;
}