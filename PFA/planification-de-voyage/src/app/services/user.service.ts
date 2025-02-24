import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5278/api/user';

  constructor(private http: HttpClient, private router: Router) {}

  // ✅ Inscription
  register(userData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData);
  }

  // ✅ Connexion (Login)
  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials);
  }

  // ✅ Stocker Token et UserID après connexion
  saveToken(token: string, userId: number): void {
    localStorage.setItem('token', token);
    localStorage.setItem('userId', userId.toString()); // 🔥 Stocker l'ID utilisateur
  }

  // ✅ Récupérer le Token JWT
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // ✅ Récupérer l'ID utilisateur
  getUserId(): number | null {
    const userId = localStorage.getItem('userId');
    return userId ? parseInt(userId, 10) : null;
  }

  // ✅ Vérifier si l'utilisateur est authentifié
  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  // ✅ Déconnexion (supprime Token et UserId)
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    this.router.navigate(['/login']);
  }
}
