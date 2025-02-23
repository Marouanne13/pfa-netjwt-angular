import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private apiUrl = 'http://localhost:5278/api/Restaurant'; // URL de ton API backend

  constructor(private http: HttpClient) {}

  getRestaurants(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getRestaurantById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  addRestaurant(restaurant: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, restaurant);
  }

  modifierRestaurant(id: number, restaurant: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, restaurant);
  }

  supprimerRestaurant(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
