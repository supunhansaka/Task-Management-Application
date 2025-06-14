import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginInfo, UserContext } from './interfaces/auth';

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

  async userContext(): Promise<UserContext> {
    try {
      const result = await firstValueFrom(
        this.http.get<UserContext>(
          `${this.apiUrl}/auth/context`,
          { withCredentials: true }
        )
      );
  
      this.loggedIn = result.isAuthenticated;
      return result;
    } catch (error) {
      this.loggedIn = false;
      return {
        isAuthenticated: false,
        username: null,
        userId: null
      };
    }
  }
  
  isLoggedIn(): boolean {
    return this.loggedIn;
  }
}
