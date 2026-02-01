import { Component, inject, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import {
  FormBuilder,
  FormControl,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ItemService } from '../../services/item.service';
import { Item } from '../../models/item.model';
import { MatIcon } from '@angular/material/icon';

interface FormType {
  code: FormControl<string>;
  description: FormControl<string>;
  image: FormControl<string>;
}

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, MatInputModule, MatButtonModule, MatDialogModule, MatIcon],
  templateUrl: './item-form-dialog.component.html',
})
export class ItemFormDialogComponent {
  private fb = inject(NonNullableFormBuilder);
  private service = inject(ItemService);
  private dialogRef = inject(MatDialogRef<ItemFormDialogComponent>);
  data = inject<Item | null>(MAT_DIALOG_DATA);

  form = this.fb.group<FormType>({
    code: this.fb.control('', Validators.required),
    description: this.fb.control('', Validators.required),
    image: this.fb.control(''),
  });

  constructor() {
    if (this.data) {
      this.form.patchValue(this.data);
      this.data.image && this.imagePreview.set(this.data.image);
    }
  }

  save(): void {
    if (this.form.invalid) return;

    const data = {
      ...this.form.getRawValue(),
      image: this.imagePreview() ?? '',
    };

    const action = this.data ? this.service.update(this.data.id, data) : this.service.create(data);

    action.subscribe(() => {
      this.dialogRef.close(true);
    });
  }

  imagePreview = signal<string | null>(null);
  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) return;

    const file = input.files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this.imagePreview.set(reader.result as string);
    };

    reader.readAsDataURL(file);
  }
}
