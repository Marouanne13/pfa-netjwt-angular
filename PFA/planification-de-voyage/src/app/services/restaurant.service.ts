import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Définition du modèle de restaurant pour le typage
export interface Restaurant {
  id?: number;
  nom: string;
  typeCuisine: string;
  adresse: string;
  numeroTelephone: string;
  nombreEtoiles: number;
  description?: string;
  livraisonDisponible: boolean;
  reservationEnLigne: boolean;
  estOuvert24h: boolean;
  utilisateurId?: number;
}

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private apiUrl = 'http://localhost:5278/api/Restaurant'; // URL de l'API backend

  constructor(private http: HttpClient) {}

  getRestaurants(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>(this.apiUrl);
  }

  getRestaurantById(id: number): Observable<Restaurant> {
    return this.http.get<Restaurant>(`${this.apiUrl}/${id}`);
  }

  addRestaurant(restaurant: Restaurant): Observable<Restaurant> {
    return this.http.post<Restaurant>(this.apiUrl, restaurant);
  }
  modifierRestaurant(id: number, restaurant: any): Observable<any> {
    console.log("Requête PUT vers :", `${this.apiUrl}/${id}`);
    console.log("Données envoyées :", restaurant);

    return this.http.put<any>(`${this.apiUrl}/${id}`, restaurant);
}


  supprimerRestaurant(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
