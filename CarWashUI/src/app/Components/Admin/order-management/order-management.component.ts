import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Order } from 'src/app/Models/order.model';
import { AdminService } from 'src/app/Services/admin.service';
import { WasherService } from 'src/app/Services/washer.service';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.css']
})
export class OrderManagementComponent implements OnInit{
  selectedStatus: string = '';
  orders: Order[] = [];
  assignForm: FormGroup;
  washers: any[] = [];
  selectedOrderId!:number;
  showAssignForm: boolean = false;


  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private washerService: WasherService,
    private route:ActivatedRoute,
    private router:Router
  ) {
    this.assignForm = this.fb.group({
      washerId: [null, Validators.required]
    });
  }


  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.selectedOrderId = Number(params.get('id'));
    });
    this.loadWashers();
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

  assignWasherToOrder(orderId:any){
    this.showAssignForm = true;
    this.selectedOrderId = orderId;
  }

  loadWashers() {
    this.washerService.getWashers().subscribe(data => {
      this.washers = data;
    });
  }

  closeAssignForm() {
    this.showAssignForm = false;
    this.assignForm.reset();
  }

  assignWasher() {
    if (this.assignForm.valid) {
      this.adminService.assignWasherToOrder(this.selectedOrderId, this.assignForm.value.washerId).subscribe(() => {
        this.closeAssignForm();
        this.ngOnInit();
        this.router.navigate(['order-management']);
      });
    }
  }
}
