import { Component, inject, signal } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { Item } from '../models/item.model';
import { ItemService } from '../services/item.service';
import { ItemFormDialogComponent } from '../components/item-form-dialog/item-form-dialog.component';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';

@Component({
  standalone: true,
  imports: [MatTableModule, MatButtonModule, MatIconModule, CommonModule, MatCardModule],
  templateUrl: './item-page.component.html',
})
export class ItemsPageComponent {
  private service = inject(ItemService);
  private dialog = inject(MatDialog);

  items = signal<Item[]>([]);
  displayedColumns = ['id', 'image', 'code', 'description', 'actions'];

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.service.getAll().subscribe((res) => {
      this.items.set(res.result);
    });
  }

  create(): void {
    this.openDialog();
  }

  edit(item: Item): void {
    this.openDialog(item);
  }

  delete(item: Item): void {
    this.service.delete(item.id).subscribe((result) => {
      if (result) this.load();
    });
  }

  private openDialog(item?: Item): void {
    const dialogRef = this.dialog.open(ItemFormDialogComponent, {
      width: '400px',
      data: item ?? null,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) this.load();
    });
  }
}
