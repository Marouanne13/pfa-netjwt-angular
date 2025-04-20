import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5278/api/auth';
  private roleSubject = new BehaviorSubject<string | null>(null); // 🔹 Stockage réactif du rôle

  constructor(private http: HttpClient, private router: Router) {
    this.loadRoleFromToken(); // 🔹 Charger le rôle au démarrage
  }

  // 🔹 Connexion utilisateur
  login(email: string, password: string) {
    this.http.post<{ token: string, role: string }>(`${this.apiUrl}/login`, { email, password })
      .subscribe({
        next: response => {
          console.log('✅ Token reçu:', response.token);
          localStorage.setItem('token', response.token);
          localStorage.setItem('role', response.role); // ✅ Stocker aussi le rôle

          // 🔹 Mettre à jour le rôle dans BehaviorSubject
          this.roleSubject.next(response.role);

          // 🔹 Rediriger immédiatement après connexion
          this.redirectUser();
        },
        error: error => {
          console.error('❌ Erreur de connexion:', error);
          alert("Échec de connexion. Vérifiez vos identifiants.");
        }
      });
  }

  // 🔹 Déconnexion utilisateur
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role'); // ✅ Supprimer aussi le rôle
    this.roleSubject.next(null); // 🔹 Effacer le rôle
    this.router.navigate(['/login']);
  }

  // 🔹 Récupérer le token JWT
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // 🔹 Charger le rôle depuis le token
  private loadRoleFromToken() {
    const token = this.getToken();
    if (!token) {
      this.roleSubject.next(null);
      return;
    }

    try {
      const payload = JSON.parse(atob(token.split('.')[1])); // 🔹 Décodage du JWT
      console.log('🔍 Payload du JWT:', payload);

      const role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || payload["role"];
      console.log('🔍 Rôle extrait:', role);

      this.roleSubject.next(role || null);
    } catch (error) {
      console.error('❌ Erreur de décodage du token:', error);
      this.roleSubject.next(null);
    }
  }

  // 🔹 Permet aux composants d'obtenir le rôle en temps réel
  getRoleObservable() {
    return this.roleSubject.asObservable();
  }

  // 🔹 Redirection en fonction du rôle (Mise à jour avec /restaurant-list et nouveaux rôles)
  redirectUser() {
    const role = localStorage.getItem('role'); // ✅ Lire le rôle depuis localStorage

    console.log('🔄 Rôle détecté pour redirection:', role);

    if (!role) {
      console.error('❌ Aucun rôle détecté. Redirection vers /login.');
      this.router.navigate(['/login']);
      return;
    }

    let targetRoute = '/login'; // 🔹 Par défaut, retour à login
    if (role === 'Gérer les voyages') targetRoute = '/destination-admin';
    else if (role === 'Gérer les activités') targetRoute = '/activites';
    else if (role === 'Gérer les restaurants') targetRoute = '/restaurant-list'; // 🔹 Changement ici
    else if (role === 'Gérer les clients') targetRoute = '/clients';
    else if (role === 'Gérer les transports') targetRoute = '/admin-messages'; // 🔹 Nouveau rôle ajouté

    console.log('🚀 Redirection vers:', targetRoute);
    this.router.navigate([targetRoute]).then(success => {
      if (!success) {
        console.error('❌ Erreur de navigation, tentative avec window.location.href');
        window.location.href = targetRoute;
      }
    });
  }

  // 🔹 Vérification si l'utilisateur est connecté
  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
