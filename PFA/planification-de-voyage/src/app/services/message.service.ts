import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Message {
  id?: number;
  expediteurId: number; // ✅ Supprimé l'accent pour correspondre au backend
  destinataireId: number;
  expediteurType: string; // ✅ Supprimé l'accent pour correspondre au backend
  contenu: string;
  dateEnvoi?: string;
  estLu?: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private apiUrl = 'http://localhost:5278/api/messages'; // ✅ Change selon ton backend

  constructor(private http: HttpClient) {}

  // Ajouter JWT à l'en-tête
  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token'); // ✅ Stocke le token après connexion
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`, // ✅ Correction de la syntaxe `${token}`
      'Content-Type': 'application/json'
    });
  }

  // Récupérer les messages d'un utilisateur
  getUserMessages(userId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/user/${userId}`, { headers: this.getHeaders() });
  }

  // Récupérer tous les messages pour l'admin
  getAdminMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/admin/messages`, { headers: this.getHeaders() });
  }

  // Envoyer un message
  sendMessage(message: Message): Observable<Message> {
    return this.http.post<Message>(`${this.apiUrl}/send`, message, { headers: this.getHeaders() });
  }

  // Marquer un message comme lu
  markAsRead(messageId: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/mark-as-read/${messageId}`, {}, { headers: this.getHeaders() });
  }
}
