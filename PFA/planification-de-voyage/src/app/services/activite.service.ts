import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

// ✅ Interface pour représenter une activité
export interface Activite {
  id?: number;
  nom: string;
  description: string;
  type: string;
  prix: number;
  duree: number;
  emplacement: string;
  evaluation: number;
  nombreMaxParticipants: number;
  estDisponible: boolean;
  dateDebut: string;
  dateFin: string;
  coordonneesContact: string;
  destinationId: number;
}

// ✅ Interface pour représenter une destination
export interface Destination {
  id: number;
  nom: string;
}

@Injectable({
  providedIn: 'root'
})
export class ActiviteService {
  private apiUrl = 'http://localhost:5278/api/activite';
  private destinationsUrl = 'http://localhost:5278/api/destinations';

  constructor(private http: HttpClient) {}

  // ✅ Récupérer toutes les activités
  getActivites(): Observable<Activite[]> {
    return this.http.get<Activite[]>(this.apiUrl).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Récupérer une seule activité par ID
  getActiviteById(id: number): Observable<Activite> {
    return this.http.get<Activite>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Ajouter une activité
  addActivite(activite: Activite): Observable<Activite> {
    return this.http.post<Activite>(this.apiUrl, activite).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Modifier une activité
  updateActivite(id: number, activite: Activite): Observable<Activite> {
    return this.http.put<Activite>(`${this.apiUrl}/${id}`, activite).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Supprimer une activité
  deleteActivite(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Récupérer les destinations disponibles
  getDestinations(): Observable<Destination[]> {
    return this.http.get<Destination[]>(this.destinationsUrl).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Gestion des erreurs HTTP
  private handleError(error: any) {
    console.error("❌ Erreur HTTP :", error);
    return throwError(() => new Error('Erreur serveur, réessayez plus tard.'));
  }
}
