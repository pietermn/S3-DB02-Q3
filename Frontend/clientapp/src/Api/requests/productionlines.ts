import { Component, ProductionLine } from '../../globalTypes';
import Api from '../Instance';

export const GetProductionLines = async () => {
    let productionLines: ProductionLine[] = [];
    await Api.get<ProductionLine[]>("productionline/readall")
    .then((res) => {
        productionLines = res.data;
    })
    
    return productionLines;
}