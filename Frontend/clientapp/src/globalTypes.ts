export type Component = {
    Id: Number,
    Name: String,
    Type: ComponentType
    Description: String,
    Port: Number,
    Board: Number,
    TotalActions: Number
    History: ProductLineHistory[]
}

export type ProductLineHistory = {
    ProductionLine: ProductionLine,
    StartDate: Date,
    EndDate: Date
}

export type ProductionLine = {
    Id: Number,
    Name: String,
    Description: String,
    Active: Boolean,
    Port: Number,
    Board: Number,
    Machines: Machine[],
    Components: Component[]
}

export type ProductSide = {
    Id: Number,
    Name: String,
    ProductionLines: ProductionLine[]
}

export type Machine = {
    Id: Number,
    Name: String,
    Description: String
}

export enum ComponentType {
    Coldhalf,
    Hothalf,
    Complete
}