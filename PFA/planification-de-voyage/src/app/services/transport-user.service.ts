import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransportUserService {
  private apiUrl = 'http://localhost:5278/api/transport-user';

  constructor(private http: HttpClient) {}

  getTransportsParDestination(destinationId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/par-destination/${destinationId}`);
  }
}
