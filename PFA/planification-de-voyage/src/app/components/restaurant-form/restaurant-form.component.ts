import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RestaurantService } from '../../services/restaurant.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-restaurant-form',
  standalone: true,
  imports: [CommonModule, FormsModule], // Importation nécessaire pour ngModel
  templateUrl: './restaurant-form.component.html',
  styleUrls: ['./restaurant-form.component.css']
})
export class RestaurantFormComponent {
  restaurant: any = {
    nom: '',
    typeCuisine: '',
    adresse: '',
    numeroTelephone: '',
    nombreEtoiles: 1,
    description: '',
    imageUrl: '',
    livraisonDisponible: false,
    reservationEnLigne: false,
    estOuvert24h: false,
    utilisateurId: 1 // ✅ Ajoute un ID temporaire
  };  


  restaurantId: number | null = null;

  constructor(
    private restaurantService: RestaurantService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.restaurantId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.restaurantId) {
      this.restaurantService.getRestaurantById(this.restaurantId).subscribe(data => {
        this.restaurant = data;
      });
    }
  }

  submit(): void {
    console.log("Données envoyées :", this.restaurant); // Vérifie les champs avant l'envoi
  
    this.restaurantService.addRestaurant(this.restaurant).subscribe(
      response => {
        console.log("Restaurant ajouté avec succès !", response);
        this.router.navigate(['/restaurants']);
      },
      error => {
        console.error("Erreur lors de l'ajout du restaurant :", error);
      }
    );
  }
  imagePreview: string | ArrayBuffer | null = null;
selectedFile: File | null = null;

onFileSelected(event: any) {
  const file = event.target.files[0];
  if (file) {
    this.selectedFile = file;

    // Générer un aperçu de l'image
    const reader = new FileReader();
    reader.onload = () => {
      this.imagePreview = reader.result;
    };
    reader.readAsDataURL(file);
  }
}

  
}
