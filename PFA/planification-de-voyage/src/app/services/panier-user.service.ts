import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PanierUserService {
  private apiUrl = 'http://localhost:5278/api/panier';
  creerSessionPaiement(data: { productName: string; amount: number }) {
    return this.http.post<any>('http://localhost:5278/api/paiement/create-checkout-session', data);
  }


  constructor(private http: HttpClient) {}

  // 🔍 Récupérer le panier complet
  getPanier(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${userId}`);
  }

  // ✅ Ajouter un panier
  ajouterAuPanier(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/ajouter`, data);
  }

  // 🗑 Supprimer un élément spécifique
  supprimerDuPanier(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/supprimer/${id}`);
  }

  // ❌ Vider tout le panier
  viderPanier(userId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/vider/${userId}`);
  }

  // 🧮 Calculer le total pour un utilisateur
  calculerTotal(userId: number): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total/${userId}`);
  }

  // (Facultatif) Modifier une activité dans le panier
  updateActiviteDansPanier(userId: number, destinationId: number, activiteId: number): Observable<any> {
    const data = { userId, destinationId, activiteId };
    return this.http.put(`${this.apiUrl}/update-activite`, data);
  }
}
