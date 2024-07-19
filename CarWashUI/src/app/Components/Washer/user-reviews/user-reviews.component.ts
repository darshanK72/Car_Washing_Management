import { Component } from '@angular/core';
import { Review } from 'src/app/Models/review.model';
import { AuthService } from 'src/app/Services/auth.service';
import { ReviewsService } from 'src/app/Services/review.service';

@Component({
  selector: 'app-user-reviews',
  templateUrl: './user-reviews.component.html',
  styleUrls: ['./user-reviews.component.css']
})
export class UserReviewsComponent {
  reviews: Review[] = [];
  washerId!: number;

  constructor(private reviewsService: ReviewsService, private authService: AuthService) {}

  ngOnInit(): void {
    let washer = this.authService.getUserRoleAndId();
    this.washerId = washer?.WasherId;
    this.loadReviews();
  }

  loadReviews(): void {
    this.reviewsService.getReviewsByWasherId(this.washerId).subscribe({
      next: (data) => this.reviews = data,
      error: (error) => console.error('There was an error!', error)
    });
  }
}
