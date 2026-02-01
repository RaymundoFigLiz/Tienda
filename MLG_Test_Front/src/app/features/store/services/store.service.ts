import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { Store, StoreBase } from '../models/store.model';

@Injectable({ providedIn: 'root' })
export class StoreService {
  private api = inject(ApiService);
  private endpoint = 'store';

  getAll() {
    return this.api.get<Store[]>(this.endpoint);
  }

  create(data: StoreBase) {
    return this.api.post<Store>(this.endpoint, data);
  }

  update(id: number, data: StoreBase) {
    return this.api.put<Store>(`${this.endpoint}/${id}`, data);
  }

  delete(id: number) {
    return this.api.delete<Store>(`${this.endpoint}/${id}`);
  }
}
