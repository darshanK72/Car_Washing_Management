import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Payment } from '../Models/payment.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = 'https://localhost:7152/api/Orders';

  constructor(private http: HttpClient) { }

  getAllOrders(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getOrderById(orderId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${orderId}`);
  }

  getUserByOrderId(orderId:number):Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/userOrder/${orderId}`);
  }

  getWasherByOrderId(orderId:number):Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/washer/${orderId}`);
  }
  getOrderByUserId(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/userOrder/${userId}`);
  }

  getReceiptsByUserId(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/user/receipt/${userId}`);
  }

  getLeaderBoard(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/leaderboard`);
  }

  placeOrder(orderData: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/place-order`, orderData);
  }

  updateOrder(id: number, orderData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, orderData);
  }

  deleteOrder(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }

  completePayment(payment:Payment) : Observable<any>{
    return this.http.post<any>(`${this.apiUrl}/complete-payment`,payment);
  }
}