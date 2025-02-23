import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ActiviteService {
  private apiUrl = 'http://localhost:5278/api/activites'; // ✅ API Backend

  constructor(private http: HttpClient) {}

  // 📌 ✅ Récupérer toutes les activités
  getActivites(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // 📌 ✅ Récupérer une seule activité par ID
  getActiviteById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  // 📌 ✅ Ajouter une nouvelle activité
  addActivite(activite: any): Observable<any> {
    console.log("🔍 Envoi de l'activité :", activite); // Debug
    return this.http.post<any>(this.apiUrl, activite);
  }

  // 📌 ✅ Modifier une activité
  updateActivite(id: number, activite: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, activite);
  }

  // 📌 ✅ Supprimer une activité
  deleteActivite(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
