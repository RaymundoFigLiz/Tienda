import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../config/api.config';
import { Wrapper } from '../models/wrapper.model';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  private baseUrl = API_CONFIG.baseUrl;

  get<T>(endpoint: string): Observable<Wrapper<T>> {
    return this.http.get<Wrapper<T>>(`${this.baseUrl}/${endpoint}`);
  }

  post<T>(endpoint: string, body: any): Observable<Wrapper<T>> {
    return this.http.post<Wrapper<T>>(`${this.baseUrl}/${endpoint}`, body);
  }

  put<T>(endpoint: string, body: any): Observable<Wrapper<T>> {
    return this.http.put<Wrapper<T>>(`${this.baseUrl}/${endpoint}`, body);
  }

  delete<T>(endpoint: string): Observable<Wrapper<T>> {
    return this.http.delete<Wrapper<T>>(`${this.baseUrl}/${endpoint}`);
  }
}
