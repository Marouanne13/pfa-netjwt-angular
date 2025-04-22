import { Component, OnInit } from '@angular/core';
import { RestaurantUserService } from '../../services/restaurant-user.service';
import { CommonModule } from '@angular/common';
import { FooterComponent } from "../../footer/footer.component";

@Component({
  selector: 'app-restaurant-user',
  standalone: true,
  imports: [CommonModule, FooterComponent],
  templateUrl: './restaurant-user.component.html',
  styleUrls: ['./restaurant-user.component.css']
})
export class RestaurantUserComponent implements OnInit {
  restaurants: any[] = [];

  constructor(private restoService: RestaurantUserService) {}

  ngOnInit(): void {
    this.loadRestaurants();
    
  }
  getRandomImage(id: number): string {
    const images = [
     // 'https://images.unsplash.com/photo-1555396273-367ea4eb4db5?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1552566626-52f8b828add9?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1579583765668-648f1bd0eb3b?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-16080891964599-f61ba0e24092?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?auto=format&fit=crop&w=800&q=80',
      'https://images.unsplash.com/photo-1498654896293-37aacf113fd9?auto=format&fit=crop&w=800&q=80'
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
  onSearchInput(input: HTMLInputElement) {
    const query = input.value;
    this.onSearch(query);
  }
  
}
