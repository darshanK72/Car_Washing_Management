import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private apiUrl = 'https://localhost:7152/api/Cars';

  constructor(private http: HttpClient) { }

  getAllCars(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getCarByUserId(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/user/${id}`);
  }

  getCarById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  addCar(carData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, carData);
  }

  updateCar(id: number, carData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, carData);
  }

  deleteCar(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}