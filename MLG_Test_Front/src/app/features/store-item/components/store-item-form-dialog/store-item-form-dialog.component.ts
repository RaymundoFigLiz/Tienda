import { Component, inject, OnInit, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import {
  FormControl,
  FormsModule,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { StoreItemService } from '../../services/store-item.service';
import { StoreItem, StoreItemBase } from '../../models/store-item.model';
import { Item } from '../../../items/models/item.model';
import { Store } from '../../../store/models/store.model';
import { StoreService } from '../../../store/services/store.service';
import { ItemService } from '../../../items/services/item.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

interface FormType {
  storeId: FormControl<number | null>;
  itemId: FormControl<number | null>;
  price: FormControl<number | null>;
  stock: FormControl<number | null>;
}

@Component({
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
  ],
  templateUrl: './store-item-form-dialog.component.html',
})
export class StoreItemFormDialogComponent implements OnInit {
  private fb = inject(NonNullableFormBuilder);
  private storeItemService = inject(StoreItemService);
  private storeService = inject(StoreService);
  private itemService = inject(ItemService);
  private dialogRef = inject(MatDialogRef<StoreItemFormDialogComponent>);
  data = inject<StoreItem | null>(MAT_DIALOG_DATA);

  stores = signal<Store[]>([]);
  items = signal<Item[]>([]);

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.storeService.getAll().subscribe((res) => {
      this.stores.set(res.result);
    });
    this.itemService.getAll().subscribe((res) => {
      this.items.set(res.result);
    });
  }

  form = this.fb.group<FormType>({
    storeId: this.fb.control(null, Validators.required),
    itemId: this.fb.control(null, Validators.required),
    price: this.fb.control(null, Validators.required),
    stock: this.fb.control(null, Validators.required),
  });

  constructor() {
    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save(): void {
    if (this.form.invalid) return;

    const action = this.data
      ? this.storeItemService.update(this.data.id, this.form.value as StoreItemBase)
      : this.storeItemService.create(this.form.value as StoreItemBase);

    action.subscribe(() => {
      this.dialogRef.close(true);
    });
  }
}
