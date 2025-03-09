import { Component, OnInit } from '@angular/core';
import { MessageService, Message } from '../../services/message.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // ✅ Ajouté pour gérer ngModel et éviter les erreurs

@Component({
  selector: 'app-admin-messages',
  templateUrl: './admin-messages.component.html',
  styleUrls: ['./admin-messages.component.css'],
  standalone: true, // ✅ Sans module
  imports: [CommonModule, FormsModule] // ✅ Ajout pour *ngIf et ngModel
})
export class AdminMessagesComponent implements OnInit {
  messages: Message[] = [];
  selectedUserId?: number;
  responseMessage: string = ''; // ✅ Ajout pour stocker la réponse avant envoi

  constructor(private messageService: MessageService) {}

  ngOnInit(): void {
    this.loadMessages();
  }

  // Charger tous les messages des utilisateurs
  loadMessages(): void {
    this.messageService.getAdminMessages().subscribe(
      (data) => {
        this.messages = data;
      },
      (error) => console.error('Erreur lors du chargement des messages', error)
    );
  }

  // Sélectionner un utilisateur pour répondre
  selectUser(userId: number): void {
    this.selectedUserId = userId;
    this.responseMessage = ''; // ✅ Vider le champ de réponse lorsqu'on change d'utilisateur
  }

  // Envoyer une réponse
  sendResponse(): void {
    if (!this.responseMessage.trim() || !this.selectedUserId) return;

    const message: Message = {
      expediteurId: 2, // ✅ ID de l'admin
      destinataireId: this.selectedUserId,
      expediteurType: 'Admin',
      contenu: this.responseMessage
    };

    this.messageService.sendMessage(message).subscribe(() => {
      this.loadMessages();
      this.responseMessage = ''; // ✅ Vider le champ après envoi
    });
  }
}
