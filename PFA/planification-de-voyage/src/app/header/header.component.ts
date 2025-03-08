import { Component, OnInit } from '@angular/core';  // ✅ Ajoute OnInit
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
 
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {  // ✅ Implémente OnInit
  isAuthenticated: boolean = false;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {  // ✅ Implémente la méthode OnInit
    this.checkAuthentication();
  }

  checkAuthentication() {
    this.isAuthenticated = this.userService.isAuthenticated();
  }

  logout() {
    this.userService.logout();
    this.isAuthenticated = false;
    this.router.navigate(['/loginUser']); // Rediriger après déconnexion
  }
}
