import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    console.log('🔍 AuthGuard - Vérification de l\'authentification:', this.authService.isAuthenticated());

    // 🔹 Vérifie si l'utilisateur est authentifié
    if (!this.authService.isAuthenticated()) {
      console.warn('🚫 Accès refusé - Non authentifié. Redirection vers /login.');
      this.router.navigate(['/login']);
      return false;
    }

    // 🔹 Vérifie si la route est "loginUser" et empêche son accès si déjà connecté
    if (route.routeConfig?.path === 'loginUser') {
      console.log('🔍 Vérification de l\'accès à /loginUser');
      if (this.authService.isAuthenticated()) {
        console.warn('🚫 Accès refusé à /loginUser - Utilisateur déjà connecté. Redirection vers /home.');
        this.router.navigate(['/home']);
        return false;
      }
    }

    console.log('✅ Accès autorisé à la route:', route.routeConfig?.path);
    return true; // ✅ Autorise l'accès si l'utilisateur est connecté et la route autorisée
  }
}
