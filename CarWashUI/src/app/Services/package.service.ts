import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PackageService {
  private apiUrl = 'https://localhost:7152/api/Packages';

  constructor(private http: HttpClient) { }

  getAllPackages(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getPackageById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  createPackage(packageData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, packageData);
  }

  updatePackage(id: number, packageData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, packageData);
  }

  deletePackage(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}