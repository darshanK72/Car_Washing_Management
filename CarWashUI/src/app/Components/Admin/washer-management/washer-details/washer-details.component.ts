import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Order } from 'src/app/Models/order.model';
import { Review } from 'src/app/Models/review.model';
import { Washer } from 'src/app/Models/washer.model';
import { AdminService } from 'src/app/Services/admin.service';
import { WasherService } from 'src/app/Services/washer.service';

@Component({
  selector: 'app-washer-details',
  templateUrl: './washer-details.component.html',
  styleUrls: ['./washer-details.component.css']
})
export class WasherDetailsComponent implements OnInit {
  washer: Washer | null = null;
  orders: Order[] = [];
  reviews: Review[] = [];

  constructor(private adminService: AdminService, private route: ActivatedRoute,private washerService:WasherService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const washerId = Number(params.get('id'));
      this.loadWasherDetails(washerId);
    });
  }

  loadWasherDetails(washerId: number): void {
    this.washerService.getWasher(washerId).subscribe(washer => {
      this.washer = washer;
      console.log(this.washer);
    });

    this.adminService.getAllWasherOrders(washerId).subscribe(orders => {
      this.orders = orders;
    });

    this.adminService.getWasherReviews(washerId).subscribe(reviews => {
      this.reviews = reviews;
    });
  }
}
