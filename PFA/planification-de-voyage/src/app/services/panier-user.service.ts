import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PanierUserService {
  private apiUrl = 'http://localhost:5278/api/panier';

  constructor(private http: HttpClient) {}

  getPanier(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${userId}`);
  }

  ajouterAuPanier(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/ajouter`, data);
  }

  supprimerDuPanier(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/supprimer/${id}`);
  }

  viderPanier(userId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/vider/${userId}`);
  }
  ajouterToutAuPanier(panierData: any) {
    return this.http.post<any>("http://localhost:5278/api/panier/ajouter", panierData);
  }
  mettreAJourPanier(id: number, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/modifier/${id}`, data);
  }
  getPanierComplet(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/complet/${userId}`);
  }
  
  updateActiviteDansPanier(userId: number, destinationId: number, activiteId: number): Observable<any> {
    const data = { userId, destinationId, activiteId };
    return this.http.put(`${this.apiUrl}/update-activite`, data);
  }
  
  
}
