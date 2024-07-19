import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Payment } from '../Models/payment.model';
import { Receipt } from '../Models/receipt.model';
import { Review } from '../Models/review.model';
import { Report } from '../Models/report.model';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private apiUrl = 'https://localhost:7152/api/Admins';

  constructor(private http: HttpClient) {}

  getAllOrders(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/orders`);
  }

  generateReport(
    orderNumber?: string,
    washerName?: string,
    type?: string,
    service?: string,
    startDate?: Date,
    endDate?: Date
  ): Observable<string> {
    let params = new HttpParams();
    if (orderNumber) params = params.set('orderNumber', orderNumber);
    if (washerName) params = params.set('washerName', washerName);
    if (type) params = params.set('type', type);
    if (service) params = params.set('service', service);
    if (startDate) params = params.set('startDate', startDate.toISOString());
    if (endDate) params = params.set('endDate', endDate.toISOString());

    return this.http.get(`${this.apiUrl}/report`, {
      params,
      responseType: 'text',
    });
  }

  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/users`);
  }

  updateUserStatus(userId: number, isActive: boolean): Observable<any> {
    return this.http.put(`${this.apiUrl}/Users/${userId}/status`, isActive);
  }

  getWashers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/washers`);
  }

  addWasher(washer: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/washers`, washer);
  }

  updateWasher(washerId: number, washer: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/washers/${washerId}`, washer);
  }

  updateWasherStatus(washerId: number, isActive: boolean): Observable<any> {
    return this.http.put(`${this.apiUrl}/washers/${washerId}/status`, isActive);
  }

  getWasherReviews(washerId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/washers/${washerId}/reviews`);
  }

  getUserReviews(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}/reviews`);
  }

  exportWasherReport(washerId: number): Observable<string> {
    return this.http.get(`${this.apiUrl}/washers/${washerId}/report`, {
      responseType: 'text',
    });
  }

  getFilteredOrders(status: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/orders/filtered`, {
      params: { status },
    });
  }

  getAllUserOrders(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}/orders`);
  }

  getAllWasherOrders(washerId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/washer/${washerId}/orders`);
  }

  getPaymentDetails(orderId: number): Observable<Payment> {
    return this.http.get<Payment>(`${this.apiUrl}/orders/${orderId}/payment`);
  }

  getReceiptDetails(orderId: number): Observable<Receipt> {
    return this.http.get<Receipt>(`${this.apiUrl}/orders/${orderId}/receipt`);
  }

  getReviewDetails(orderId: number): Observable<Review> {
    return this.http.get<Review>(`${this.apiUrl}/orders/${orderId}/review`);
  }

  getLeaderboard(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/leaderboard`);
  }

  getReport(startDate: Date, endDate: Date): Observable<Report> {
    let params = new HttpParams()
      .set('startDate', startDate.toISOString())
      .set('endDate', endDate.toISOString());

    return this.http.get<Report>(`${this.apiUrl}/report`, { params });
  }

  assignWasherToOrder(orderId: number, washerId: number): Observable<void> {
    return this.http.put<void>(
      `${this.apiUrl}/orders/${orderId}/assign-washer/${washerId}`,
      {}
    );
  }
}
