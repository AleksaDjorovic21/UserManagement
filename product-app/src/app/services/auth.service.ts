import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Login } from '../../models/login.model';
import { Register } from '../../models/register.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'http://localhost:5099/api/';
  private currentUserSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public currentUser = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) { }

  register(user: Register): Observable<string> {
    return this.http.post(`${this.baseUrl}ApplicationUser/register`, user, { responseType: 'text' });
  }

  // Check if 'localStorage' is available (for client-side execution only)
  getUserRoles(): string[] {
  if (typeof window !== 'undefined' && localStorage) {
    const user = localStorage.getItem('user');
    if (user) {
      const parsedUser = JSON.parse(user);
      return parsedUser.roles || [];
    }
  }
  return [];
}

  login(credentials: Login): Observable<any> {
    return this.http.post(`${this.baseUrl}ApplicationUser/login`, credentials).pipe(
      tap((user: any) => {
        this.currentUserSubject.next(user);
        localStorage.setItem('user', JSON.stringify(user)); 
      })
    );
  }
  
  logout() {
    localStorage.removeItem('user');
    this.currentUserSubject.next(null);
  }

  isLoggedIn() {
    return !!localStorage.getItem('user');
  }

  getAllUsers(): Observable<any> {
    return this.http.get(`${this.baseUrl}Admin/users`);
  }

  assignAdminRole(userId: string): Observable<string> {
    return this.http.post(`${this.baseUrl}Admin/assign-admin-role/${userId}`, {}, { responseType: 'text' });
  }
  
  getAuthHeaders() {
    const token = localStorage.getItem('authToken'); 
    return {
        Authorization: `Bearer ${token}`,
    };
  }
}
