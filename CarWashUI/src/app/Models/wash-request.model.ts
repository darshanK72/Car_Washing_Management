import { User } from "./user.model";

export interface WashRequest {
    orderId: number;
    status: string;
    scheduledDate: Date;
    actualWashDate: Date;
    totalPrice: number;
    notes: string;
    userId: number;
    user?: User,
    address?:string;
  }