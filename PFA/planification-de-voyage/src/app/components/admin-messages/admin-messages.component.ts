import { Component, OnInit } from '@angular/core';
import { MessageService, Message } from '../../services/message.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-messages',
  standalone: true,
  templateUrl: './admin-messages.component.html',
  styleUrls: ['./admin-messages.component.css'],
  imports: [CommonModule, FormsModule]
})
export class AdminMessagesComponent implements OnInit {
  messages: Message[] = [];
  selectedUserId?: number;
  responseMessage: string = '';

  constructor(private messageService: MessageService) {}

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages(): void {
    this.messageService.getAdminMessages().subscribe({
      next: (data) => this.messages = data,
      error: (err) => console.error('Erreur chargement messages', err)
    });
  }

  selectUser(userId: number): void {
    this.selectedUserId = userId;
    this.responseMessage = '';
  }

  sendResponse(): void {
    if (!this.responseMessage.trim() || !this.selectedUserId) return;

    const message: Message = {
      expediteurId: 2,
      destinataireId: this.selectedUserId,
      expediteurType: 'Admin',
      contenu: this.responseMessage
    };

    this.messageService.sendMessage(message).subscribe(() => {
      this.loadMessages();
      this.responseMessage = '';
    });
  }

  getMessagesForSelectedUser(): Message[] {
    return this.messages.filter(m =>
      m.expediteurId === this.selectedUserId || m.destinataireId === this.selectedUserId
    );
  }

  getUsers(): number[] {
    const ids = this.messages
      .filter(m => m.expediteurType === 'User')
      .map(m => m.expediteurId);
    return Array.from(new Set(ids));
  }
}
