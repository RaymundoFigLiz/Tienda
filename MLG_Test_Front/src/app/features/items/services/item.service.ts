import { Injectable, inject } from '@angular/core';
import { Item, ItemBase } from '../models/item.model';
import { ApiService } from '../../../core/services/api.service';

@Injectable({ providedIn: 'root' })
export class ItemService {
  private api = inject(ApiService);
  private endpoint = 'item';

  getAll() {
    return this.api.get<Item[]>(this.endpoint);
  }

  create(data: ItemBase) {
    return this.api.post<Item>(this.endpoint, data);
  }

  update(id: number, data: ItemBase) {
    return this.api.put<Item>(`${this.endpoint}/${id}`, data);
  }

  delete(id: number) {
    return this.api.delete<Item>(`${this.endpoint}/${id}`);
  }
}
