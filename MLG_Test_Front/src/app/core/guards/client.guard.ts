import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Roles } from '../enums/roles.enum';

export const clientGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  const role = auth.user()?.roleId;

  return role == Number(Roles.CLIENTE) ? true : router.createUrlTree(['/items']);
};
