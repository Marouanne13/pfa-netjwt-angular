import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserSessionService {

  constructor() {}

  // 🔹 Récupérer toutes les infos utiles depuis le token
  getUserFromToken(): { email: string, userId: number, role: string } | null {
    const token = localStorage.getItem('token');
    if (!token) return null;

    try {
      const payload = this.decodeToken(token);
      return {
        email: payload["email"],
        userId: +payload["sub"],
        role: payload["role"]
      };
    } catch (error) {
      console.error("❌ Erreur lors de l'extraction des infos du token", error);
      return null;
    }
  }

  // 🔹 Méthode rapide pour obtenir juste l'ID de l'utilisateur
  getUserId(): number | null {
    const user = this.getUserFromToken();
    return user?.userId ?? null;
  }

  // 🔹 Méthode privée pour décoder le token
  private decodeToken(token: string): any {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
