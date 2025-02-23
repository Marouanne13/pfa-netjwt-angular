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

  constructor(private userService: UserService, private router: Router) {}

  onLogin() {
    if (!this.email || !this.password) {
      alert('❌ Veuillez remplir tous les champs.');
      return;
    }

    this.userService.login({ email: this.email, password: this.password }).subscribe({
      next: (response) => {
        console.log('✅ Connexion réussie :', response);
        
        this.userService.saveToken(response.token); // ✅ Stocker le token

        // ✅ Rediriger vers la page "home" après connexion réussie
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('❌ Erreur de connexion :', err);
        alert('Échec de connexion. Vérifiez vos identifiants.');
      }
    });
  }
}
