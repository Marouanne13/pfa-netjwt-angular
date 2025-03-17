import { Component, OnInit } from '@angular/core';
import { RestaurantUserService } from '../../services/restaurant-user.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-restaurant-user',
  imports: [CommonModule],
  templateUrl: './restaurant-user.component.html',
  styleUrls: ['./restaurant-user.component.css']
})
export class RestaurantUserComponent implements OnInit {
  restaurants: any[] = [];

  constructor(
    private restaurantService: RestaurantUserService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadRestaurants();
  }

  loadRestaurants() {
    this.restaurantService.getRestaurants().subscribe({
      next: (data) => {
        this.restaurants = data;
        console.log("✅ Restaurants chargés :", this.restaurants);
      },
      error: (err) => {
        console.error("❌ Erreur chargement restaurants", err);
      }
    });
  }

  choisirRestaurant(id: number) {
    localStorage.setItem('restaurantId', id.toString());
    console.log("🎭 Restaurant enregistré en session (ID) :", id);
    
    // Redirection vers une autre page après le choix du restaurant
    this.router.navigate(['/transport']);
  }
}
