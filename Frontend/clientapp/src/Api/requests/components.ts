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