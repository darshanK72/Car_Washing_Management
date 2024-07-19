import { Order } from "./order.model";
import { Review } from "./review.model";

export interface Washer {
    washerId: number;
    name: string;
    email: string;
    password: string;
    phoneNumber: string;
    role: string;
    profilePicture?: string;
    isActive?: boolean;
    address?: string;
    orders?: Order[];
    reviews?: Review[];
  }
  