import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import {
  FormControl,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { StoreService } from '../../services/store.service';
import { Store } from '../../models/store.model';

interface FormType {
  name: FormControl<string>;
  address: FormControl<string>;
  postalCode: FormControl<string>;
  externalNumber: FormControl<string>;
  internalNumber?: FormControl<string>;
}

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, MatInputModule, MatButtonModule, MatDialogModule],
  templateUrl: './store-form-dialog.component.html',
})
export class StoreFormDialogComponent {
  private fb = inject(NonNullableFormBuilder);
  private service = inject(StoreService);
  private dialogRef = inject(MatDialogRef<StoreFormDialogComponent>);
  data = inject<Store | null>(MAT_DIALOG_DATA);

  form = this.fb.group<FormType>({
    name: this.fb.control('', Validators.required),
    address: this.fb.control('', Validators.required),
    postalCode: this.fb.control('', Validators.required),
    externalNumber: this.fb.control('', Validators.required),
    internalNumber: this.fb.control(''),
  });

  constructor() {
    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save(): void {
    if (this.form.invalid) return;

    const action = this.data
      ? this.service.update(this.data.id, this.form.getRawValue())
      : this.service.create(this.form.getRawValue());

    action.subscribe(() => {
      this.dialogRef.close(true);
    });
  }
}
