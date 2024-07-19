import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/Models/order.model';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.css']
})
export class OrderManagementComponent implements OnInit{
  selectedStatus: string = '';
  orders: any[] = [];

  constructor(private adminService: AdminService) { } 

  ngOnInit(): void {
    this.loadOrders();
  }

  onStatusChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    this.selectedStatus = target.value;
    this.filterOrdersByStatus(this.selectedStatus);
  }

  loadOrders(): void {
    this.adminService.getAllOrders().subscribe(
      orders => this.orders = orders,
      error => console.error('Error loading orders', error)
    );
  }

  filterOrdersByStatus(status: string): void {
    this.adminService.getFilteredOrders(status).subscribe(
      orders => this.orders = orders,
      error => console.error('Error filtering orders', error)
    );
  }
}
