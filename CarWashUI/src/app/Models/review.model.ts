export interface Review {
    reviewId: number;
    userId: number;
    rating: number;
    comment: string;
    receiptId: number;
    orderId:number;
    washerId?:number;
  }
  