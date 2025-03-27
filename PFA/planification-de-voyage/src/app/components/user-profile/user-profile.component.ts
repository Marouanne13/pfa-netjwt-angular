import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router'; // ✅ Import du router
import { Location } from '@angular/common';
import { UserSessionService } from '../../services/user-session.service';
import { UserService } from '../../services/user.service';
import { UserMessageComponent } from '../../components/user-message/user-message.component';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, FormsModule, UserMessageComponent],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent {
  userData: any = null;
  editMode: boolean = false;

  constructor(
    private sessionService: UserSessionService,
    private userService: UserService,
    private location: Location,
    private router: Router // ✅ Injecté ici
  ) {}

  ngOnInit(): void {
    this.afficherProfil();
  }

  retour(): void {
    this.location.back();
  }

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
      const updatedUser = {
        ...this.userData,
        motDePasse: '',
        admins: []
      };

      this.userService.updateUserProfile(userId, updatedUser).subscribe({
        next: () => {
          alert("✅ Profil mis à jour avec succès !");
          this.editMode = false;

          // ✅ Redirection vers la home après mise à jour
          this.router.navigate(['/home']);
        },
        error: (err) => {
          console.error("❌ Erreur backend :", err);
          alert("❌ Échec de la mise à jour du profil !");
        }
      });
    }
  }
}
