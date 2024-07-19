import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Washer } from '../Models/washer.model';
import { WashRequest } from '../Models/wash-request.model';
import { Order } from '../Models/order.model';

@Injectable({
  providedIn: 'root'
})
export class WasherService {
  private apiUrl = 'https://localhost:7152/api/washers';

  constructor(private http: HttpClient) { }

  getWashers(): Observable<Washer[]> {
    return this.http.get<Washer[]>(this.apiUrl);
  }

  getWasher(id: number): Observable<Washer> {
    return this.http.get<Washer>(`${this.apiUrl}/${id}`);
  }

  createWasher(washer: Washer): Observable<Washer> {
    return this.http.post<Washer>(this.apiUrl, washer);
  }

  updateWasher(id: number, washer: Washer): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, washer);
  }

  deleteWasher(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  getWashingRequests(washerId: number): Observable<WashRequest[]> {
    return this.http.get<WashRequest[]>(`${this.apiUrl}/${washerId}/washing-requests`);
  }

  getWasherOrders(washerId: number): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/${washerId}/orders`);
  }
  

  acceptOrder(orderId: number): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/accept-order/${orderId}`, {});
  }

  rejectOrder(orderId: number): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/reject-order/${orderId}`, {});
  }
}
