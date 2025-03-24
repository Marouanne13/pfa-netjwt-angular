import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Destination {
  id?: number;
  nom: string;
  description: string;
  region: string;
  latitude: number;
  longitude: number;
  imageUrl: string;
  estPopulaire: boolean;
  nombreVisites: number;
  ville: string;
}


@Injectable({
  providedIn: 'root'
})
export class DestinationAdminService {
  private apiUrl = 'http://localhost:5278/api/destinations';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Destination[]> {
    return this.http.get<Destination[]>(this.apiUrl);
  }

  getById(id: number): Observable<Destination> {
    return this.http.get<Destination>(`${this.apiUrl}/${id}`);
  }

  create(destination: Destination): Observable<Destination> {
    return this.http.post<Destination>(this.apiUrl, destination);
  }

  update(id: number, destination: Destination): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, destination);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
