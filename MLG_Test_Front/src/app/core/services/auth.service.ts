import { Injectable, computed, inject, signal } from '@angular/core';
import { ApiService } from './api.service';
import { TokenService } from './token.service';
import { tap } from 'rxjs';
import { TokenData } from '../models/token-data.model';
import { LoginRequest } from '../models/login-request.model';
import { RegisterRequest } from '../models/register-request.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private api = inject(ApiService);
  private tokenService = inject(TokenService);
  private _user = signal<TokenData | null>(null);

  user = computed(() => this._user());
  isAuthenticated = computed(() => !!this._user());

  constructor() {
    this.loadUserFromToken();
  }

  login(credentials: LoginRequest) {
    return this.api.post<{ token: string }>('auth/login', credentials).pipe(
      tap((res) => {
        this.tokenService.setToken(res.result.token);
        this.loadUserFromToken();
      }),
    );
  }

  register(data: RegisterRequest) {
    return this.api.post('auth/register', data);
  }

  logout(): void {
    this.tokenService.clear();
    this._user.set(null);
  }

  private loadUserFromToken(): void {
    const payload: any = this.tokenService.decodeToken();

    if (!payload || this.tokenService.isTokenExpired()) {
      this.logout();
      return;
    }

    this._user.set({
      id: payload.sub,
      email: payload.email,
      name: payload.name,
      firstLastName: payload.firstLastName,
      secondLastName: payload.secondLastName,
      roleId: payload.roleId,
    });
  }
}
