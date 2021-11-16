export type Component = {
  id: number;
  name: string;
  type: ComponentType;
  description: string;
  port: number;
  board: number;
  totalActions: number;
  history: ProductLineHistory[];
  maxActions: number;
  currentActions: number;
  percentageMaintenance: number;
};

export type ProductLineHistory = {
  productionLine: ProductionLine;
  startDate: Date;
  endDate: Date;
};

export type ProductionLine = {
  id: number;
  name: string;
  description: string;
  active: Boolean;
  port: number;
  board: number;
  machines?: Machine[];
  components?: Component[];
};

export type ProductSide = {
  id: number;
  name: string;
  productionLines: ProductionLine[];
};

export type Machine = {
  id: number;
  name: string;
  description: string;
};

export enum ComponentType {
  coldhalf,
  hothalf,
  complete,
}

export type MaintenanceNotification = {
  id: number;
  componentId: number;
  component: string;
  description: string;
};

export type Uptime = {
  id: number;
  productionLineId: number;
  begin: Date;
  end: Date;
  active: boolean;
};
