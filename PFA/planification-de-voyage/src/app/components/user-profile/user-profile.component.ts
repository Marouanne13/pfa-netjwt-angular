import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { UserSessionService } from '../../services/user-session.service';
import { UserService } from '../../services/user.service';
import { UserMessageComponent } from '../../components/user-message/user-message.component';
import { FooterComponent } from "../../footer/footer.component";

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, FormsModule, UserMessageComponent, FooterComponent],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  userData: any = null;
  editMode: boolean = false;
  profileImageUrl: string = ''; // ✅ Pour afficher l’image dynamiquement

  constructor(
    private sessionService: UserSessionService,
    private userService: UserService,
    private location: Location,
    private router: Router
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
          this.setProfileImage(); // ✅ Définir l’image selon le genre
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
          this.setProfileImage(); // ✅ Mettre à jour si le genre change
          this.router.navigate(['/home']);
        },
        error: (err) => {
          console.error("❌ Erreur backend :", err);
          alert("❌ Échec de la mise à jour du profil !");
        }
      });
    }
  }

  // ✅ Ajout de l’image selon le genre
  setProfileImage() {
    const genre = this.userData?.genre;
    console.log("📸 Genre détecté :", genre); // Debug
    if (genre === 'Femme') {
      this.profileImageUrl = '../../../assets/img/femme.jpg';
    } else if (genre === 'Homme') {
      this.profileImageUrl = '../../../assets/img/homme.jpg';
    }

    console.log("🖼️ Image définie :", this.profileImageUrl);
  }

}
