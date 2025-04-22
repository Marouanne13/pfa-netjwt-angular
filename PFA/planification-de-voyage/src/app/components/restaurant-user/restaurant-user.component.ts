import { Component, OnInit } from '@angular/core';
import { RestaurantUserService } from '../../services/restaurant-user.service';
import { CommonModule } from '@angular/common';
import { FooterComponent } from "../../footer/footer.component";
import { Router } from '@angular/router';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-restaurant-user',
  standalone: true,
  imports: [CommonModule, FooterComponent],
  templateUrl: './restaurant-user.component.html',
  styleUrls: ['./restaurant-user.component.css']
})
export class RestaurantUserComponent implements OnInit {
  restaurants: any[] = [];

  constructor(private restoService: RestaurantUserService ,private router: Router) {}

  ngOnInit(): void {
    this.loadRestaurants();
    
    
  }
  
  voirPlanning() {
    const stored = localStorage.getItem('restaurants_temp');
    const restaurants = stored ? JSON.parse(stored) : [];
  
    if (restaurants.length === 0) {
      Swal.fire({
        icon: 'warning',
        title: 'Aucun restaurant',
        text: 'Aucun restaurant n’a été ajouté au planning.'
      });
    } else {
      Swal.fire({
        icon: 'info',
        title: 'Restaurants sélectionnés',
        html: restaurants.map((r: any) => `<li><strong>${r.nom}</strong> (${r.adresse})</li>`).join(''),
        confirmButtonText: 'OK'
      });
    }
  }
  
  ajouterAuPlanning(resto: any) {
    const stored = localStorage.getItem('restaurants_temp');
    const restaurants = stored ? JSON.parse(stored) : [];
  
    restaurants.push(resto);
    localStorage.setItem('restaurants_temp', JSON.stringify(restaurants));
  
    Swal.fire({
      icon: 'success',
      title: 'Ajouté au planning !',
      html: `<strong>${resto.nom}</strong><br>${resto.adresse}<br><em>${resto.typeCuisine}</em>`
    });
  }
  
  viderPlanning() {
    localStorage.removeItem('restaurants_temp');
    Swal.fire({
      icon: 'info',
      title: 'Planning vidé',
      text: 'Tous les restaurants ont été retirés du planning.'
    });
  }
  
  
  
  
  
  getRandomImage(id: number): string {
    const images = [
      'https://images.unsplash.com/photo-1552566626-52f8b828add9?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1579583765668-648f1bd0eb3b?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1600891964599-f61ba0e24092?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1498654896293-37aacf113fd9?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1613145991742-b56c3fcbc205?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1559339352-11c53f59927c?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1504674900247-0877df9cc836?auto=format&fit=crop&w=800&q=80'
    ];
    return images[id % images.length];
  }
  
  
  

  loadRestaurants() {
    
    this.restoService.getRestaurants().subscribe((data) => {
      this.restaurants = data;
    });
  }

  onSearch(query: string) {
    if (query.trim() === '') {
      this.loadRestaurants(); // Recharge tout si vide
    } else {
      this.restoService.searchRestaurants(query).subscribe((data) => {
        this.restaurants = data;
      });
    }
  }
  onSearchInput(target: EventTarget | null) {
    const input = target as HTMLInputElement;
    const query = input.value;
    this.onSearch(query);
  }
 
  retourAccueil() {
    this.router.navigate(['/home']); // 🔁 Redirige vers ta route d'accueil
  }
  
}
