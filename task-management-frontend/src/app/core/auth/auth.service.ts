import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginInfo } from './interfaces/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = false;
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  async login(loginInfo: LoginInfo): Promise<boolean> {
    try {
      await firstValueFrom(
        this.http.post(`${this.apiUrl}/auth/login`, loginInfo, { withCredentials: true })
      );
      this.loggedIn = true;
      return true;
    } catch (error) {
      this.loggedIn = false;
      return false;
    }
  }

  async logout(): Promise<void> {
    try {
      await firstValueFrom(
        this.http.post(`${this.apiUrl}/auth/logout`, {}, { withCredentials: true })
      );
    } finally {
      this.loggedIn = false;
    }
  }

  async checkAuthStatus(): Promise<boolean> {
    try {
      const result = await firstValueFrom(
        this.http.get<{ isLoggedIn: boolean, username?: string }>(
          `${this.apiUrl}/auth/status`,
          { withCredentials: true }
        )
      );
  
      this.loggedIn = result.isLoggedIn;
      return this.loggedIn;
    } catch (error) {
      this.loggedIn = false;
      return false;
    }
  }
  
  isLoggedIn(): boolean {
    return this.loggedIn;
  }
}
