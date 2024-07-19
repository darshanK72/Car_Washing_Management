import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Order } from 'src/app/Models/order.model';
import { Payment } from 'src/app/Models/payment.model';
import { Review } from 'src/app/Models/review.model';
import { AuthService } from 'src/app/Services/auth.service';
import { OrderService } from 'src/app/Services/order.service';
import { ReviewsService } from 'src/app/Services/review.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {
  orders: Order[] = [];
  selectedOrder: Order | null = null;
  showReviewForm: boolean = false;
  showPaymentForm:boolean = false;
  paymentForm: FormGroup;
  reviewForm: FormGroup;
  userId!: number;
  paymentMethods: string[] = ['Credit Card', 'Debit Card', 'PayPal'];

  constructor(
    private orderService: OrderService,
    private fb: FormBuilder,
    private authService: AuthService,
    private reviewService: ReviewsService
  ) {
    this.paymentForm = this.fb.group({
      totalPrice: [{ value: '', disabled: true }, Validators.required],
      paymentTime: ['', Validators.required],
      paymentType: ['', [Validators.required, Validators.maxLength(100)]],
      orderId: ['', Validators.required],
      userId: ['', Validators.required],
      receiptId: ['', Validators.required],
      status: ['']
    });

    this.reviewForm = this.fb.group({
      rating: ['', [Validators.required, Validators.min(1), Validators.max(5)]],
      comment: ['', [Validators.required, Validators.maxLength(500)]],
      receiptId: ['', Validators.required],
      userId: ['', Validators.required],
      orderId:['',Validators.required]
    });
  }

  ngOnInit(): void {
    this.userId = this.authService.getUserId();
    this.loadOrders();
  }

  loadOrders(): void {
    this.orderService.getOrderByUserId(this.userId).subscribe({
      next: (data) => {
        this.orders = data;
        console.log(data);
      },
      error: (error) => console.error('There was an error!', error)
    });
  }

  onPay(order: Order): void {
    this.selectedOrder = order;
    this.showPaymentForm = true;
    this.showReviewForm = false;
    this.paymentForm.setValue({
      totalPrice: order.totalPrice,
      paymentTime: '',
      paymentType: '',
      orderId: order.orderId,
      userId: order.userId,
      status: order.status,
      receiptId: order.receiptId
    });
  }

  onSubmitPayment(): void {
    if (this.paymentForm.valid) {
      const payment: Payment = {
        paymentId: 0,
        totalAmount: this.selectedOrder!.totalPrice,
        paymentTime: this.paymentForm.value.paymentTime,
        paymentType: this.paymentForm.value.paymentType,
        userId: this.selectedOrder!.userId,
        receiptId: this.selectedOrder?.receiptId,
        orderId: this.selectedOrder?.orderId
      };

      this.orderService.completePayment(payment).subscribe(() => {
        this.loadOrders();
        this.selectedOrder = null;
        this.paymentForm.reset();
      });
    } else {
      this.paymentForm.markAllAsTouched();
    }
  }

  onRate(order: Order): void {
    this.selectedOrder = order;
    this.showReviewForm = true;
    this.showPaymentForm = false;
    this.reviewForm.setValue({
      rating: '',
      comment: '',
      receiptId: order.receiptId,
      userId: order.userId,
      orderId:order.orderId
    });
  }

  onSubmitReview(): void {
    if (this.reviewForm.valid) {
      const review: Review = {
        reviewId: 0,
        rating: this.reviewForm.value.rating,
        comment: this.reviewForm.value.comment,
        receiptId: this.reviewForm.value.receiptId,
        userId: this.reviewForm.value.userId,
        orderId:this.reviewForm.value.orderId
      };

      this.reviewService.addReview(review).subscribe(() => {
        this.loadOrders();
        this.selectedOrder = null;
        this.showReviewForm = false;
        this.reviewForm.reset();
      });
    } else {
      this.reviewForm.markAllAsTouched();
    }
  }

  onCancel(): void {
    this.selectedOrder = null;
    this.paymentForm.reset();
  }

  onCancelReview(): void {
    this.selectedOrder = null;
    this.showReviewForm = false;
    this.reviewForm.reset();
  }
}
