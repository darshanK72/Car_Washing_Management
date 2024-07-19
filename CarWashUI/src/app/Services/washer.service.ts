import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Washer } from '../Models/washer.model';

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
}
