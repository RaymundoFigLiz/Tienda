import { Component, inject, signal } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { StoreService } from '../services/store.service';
import { Store } from '../models/store.model';
import { StoreFormDialogComponent } from '../components/store-form-dialog/store-form-dialog.component';

@Component({
  standalone: true,
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './store-page.component.html',
})
export class StorePageComponent {
  private service = inject(StoreService);
  private dialog = inject(MatDialog);

  stores = signal<Store[]>([]);
  displayedColumns = [
    'name',
    'address',
    'postalCode',
    'internalNumber',
    'externalNumber',
    'actions',
  ];

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.service.getAll().subscribe((res) => {
      this.stores.set(res.result);
    });
  }

  create(): void {
    this.openDialog();
  }

  edit(store: Store): void {
    this.openDialog(store);
  }

  delete(store: Store): void {
    this.service.delete(store.id).subscribe((result) => {
      if (result) this.load();
    });
  }

  private openDialog(store?: Store): void {
    const dialogRef = this.dialog.open(StoreFormDialogComponent, {
      width: '400px',
      data: store ?? null,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) this.load();
    });
  }
}
