import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

// ✅ Interface pour représenter un transport
export interface Transport {
  id?: number;
  capacite: number;
  estDisponible: boolean;
  typeTransport: string;
  prix: number;
  dateCreation?: string;
  nomCompagnie: string;
  modeDeTransport: string;
  numeroImmatriculation?: string;
  heureDepart: string;  // Format: HH:mm
  heureArrivee: string; // Format: HH:mm
  numeroServiceClient?: string;
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
export class TransportService {
  private apiUrl = 'http://localhost:5278/api/transport';
  private destinationsUrl = 'http://localhost:5278/api/destinations';

  constructor(private http: HttpClient) {}

  // ✅ Récupérer tous les transports
  getTransports(): Observable<Transport[]> {
    return this.http.get<Transport[]>(this.apiUrl).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Récupérer un seul transport par ID
  getTransportById(id: number): Observable<Transport> {
    return this.http.get<Transport>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Ajouter un transport (avec conversion des horaires)
  addTransport(transport: Transport): Observable<Transport> {
    const transportData = this.formatTransportData(transport);
    console.log("📤 Données envoyées (Ajout) :", transportData);

    return this.http.post<Transport>(this.apiUrl, transportData).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Modifier un transport (avec conversion des horaires)
  updateTransport(id: number, transport: Transport): Observable<Transport> {
    const transportData = this.formatTransportData(transport);
    console.log("📤 Données envoyées (Modification) :", transportData);

    return this.http.put<Transport>(`${this.apiUrl}/${id}`, transportData).pipe(
      catchError(this.handleError)
    );
  }

  // ✅ Supprimer un transport
  deleteTransport(id: number): Observable<void> {
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

  // ✅ Convertir les données du transport avant envoi
  private formatTransportData(transport: Transport): Transport {
    return {
      ...transport,
      heureDepart: this.convertToTimeSpan(transport.heureDepart),
      heureArrivee: this.convertToTimeSpan(transport.heureArrivee)
    };
  }

  // ✅ Convertit HH:mm en HH:mm:ss pour .NET
  private convertToTimeSpan(time: string): string {
    if (!time) return '00:00:00';
    return `${time}:00`;
  }

  // ✅ Gestion des erreurs HTTP
  private handleError(error: any) {
    console.error("❌ Erreur HTTP :", error);

    let errorMessage = 'Erreur serveur, réessayez plus tard.';
    if (error.status === 400) {
      console.log("📌 Détails de l'erreur API :", error.error);
      errorMessage = "🚨 Requête invalide : Vérifiez les données envoyées.";
    } else if (error.status === 404) {
      errorMessage = "❌ Ressource non trouvée.";
    } else if (error.status === 500) {
      errorMessage = "⚠️ Erreur interne du serveur.";
    }

    return throwError(() => new Error(errorMessage));
  }
}
