import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface User {
  id: number;
  nom: string;
  email: string;
  telephone: string;
  adresse: string;
}

@Injectable({
  providedIn: 'root'
})
export class ClientsService {
  public apiUrl = 'http://localhost:5278/api/Client'; // ✅ Vérification de l'URL API

  constructor(private http: HttpClient) {}

  getClients(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  updateClient(id: number, client: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, client); // ✅ Correction avec backticks
  }

  deleteClient(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`); // ✅ Correction avec backticks
  }
}
