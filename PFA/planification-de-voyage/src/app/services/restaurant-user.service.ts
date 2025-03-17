import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RestaurantUserService {
  private apiUrl = 'http://localhost:5278/api/restaurantsUser'; // Remplace par l'URL réelle

  constructor(private http: HttpClient) {}

  // 🔥 Récupérer les restaurants basés sur la destination en session
  getRestaurants(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/par-destination`);
  }
}
