import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../../services/restaurant.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common'; // ✅ Importer CommonModule pour *ngIf et *ngFor

@Component({
  selector: 'app-restaurant-list',
  standalone: true,
  imports: [CommonModule], // ✅ Ajouter CommonModule ici pour activer *ngIf et *ngFor
  templateUrl: './restaurant-list.component.html',
  styleUrls: ['./restaurant-list.component.css']
})
export class RestaurantListComponent implements OnInit {
  restaurants: any[] = [];

  constructor(private restaurantService: RestaurantService, private router: Router) {}

  ngOnInit(): void {
    this.chargerRestaurants();
  }

  chargerRestaurants(): void {
    this.restaurantService.getRestaurants().subscribe(
      data => {
        console.log("Données reçues :", data); // 🔍 Vérifie si les restaurants sont bien reçus
        this.restaurants = data;
      },
      error => console.error("Erreur lors du chargement des restaurants :", error)
    );
  }

  ajouterRestaurant(): void {
    this.router.navigate(['/restaurant-form']); // Redirige vers le formulaire d'ajout
  }
  
  modifierRestaurant(id: number): void {
    this.router.navigate([`/restaurant-form/${id}`]);
  }

  supprimerRestaurant(id: number): void {
    if (confirm("Voulez-vous vraiment supprimer ce restaurant ?")) {
      this.restaurantService.supprimerRestaurant(id).subscribe(() => {
        this.chargerRestaurants(); // Recharge la liste après suppression
      });
    }
  }
}
