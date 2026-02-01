import { Component, inject, signal } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Sale } from '../models/sale.model';
import { DatePipe } from '@angular/common';
import { SaleService } from '../services/sale.service';

@Component({
  standalone: true,
  imports: [MatTableModule, MatButtonModule, MatIconModule, DatePipe],
  templateUrl: './sale-page.component.html',
})
export class SalePageComponent {
  private service = inject(SaleService);
  sales = signal<Sale[]>([]);
  displayedColumns = ['clientFullName', 'itemName', 'storeName', 'price', 'date'];

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.service.getAll().subscribe((res) => {
      this.sales.set(res.result);
    });
  }
}
