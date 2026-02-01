import { Component, inject, OnInit, signal } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { StoreItemService } from '../services/store-item.service';
import { StoreItem } from '../models/store-item.model';
import { StoreItemFormDialogComponent } from '../components/store-item-form-dialog/store-item-form-dialog.component';

@Component({
  standalone: true,
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './store-item-page.component.html',
})
export class StoreItemPageComponent implements OnInit {
  private service = inject(StoreItemService);
  private dialog = inject(MatDialog);

  storeItems = signal<StoreItem[]>([]);
  displayedColumns = ['storeName', 'itemName', 'price', 'stock', 'actions'];

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.service.getAll().subscribe((res) => {
      this.storeItems.set(res.result);
    });
  }

  create(): void {
    this.openDialog();
  }

  edit(storeItem: StoreItem): void {
    this.openDialog(storeItem);
  }

  delete(storeItem: StoreItem): void {
    this.service.delete(storeItem.id).subscribe((result) => {
      if (result) this.load();
    });
  }

  private openDialog(storeItem?: StoreItem): void {
    const dialogRef = this.dialog.open(StoreItemFormDialogComponent, {
      width: '400px',
      data: storeItem ?? null,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) this.load();
    });
  }
}
