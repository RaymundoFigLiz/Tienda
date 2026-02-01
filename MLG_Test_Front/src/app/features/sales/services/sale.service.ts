import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { Sale } from '../models/sale.model';
import { PurchaseCart } from '../../shop/models/cart.model';

@Injectable({ providedIn: 'root' })
export class SaleService {
  private api = inject(ApiService);
  private endpoint = 'sale';

  getAll() {
    return this.api.get<Sale[]>(this.endpoint);
  }

  buyItems(data: PurchaseCart) {
    return this.api.post(this.endpoint, data);
  }
}
