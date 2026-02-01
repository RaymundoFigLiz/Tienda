import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { guestGuard } from './core/guards/guest.guard';
import { LoginPageComponent } from './features/auth/login/pages/login-page.component';
import { RegisterPageComponent } from './features/auth/register/pages/register-page.component';
import { MainLayoutComponent } from './shared/layouts/main-layout/main-layout.component';
import { adminGuard } from './core/guards/admin.guard';
import { clientGuard } from './core/guards/client.guard';

export const routes: Routes = [
  // Auth
  {
    path: 'login',
    component: LoginPageComponent,
    canActivate: [guestGuard],
  },
  {
    path: 'register',
    component: RegisterPageComponent,
    canActivate: [guestGuard],
  },

  // Pages
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        redirectTo: 'items',
        pathMatch: 'full',
      },
      {
        path: 'items',
        canActivate: [adminGuard],
        loadComponent: () =>
          import('./features/items/pages/item-page.component').then((p) => p.ItemsPageComponent),
      },
      {
        path: 'stores',
        canActivate: [adminGuard],
        loadComponent: () =>
          import('./features/store/pages/store-page.component').then((p) => p.StorePageComponent),
      },
      {
        path: 'store-items',
        canActivate: [adminGuard],
        loadComponent: () =>
          import('./features/store-item/pages/store-item-page.component').then(
            (p) => p.StoreItemPageComponent,
          ),
      },
      {
        path: 'sales',
        canActivate: [adminGuard],
        loadComponent: () =>
          import('./features/sales/pages/sale-page.component').then((p) => p.SalePageComponent),
      },
      {
        path: 'shop',
        canActivate: [clientGuard],
        loadComponent: () =>
          import('./features/shop/pages/shop-page.component').then((p) => p.ShopPageComponent),
      },
    ],
  },

  // Redirect
  {
    path: '**',
    redirectTo: '',
  },
];
