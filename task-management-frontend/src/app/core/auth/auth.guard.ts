import { Injectable } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';

export const AuthGuard: CanActivateFn = async (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (await authService.checkAuthStatus()) {
    return true;
  } else {
    router.navigate(['/login']);
    return false;
  }
};
