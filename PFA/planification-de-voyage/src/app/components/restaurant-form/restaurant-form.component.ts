import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RestaurantService } from '../../services/restaurant.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-restaurant-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
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
    utilisateurId: 1
  };

  restaurantId: number | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  selectedFile: File | null = null;

  constructor(
    private restaurantService: RestaurantService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.restaurantId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.restaurantId) {
      this.restaurantService.getRestaurantById(this.restaurantId).subscribe(data => {
        if (data) {
          this.restaurant = {
            ...data,
            livraisonDisponible: !!data.livraisonDisponible,
            reservationEnLigne: !!data.reservationEnLigne,
            estOuvert24h: !!data.estOuvert24h
          };
          console.log("Données récupérées :", this.restaurant);
        }
      }, error => {
        console.error("Erreur lors de la récupération du restaurant :", error);
      });
    }
  }

  submit(): void {
    console.log("Données envoyées avant modification :", this.restaurant);

    const restaurantModifie = { ...this.restaurant };
    delete restaurantModifie.utilisateur; // 🚨 Supprimer l'objet utilisateur avant d'envoyer la requête

    console.log("Données après suppression de `utilisateur` :", restaurantModifie);

    if (this.restaurantId) {
        console.log("Modification du restaurant :", restaurantModifie);
        this.restaurantService.modifierRestaurant(this.restaurantId, restaurantModifie).subscribe(
            response => {
                console.log("Restaurant modifié avec succès !", response);
                this.router.navigate(['/restaurant-list']);
            },
            error => {
                console.error("Erreur lors de la modification du restaurant :", error);
            }
        );
    } else {
        this.restaurantService.addRestaurant(restaurantModifie).subscribe(
            response => {
                console.log("Restaurant ajouté avec succès !", response);
                this.router.navigate(['/restaurant-list']);
            },
            error => {
                console.error("Erreur lors de l'ajout du restaurant :", error);
            }
        );
    }
}


  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result;
      };
      reader.readAsDataURL(file);
    }
  }
}
