import { Component, inject } from '@angular/core';
import {
  Validators,
  ReactiveFormsModule,
  NonNullableFormBuilder,
  FormControl,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Roles } from '../../../../core/enums/roles.enum';

interface FormType {
  email: FormControl<string>;
  password: FormControl<string>;
  name: FormControl<string>;
  firstLastName: FormControl<string>;
  secondLastName: FormControl<string>;
  roleId: FormControl<number>;
  addressName: FormControl<string>;
  postalCode: FormControl<string>;
  externalNumber: FormControl<string>;
  internalNumber?: FormControl<string>;
}

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [ReactiveFormsModule, MatInputModule, MatButtonModule, MatCardModule, MatSnackBarModule],
  templateUrl: './register-page.component.html',
})
export class RegisterPageComponent {
  private fb = inject(NonNullableFormBuilder);
  private auth = inject(AuthService);
  private snackBar = inject(MatSnackBar);
  router = inject(Router);

  form = this.fb.group<FormType>({
    email: this.fb.control('', [Validators.required, Validators.email]),
    password: this.fb.control('', [Validators.required, Validators.minLength(8)]),
    name: this.fb.control('', Validators.required),
    firstLastName: this.fb.control('', Validators.required),
    secondLastName: this.fb.control(''),
    roleId: this.fb.control(Roles.CLIENTE), // Cliente por defecto
    addressName: this.fb.control('', Validators.required),
    postalCode: this.fb.control('', Validators.required),
    externalNumber: this.fb.control('', Validators.required),
    internalNumber: this.fb.control(''),
  });

  submit(): void {
    if (this.form.invalid) return;

    this.auth.register(this.form.getRawValue()).subscribe({
      next: () => {
        this.snackBar.open('Registro exitoso, ahora puedes iniciar sesión', 'Cerrar', {
          duration: 3000,
          panelClass: ['success-snackbar'],
        });

        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
