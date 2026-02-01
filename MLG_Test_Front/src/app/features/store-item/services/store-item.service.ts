import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { StoreItem, StoreItemBase } from '../models/store-item.model';

@Injectable({ providedIn: 'root' })
export class StoreItemService {
  private api = inject(ApiService);
  private endpoint = 'store-item';

  getAll() {
    return this.api.get<StoreItem[]>(this.endpoint);
  }

  create(data: StoreItemBase) {
    return this.api.post<StoreItem>(this.endpoint, data);
  }

  update(id: number, data: StoreItemBase) {
    return this.api.put<StoreItem>(`${this.endpoint}/${id}`, data);
  }

  delete(id: number) {
    return this.api.delete<StoreItem>(`${this.endpoint}/${id}`);
  }
}
