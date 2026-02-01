import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Roles } from '../enums/roles.enum';

export const adminGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  const role = auth.user()?.roleId;

  return role == Roles.ADMINISTRADOR ? true : router.createUrlTree(['/shop']);
};
