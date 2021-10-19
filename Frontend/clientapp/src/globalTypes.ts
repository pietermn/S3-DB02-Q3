export type Component = {
    id: number,
    name: string,
    type: ComponentType
    description: string,
    port: number,
    board: number,
    totalActions: number
    history: ProductLineHistory[]
}

export type ProductLineHistory = {
    productionLine: ProductionLine,
    startDate: Date,
    endDate: Date
}

export type ProductionLine = {
    id: number,
    name: string,
    description: string,
    active: Boolean,
    port: number,
    board: number,
    machines?: Machine[],
    components?: Component[]
}

export type ProductSide = {
    id: number,
    name: string,
    productionLines: ProductionLine[]
}

export type Machine = {
    id: number,
    name: string,
    description: string
}

export enum ComponentType {
    coldhalf,
    hothalf,
    complete
}

export type MaintenanceNotification = {
    Id: number,
    ComponentId: number
    Component: string,
    Message: string
}

export type Uptime = {
    id: number,
    productionLineId: number,
    begin: Date,
    end: Date,
    active: boolean
}