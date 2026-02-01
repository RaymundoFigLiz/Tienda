import { inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { StoreItem } from '../../store-item/models/store-item.model';
import { AuthService } from '../../../core/services/auth.service';
import { SaleService } from '../../sales/services/sale.service';

@Injectable({ providedIn: 'root' })
export class CartService {
  private authService = inject(AuthService);
  private saleService = inject(SaleService);
  private storeItems: StoreItem[] = [];
  storeItems$ = new BehaviorSubject<StoreItem[]>([]);
  total$ = new BehaviorSubject<number>(0);
  total: number = 0;

  add(storeItem: StoreItem) {
    this.storeItems.push(storeItem);
    this.storeItems$.next(this.storeItems);
    this.total = this.total$.getValue() + storeItem.price;
    this.total$.next(this.total);
  }

  remove(index: number, storeItem: StoreItem) {
    this.storeItems.splice(index, 1);
    this.storeItems$.next(this.storeItems);
    this.total = this.total$.getValue() - storeItem.price;
    this.total$.next(this.total);
  }

  clear() {
    this.storeItems = [];
    this.storeItems$.next([]);
    this.total = 0;
    this.total$.next(0);
  }

  buy() {
    if (this.storeItems.length === 0) return;

    const clientId = this.authService.user()!.id;
    const storeItemIds = this.storeItems.map((s) => s.id);

    return this.saleService.buyItems({ clientId, storeItemIds });
  }
}
