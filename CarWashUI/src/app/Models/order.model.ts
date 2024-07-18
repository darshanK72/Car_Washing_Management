export interface Order {
    orderId: number;
    status: string;
    scheduledDate: Date;
    actualWashDate: Date;
    totalPrice: number;
    notes: string;
    userId: number;
    washerId: number;
    carId: number;
    receiptId?: number;
    packageId: number;
    reviewId?:number;
  }