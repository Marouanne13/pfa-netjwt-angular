import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {
  private apiUrl = 'http://localhost:5278/api/Restaurant'; // Assure-toi que c'est la bonne URL

  constructor(private http: HttpClient) {}

  getRestaurants(): Observable<any[]> {
    console.log('Appel API en cours...');
    return this.http.get<any[]>(this.apiUrl);
  }
}
