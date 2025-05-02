import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';
import { Observable, tap } from 'rxjs';

interface AuthResponse {
  token: string;
}
interface LoginRequest {
  email: string;
  password: string;
}
interface DecodedToken {
  name?: string;
  role?: string;
  exp: number;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:7266/api/Auth';
  private tokenKey = 'jwt_token';

  constructor(private http: HttpClient) {}

  login(credentials: LoginRequest, type: 'admin' | 'trainer' | 'user'): Observable<AuthResponse> {
    const endpoint = type === 'admin' ? 'admin-login' : 'login';
    return this.http.post<AuthResponse>(`${this.apiUrl}/${endpoint}`, credentials, {
      withCredentials: true
    }).pipe(tap(response => this.saveToken(response.token)));
  }

  saveToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  removeToken(): void {
    localStorage.removeItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) return false;
    try {
      const decoded: DecodedToken = jwtDecode(token);
      return decoded.exp > Date.now() / 1000;
    } catch {
      return false;
    }
  }

  getUserRole(): string | null {
    const token = this.getToken();
    console.log("token"+token);
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
    } catch {
      return null;
    }
  }

  getUserName(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || null;
    } catch {
      return null;
    }
  }
}
