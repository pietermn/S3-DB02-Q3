export type Component = {
    Id: number,
    Name: String,
    Type: ComponentType
    Description: String,
    Port: number,
    Board: number,
    TotalActions: number
    History: ProductLineHistory[]
}

export type ProductLineHistory = {
    ProductionLine: ProductionLine,
    StartDate: Date,
    EndDate: Date
}

export type ProductionLine = {
    Id: number,
    Name: String,
    Description: String,
    Active: Boolean,
    Port: number,
    Board: number,
    Machines?: Machine[],
    Components?: Component[]
}

export type ProductSide = {
    Id: number,
    Name: String,
    ProductionLines: ProductionLine[]
}

export type Machine = {
    Id: number,
    Name: String,
    Description: String
}

export enum ComponentType {
    Coldhalf,
    Hothalf,
    Complete
}

export type MaintenanceNotification = {
    component: string,
    maintenance: string
}