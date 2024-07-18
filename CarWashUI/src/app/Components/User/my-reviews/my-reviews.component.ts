import { Component } from '@angular/core';
import { Review } from 'src/app/Models/review.model';
import { AuthService } from 'src/app/Services/auth.service';
import { ReviewsService } from 'src/app/Services/review.service';

@Component({
  selector: 'app-my-reviews',
  templateUrl: './my-reviews.component.html',
  styleUrls: ['./my-reviews.component.css']
})
export class MyReviewsComponent {
  reviews: Review[] = [];
  userId!: number;

  constructor(private reviewsService: ReviewsService, private authService: AuthService) {}

  ngOnInit(): void {
    this.userId = this.authService.getUserId();
    this.loadReviews();
  }

  loadReviews(): void {
    this.reviewsService.getReviewsByUserId(this.userId).subscribe({
      next: (data) => this.reviews = data,
      error: (error) => console.error('There was an error!', error)
    });
  }
}
