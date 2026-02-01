import { Component, inject, output } from '@angular/core';
import { Item } from '../../../items/models/item.model';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { CartService } from '../../services/cart.service';
import { StoreItem } from '../../../store-item/models/store-item.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    CommonModule,
    MatCardModule,
    MatListModule,
  ],

  templateUrl: './cart.component.html',
})
export class CartComponent {
  private service = inject(CartService);
  private snackBar = inject(MatSnackBar);
  storeItems: StoreItem[] = [];
  total: number = 0;

  refreshData = output();

  ngOnInit() {
    this.service.storeItems$.subscribe((storeItems) => (this.storeItems = storeItems));
    this.service.total$.subscribe((total) => (this.total = total));
  }

  remove(i: number, storeItem: StoreItem) {
    this.service.remove(i, storeItem);
  }

  clear() {
    this.service.clear();
  }

  buy() {
    this.service.buy()?.subscribe((result) => {
      if (result) {
        this.snackBar.open('Compra realizada con éxito', 'Cerrar', {
          duration: 3000,
          panelClass: ['success-snackbar'],
        });
        this.clear();
        this.refreshData.emit();
      }
    });
  }
}
