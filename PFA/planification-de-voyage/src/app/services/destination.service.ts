import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  private apiUrl = 'http://localhost:5278/api/destinations'; // Remplace avec ton backend

  constructor(private http: HttpClient) {}

  getDestinations(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
