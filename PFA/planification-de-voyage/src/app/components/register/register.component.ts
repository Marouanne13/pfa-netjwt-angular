import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [CommonModule, FormsModule]
})
export class RegisterComponent {
  nom: string = '';
  email: string = '';
  password: string = '';
  telephone: string = '';
  adresse: string = '';
  dateDeNaissance: string = '';
  genre: string = '';

  constructor(private userService: UserService, private router: Router) {}

  goToLogin() {
    this.router.navigate(['/login']);
  }
  onRegister() {
    if (!this.nom || !this.email || !this.password || !this.telephone || !this.adresse || !this.dateDeNaissance || !this.genre) {
      alert('Veuillez remplir tous les champs.');
      return;
    }

  
 

    const userData = {
      nom: this.nom,
      email: this.email,
      password: this.password,
      telephone: this.telephone,
      adresse: this.adresse,
      dateDeNaissance: this.dateDeNaissance,
      genre: this.genre
    };

    this.userService.register(userData).subscribe({
      next: () => {
        alert('Inscription réussie ! Connectez-vous maintenant.');
        this.router.navigate(['/loginUser']);
      },
      error: (err) => {
        console.error('❌ Erreur d\'inscription :', err);
        alert('Erreur lors de l\'inscription.');
      }
    });
  }
}
