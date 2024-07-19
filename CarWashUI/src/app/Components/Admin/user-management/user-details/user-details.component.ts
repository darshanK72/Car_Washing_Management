import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Order } from 'src/app/Models/order.model';
import { Review } from 'src/app/Models/review.model';
import { User } from 'src/app/Models/user.model';
import { AdminService } from 'src/app/Services/admin.service';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  user: User | null = null;
  orders: Order[] = [];
  reviews: Review[] = [];

  constructor(private adminService: AdminService, private route: ActivatedRoute,private authService:AuthService) {}

  ngOnInit(): void {
    const userId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadUserDetails(userId);
  }

  loadUserDetails(userId: number): void {
    this.authService.getUserById(userId).subscribe(user => {
      this.user = user;
    });

    this.adminService.getAllUserOrders(userId).subscribe(orders => {
      this.orders = orders;
    });

    this.adminService.getUserReviews(userId).subscribe(reviews => {
      this.reviews = reviews;
    });
  }

}