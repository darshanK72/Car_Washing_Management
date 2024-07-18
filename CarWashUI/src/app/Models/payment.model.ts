export interface Payment {
    paymentId: number;
    totalAmount: number;
    paymentTime: Date;
    paymentType: string;
    receiptId?: number;
    userId?:number;
    orderId?:number;
  }