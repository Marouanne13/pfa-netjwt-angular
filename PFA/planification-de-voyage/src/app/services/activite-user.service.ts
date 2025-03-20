import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'  // Vérifie que le service est bien injectable à l'échelle de l'application
})
export class ActiviteUserService {
  private apiUrl = 'http://localhost:5278/api';  // Assure-toi que l'URL est correcte

  constructor(private http: HttpClient) {}

  getActivitesParDestination(destinationId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/activites/par-destination/${destinationId}`);
  }
  
  
}
