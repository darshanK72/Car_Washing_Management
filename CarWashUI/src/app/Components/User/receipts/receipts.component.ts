import { Component, OnInit } from '@angular/core';
import { Receipt } from 'src/app/Models/receipt.model';
import { AuthService } from 'src/app/Services/auth.service';
import { OrderService } from 'src/app/Services/order.service';

@Component({
  selector: 'app-receipts',
  templateUrl: './receipts.component.html',
  styleUrls: ['./receipts.component.css']
})
export class ReceiptsComponent implements OnInit {
  receipts: Receipt[] = [];
  userId!:number;

  constructor(private orderService: OrderService,private authService:AuthService) {}

  ngOnInit(): void {
    this.userId = this.authService.getUserId();
    this.loadReceipts();
  }

  loadReceipts(): void {
    this.orderService.getReceiptsByUserId(this.userId).subscribe({
      next: (data) => this.receipts = data,
      error: (error) => console.error('There was an error!', error)
    });
  }
}
