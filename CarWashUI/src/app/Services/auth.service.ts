import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { User } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7152/api/Auth';

  private loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.hasToken());
  private loggedInUser: BehaviorSubject<any> = new BehaviorSubject<any>(this.getUserDetails());

  constructor(private http: HttpClient,private router:Router) { }

  get isLoggedIn$(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  get loggedInUser$(): Observable<any> {
    return this.loggedInUser.asObservable();
  }

  public hasToken(): boolean {
    return !!localStorage.getItem('token');
  }

  public getUserDetails(): any {
    const userData = localStorage.getItem('user');
    if (userData) {
      return JSON.parse(userData);
    }
    return null;
  }

  public getUserId(){
    const userData = localStorage.getItem('user');
    if (userData) {
      let user = JSON.parse(userData);
        return user.UserId;
     
    }
    return null;
  }

  getUserRoleAndId(){
    const userData = localStorage.getItem('user');
    if (userData) {
      let user = JSON.parse(userData);
      let {AdminId,UserId,WasherId,Role} = user;
      if(AdminId) return {AdminId,Role};
      else if(UserId) return {UserId,Role};
      else return {WasherId,Role};
    }
    return null;
  }

  login(loginDto: LoginDto): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/login`, loginDto).pipe(
      tap(response => {
        const token = response.token;
        if (token) {
          localStorage.setItem('token', token);
          this.loggedIn.next(true);
          const decodedToken = jwtDecode<DecodedToken>(token);
          console.log(decodedToken);
          const user = JSON.parse(decodedToken.user);
          this.loggedInUser.next(user);
          localStorage.setItem('user', JSON.stringify(user));
        }
      })
    );
  }

  register(registerDto: RegisterDto): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/register`, registerDto);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.loggedIn.next(false);
    this.loggedInUser.next(null);
    this.router.navigate(['/login']);
  }

  updateUser(user: User): Observable<User> {
    const url = `${this.baseUrl}/user`;
    return this.http.put<User>(url, user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  getUserById(id:number) : Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/user/${id}`);
  }

  getUserByRoleAndId(userRoleId:any){
    let {AdminId,UserId,WasherId,Role} = userRoleId;
      if(AdminId) {
        return this.http.get<User>(`${this.baseUrl}/admin/${AdminId}`);
      }
      else if(UserId){
        return this.http.get<User>(`${this.baseUrl}/user/${UserId}`);
      }
      else{
        return this.http.get<User>(`${this.baseUrl}/washer/${WasherId}`);
      }
  }

}

export interface RegisterDto {
  email: string;
  password: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

interface DecodedToken {
  user:string;
}