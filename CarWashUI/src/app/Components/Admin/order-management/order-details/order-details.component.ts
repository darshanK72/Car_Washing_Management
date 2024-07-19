import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Order } from 'src/app/Models/order.model';
import { Payment } from 'src/app/Models/payment.model';
import { Receipt } from 'src/app/Models/receipt.model';
import { Review } from 'src/app/Models/review.model';
import { AdminService } from 'src/app/Services/admin.service';
import { OrderService } from 'src/app/Services/order.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit{
  order: Order | null = null;
  payment: Payment | null = null;
  receipt: Receipt | null = null;
  review: Review | null = null;

  constructor(private adminService: AdminService, private route: ActivatedRoute,private orderService:OrderService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const orderId = Number(params.get('id'));
      this.loadOrderDetails(orderId);
    });
  }

  loadOrderDetails(orderId: number): void {
    this.orderService.getOrderById(orderId).subscribe(order => {
      this.order = order;
    });

    this.adminService.getPaymentDetails(orderId).subscribe(payment => {
      this.payment = payment;
    });

    this.adminService.getReceiptDetails(orderId).subscribe(receipt => {
      this.receipt = receipt;
    });

    this.adminService.getReviewDetails(orderId).subscribe(review => {
      this.review = review;
    });
  }
}
