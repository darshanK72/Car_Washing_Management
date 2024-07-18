import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Review } from '../Models/review.model';


@Injectable({
  providedIn: 'root'
})
export class ReviewsService {
  private apiUrl = 'https://localhost:7152/api/Reviews';

  constructor(private http: HttpClient) { }

  getAllReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(this.apiUrl);
  }

  getReviewById(id: number): Observable<Review> {
    return this.http.get<Review>(`${this.apiUrl}/${id}`);
  }

  getReviewsByUserId(userId: number): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiUrl}/user/${userId}`);
  }

  getReviewsByWasherId(washerId: number): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiUrl}/washer/${washerId}`);
  }

  addReview(review: Review): Observable<Review> {
    return this.http.post<Review>(this.apiUrl, review, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  updateReview(id: number, review: Review): Observable<Review> {
    return this.http.put<Review>(`${this.apiUrl}/${id}`, review, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  deleteReview(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}
