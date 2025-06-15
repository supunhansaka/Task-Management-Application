import { Component, Input } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  standalone: true,
  selector: 'app-header',
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent { 

  @Input() username: string | null = null;

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {}

  async logout() {
    try {
      await this.authService.logout();
      this.toastr.success('Logged out successfully');
      this.router.navigate(['/login']);
    } catch (error) {
      this.toastr.error('Error logging out');
    }
  }
}


