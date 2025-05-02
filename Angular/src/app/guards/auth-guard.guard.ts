import { CanActivateFn, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const authService = inject(AuthService); // Inject AuthService using inject() function
  const router = inject(Router); // Inject Router

  const expectedRole = route.data['role']; // Get expected role from route metadata
  const isLoggedIn = authService.isAuthenticated(); // Check if the user is logged in
  const userRole = authService.getUserRole(); // Get the current user's role

  // If the user is not logged in, redirect to the home page
  if (!isLoggedIn) {
    return router.parseUrl('/'); 
  }

  // If the user's role does not match the expected role, redirect to the "unauthorized" page
  if (expectedRole && userRole !== expectedRole) {
    return router.parseUrl('/unauthorized');
  }

  // Allow access if everything is fine
  return true;
};
