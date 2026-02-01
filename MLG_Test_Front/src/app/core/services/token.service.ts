import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class TokenService {
  private readonly TOKEN_KEY = 'jwt';

  setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  clear(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  decodeToken<T = any>(): T | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const payload = token.split('.')[1];

      const base64 = payload.replace(/-/g, '+').replace(/_/g, '/');

      const decoded = atob(base64);
      return JSON.parse(decoded);
    } catch (error) {
      console.error('Error decoding token', error);
      return null;
    }
  }

  isTokenExpired(): boolean {
    const payload: any = this.decodeToken();
    if (!payload?.exp) return true;

    const now = Math.floor(Date.now() / 1000);
    return payload.exp < now;
  }
}
