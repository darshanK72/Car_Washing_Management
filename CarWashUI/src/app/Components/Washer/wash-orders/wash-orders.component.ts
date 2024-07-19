import { Component, OnInit } from '@angular/core';
import { WashRequest } from 'src/app/Models/wash-request.model';
import { AuthService } from 'src/app/Services/auth.service';
import { WasherService } from 'src/app/Services/washer.service';

@Component({
  selector: 'app-wash-orders',
  templateUrl: './wash-orders.component.html',
  styleUrls: ['./wash-orders.component.css']
})
export class WashOrdersComponent implements OnInit {
  completedOrders: WashRequest[] = [];
  washerId!: number;

  constructor(private washerService: WasherService,private authService:AuthService) { }

  ngOnInit(): void {
    let washer = this.authService.getUserRoleAndId();
    this.washerId = washer?.WasherId;
    this.loadCompletedOrders();
  }

  loadCompletedOrders(): void {
    this.washerService.getWasherOrders(this.washerId).subscribe(
      (orders: WashRequest[]) => {
        this.completedOrders = orders;
      },
      error => console.error('Error fetching completed orders:', error)
    );
  }
}
