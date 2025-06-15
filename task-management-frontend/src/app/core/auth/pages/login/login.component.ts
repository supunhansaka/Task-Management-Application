import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth.service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { LoginInfo } from '../../interfaces/auth';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  async onSubmit() {
    if (this.loginForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';
      
      try {
        const loginInfo: LoginInfo = this.loginForm.value;
        const success = await this.authService.login(loginInfo);

        if (success) {
          this.router.navigate(['/dashboard']);
          this.toastr.success('Welcome back!', 'Login successful');
        } else {
          this.errorMessage = 'Invalid username or password';
          this.toastr.error('Please check your credentials', 'Login failed');
        }
      } catch (error) {
        this.errorMessage = 'An error occurred. Please try again.';
        this.toastr.error('Something went wrong', 'Error');
      } finally {
        this.isLoading = false;
      }
    } else {
      this.loginForm.markAllAsTouched();
    }
  }
}
