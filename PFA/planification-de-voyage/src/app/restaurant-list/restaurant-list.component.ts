import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantService } from '../services/restaurant.service';

@Component({
  selector: 'app-restaurant-list',
  standalone: true,
  templateUrl: './restaurant-list.component.html',
  styleUrls: ['./restaurant-list.component.css'],
  imports: [CommonModule],
})
export class RestaurantListComponent implements OnInit {
  restaurants: any[] = [];

  constructor(private restaurantService: RestaurantService) {}

  ngOnInit(): void {
    this.restaurantService.getRestaurants().subscribe(
      (data) => {
        console.log('Données reçues:', data);
        this.restaurants = data;
      },
      (error) => {
        console.error('Erreur API:', error);
      }
    );
  }
}
