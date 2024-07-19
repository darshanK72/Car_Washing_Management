import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WashRequest } from 'src/app/Models/wash-request.model';
import { AuthService } from 'src/app/Services/auth.service';
import { WasherService } from 'src/app/Services/washer.service';

@Component({
  selector: 'app-wash-request',
  templateUrl: './wash-request.component.html',
  styleUrls: ['./wash-request.component.css']
})
export class WashRequestComponent implements OnInit {
  washRequests: WashRequest[] = [];
  washerId!: number;
  isThereWashRequests: boolean = false;

  constructor(
    private washRequestService: WasherService,
    private route: ActivatedRoute,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    let washer = this.authService.getUserRoleAndId();
    this.washerId = washer?.WasherId;
    this.loadWashRequests();
  }

  loadWashRequests(): void {
    this.washRequestService.getWashingRequests(this.washerId).subscribe(
      (requests: WashRequest[]) => {
        this.washRequests = requests;
        this.isThereWashRequests = requests.length > 0;
      },
      error => console.error('Error fetching wash requests:', error)
    );
  }

  acceptOrder(orderId: number): void {
    this.washRequestService.acceptOrder(orderId).subscribe(
      response => {
        window.alert('Order Accepted');
        this.updateRequestStatus(orderId, 'accepted');
      },
      error => console.error('Error accepting order:', error)
    );
  }

  rejectOrder(orderId: number): void {
    this.washRequestService.rejectOrder(orderId).subscribe(
      response => {
        window.alert('Order Rejected');
        this.updateRequestStatus(orderId, 'rejected');
      },
      error => console.error('Error rejecting order:', error)
    );
  }

  private updateRequestStatus(orderId: number, status: string): void {
    // Update the status of the specific request in the array
    const request = this.washRequests.find(req => req.orderId === orderId);
    if (request) {
      request.status = status;
    }
  }
}
