import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login-user',
  standalone: true,
  templateUrl: './login-user.component.html',
  styleUrls: ['./login-user.component.css'],
  imports: [CommonModule, FormsModule]
})
export class LoginUserComponent {
  email: string = '';
  password: string = '';
  isLoading: boolean = false; // ✅ Pour empêcher plusieurs clics
  errorMessage: string = ''; // ✅ Pour afficher les erreurs
  userId: number | null = null; // ✅ Stocker l'ID utilisateur après connexion

  constructor(private userService: UserService, private router: Router) {}

  onLogin() {
    if (!this.email || !this.password) {
      this.errorMessage = '❌ Veuillez remplir tous les champs.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.userService.login({ email: this.email, password: this.password }).subscribe({
      next: (response) => {
        console.log('✅ Connexion réussie :', response);

        // ✅ Stocker le token et l'ID utilisateur
        this.userService.saveToken(response.token, response.userId);
        this.userId = response.userId; // ✅ Afficher l'ID utilisateur

        // ✅ Rediriger vers "home" après 2 secondes
        setTimeout(() => this.router.navigate(['/home']), 2000);
      },
      error: (err) => {
        console.error('❌ Erreur de connexion :', err);
        this.errorMessage = 'Échec de connexion. Vérifiez vos identifiants.';
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}
