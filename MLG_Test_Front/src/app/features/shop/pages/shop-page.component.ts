import { Component, inject, signal } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { Item } from '../../items/models/item.model';
import { CartService } from '../services/cart.service';
import { CartComponent } from '../components/cart-component/cart.component';
import { StoreItem } from '../../store-item/models/store-item.model';
import { StoreItemService } from '../../store-item/services/store-item.service';

@Component({
  standalone: true,
  imports: [
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    CommonModule,
    MatCardModule,
    CartComponent,
  ],

  templateUrl: './shop-page.component.html',
})
export class ShopPageComponent {
  private cartService = inject(CartService);
  private storeItemService = inject(StoreItemService);

  storeItems = signal<StoreItem[]>([]);

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.storeItemService.getAll().subscribe((res) => {
      this.storeItems.set(res.result);
    });
  }

  add(storeItem: StoreItem) {
    this.cartService.add(storeItem);
  }

  onRefreshData() {
    this.load();
  }
}
