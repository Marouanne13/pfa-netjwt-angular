import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,  // ✅ Angular standalone component
  imports: [FormsModule],  // ✅ Ajoute FormsModule ici
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService) {}

  onLogin() {
    this.authService.login(this.email, this.password);
  }
}
