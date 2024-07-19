import { Order } from './order.model';

export interface Report {
  orderReport: OrderReport;
  washersReport: WashersReport;
}

export interface OrderReport {
  totalOrders: string;
  totalRevenue: number;
  orders: Order[];
  generatedDate: Date;
}

export interface WashersReport {
  totalWashersRevenue: number;
  washerReports: IndividualWasherReport[];
  generatedDate: Date;
}

export interface IndividualWasherReport {
  washerId: number;
  totalOrders: number;
  totalRevenue: number;
}
