import { Component, computed, inject } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/enums/roles.enum';

@Component({
  standalone: true,
  selector: 'app-main-layout',
  imports: [
    RouterModule,
    RouterOutlet,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
  ],
  templateUrl: './main-layout.component.html',
})
export class MainLayoutComponent {
  private auth = inject(AuthService);
  private router = inject(Router);
  user = this.auth.user;

  isAdministrator = computed(() => this.user()?.roleId == Roles.ADMINISTRADOR);
  fullName = computed(() => {
    const user = this.user();
    if (!user) return '';

    return `${user.name ?? ''} ${user.firstLastName ?? ''} ${user.secondLastName ?? ''}`.trim();
  });

  getRoleDescription(roleId: number | undefined) {
    if (roleId) return Roles[roleId];
    return '';
  }

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
