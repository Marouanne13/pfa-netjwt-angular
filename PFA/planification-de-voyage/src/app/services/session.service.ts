import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private apiUrl = "http://localhost:5278/api/session";

  constructor(private http: HttpClient) {}

  // ✅ Enregistrer une donnée dans la session API
  setSessionData(key: string, value: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/set`, { key, value });
  }

  // ✅ Récupérer une donnée de la session API
  getSessionData(key: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/get/${key}`).pipe(
      catchError(error => {
        console.error(`❌ Erreur lors de la récupération de la session (${key}):`, error);
        return of(null); // Retourne `null` en cas d'erreur
      })
    );
  }

  // ✅ Supprimer une donnée de session
  removeSessionData(key: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/remove/${key}`);
  }

  // ✅ Alternative : Utiliser `localStorage` pour la session locale
  setLocalStorage(key: string, value: string) {
    localStorage.setItem(key, value);
  }
  
  getLocalStorage(key: string): string | null {
    return localStorage.getItem(key);
  }
  removeLocalStorage(key: string) {
    localStorage.removeItem(key);
  }
}
