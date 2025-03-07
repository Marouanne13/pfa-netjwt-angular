import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isAuthenticated = this.authService.isAuthenticated(); // 🔹 Stockage pour éviter l'appel multiple
    console.debug('🔍 AuthGuard - Vérification de l\'authentification:', isAuthenticated);

    if (!isAuthenticated) {
      console.warn('🚫 Accès refusé - Non authentifié. Redirection vers /login.');
      this.router.navigate(['/login']);
      return false;
    }

    // Vérifie si l'utilisateur tente d'accéder à /loginUser alors qu'il est déjà connecté
    if (route.routeConfig && route.routeConfig.path === 'loginUser') {
      console.debug('🔍 Vérification de l\'accès à /loginUser');

      if (isAuthenticated) {
        console.warn('🚫 Accès refusé à /loginUser - Utilisateur déjà connecté. Redirection vers /home.');
        this.router.navigate(['/home']);
        return false;
      }
    }

    console.debug('✅ Accès autorisé à la route:', route.routeConfig?.path);
    return true; // ✅ Autorise l'accès
  }
}
