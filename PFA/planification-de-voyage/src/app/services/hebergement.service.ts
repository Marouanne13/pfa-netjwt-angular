import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HebergementService {
  private apiUrl = 'http://localhost:5278/api/hebergements'; // URL de ton API .NET

  constructor(private http: HttpClient) {}

  getHebergementsParDestination(nom: string): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:5278/api/hebergements/destination/${nom}`);
  }
  
}
