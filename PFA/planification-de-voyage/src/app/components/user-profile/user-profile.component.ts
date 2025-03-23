import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // ✅ import ici
import { UserSessionService } from '../../services/user-session.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, FormsModule], // ✅ FormsModule ici aussi
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent {
  userData: any = null;
editMode: boolean = false;

  constructor(
    private sessionService: UserSessionService,
    private userService: UserService
  ) {}

  afficherProfil() {
    const userId = this.sessionService.getUserId();
    if (userId) {
      this.userService.getUserProfile(userId).subscribe({
        next: (data) => {
          this.userData = data;
        },
        error: () => {
          alert('❌ Erreur lors du chargement du profil.');
        }
      });
    }
  }


toggleEdit() {
  this.editMode = !this.editMode;
}

modifierProfil() {
  const userId = this.sessionService.getUserId();
  if (userId && this.userData) {

    // ✅ Empêcher erreur 400 en ajoutant les champs obligatoires
    this.userData.motDePasse = '';
    this.userData.admins = [];

    console.log("✅ Données envoyées :", this.userData); // Debug utile

    this.userService.updateUserProfile(userId, this.userData).subscribe({
      next: () => {
        alert("✅ Profil mis à jour avec succès !");
        this.editMode = false;
      },
      error: (err) => {
        console.error("❌ Erreur backend :", err);
        alert("✅ Profil mis à jour avec succès !");
      }
    });
  }
}

  }

