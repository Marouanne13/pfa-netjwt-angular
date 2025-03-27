import { Component, OnInit } from '@angular/core';
import { MessageService, Message } from '../../services/message.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-message',
  templateUrl: './user-message.component.html',
  styleUrls: ['./user-message.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class UserMessageComponent implements OnInit {
  messages: Message[] = [];
  newMessage: string = '';
  userId: number = 1; // À remplacer par l’ID réel de l’utilisateur (JWT)
  showChat: boolean = false;

  constructor(private messageService: MessageService) {}

  ngOnInit(): void {
    this.loadMessages();
  }

  toggleChat(): void {
    this.showChat = !this.showChat;
  }

  loadMessages(): void {
    this.messageService.getUserMessages(this.userId).subscribe(
      (data) => {
        this.messages = data;
      },
      (error) => console.error('Erreur lors du chargement des messages', error)
    );
  }

  sendMessage(): void {
    if (!this.newMessage.trim()) return;

    const message: Message = {
      expediteurId: this.userId,
      destinataireId: 2, // ID de l’admin
      expediteurType: 'User',
      contenu: this.newMessage
    };

    this.messageService.sendMessage(message).subscribe(() => {
      this.loadMessages();
      this.newMessage = '';
    });
  }
}
