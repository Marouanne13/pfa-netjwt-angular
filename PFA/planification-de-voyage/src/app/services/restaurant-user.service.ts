import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RestaurantUserService {

  // ✅ URL correcte (avec le bon nom de contrôleur)
  private apiUrl = 'http://localhost:5278/api/restaurantsUser'; // ✔️ note le "s"


  constructor(private http: HttpClient) {}

  // 🔁 Récupère tous les restaurants
  getRestaurants(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}`);
  }
// restaurant-user.service.ts
getRestaurantsTemp(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/session`);
}


  // 🔍 Rechercher un restaurant
  searchRestaurants(query: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/search?query=${query}`);
  }

  // 📦 Stocker un restaurant en session
  stockerDansSession(resto: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/session`, resto);
  }

  
  
}
