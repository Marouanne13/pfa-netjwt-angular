import { Component, OnInit } from '@angular/core';
import { PanierUserService } from '../../services/panier-user.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { TransportService } from '../../services/transport.service'; // Import du service

@Component({
  selector: 'app-transport-user',
  imports: [CommonModule],
  templateUrl: './transport-user.component.html',
  styleUrls: ['./transport-user.component.css'],
})
export class TransportUserComponent implements OnInit {
  userId: number = 1; // ID de l'utilisateur connecté (à récupérer dynamiquement)
  transportId: number | null = null;
  destinationId: number | null = null;
  hebergementId: number | null = null;
  activiteId: number | null = null;
  restaurantId: number | null = null;

  transports: any[] = []; // ✅ Ajout de la liste des transports

  constructor(
    private panierUserService: PanierUserService,
    private transportService: TransportService, // Service pour récupérer les transports
    private router: Router
  ) {}

  ngOnInit() {
    this.recupererChoix();
    this.chargerTransports(); // ✅ Charger les transports disponibles
  }

  // ✅ Récupérer tous les choix de l'utilisateur stockés en session
  recupererChoix() {
    this.destinationId = Number(localStorage.getItem('destinationId')) || null;
    this.hebergementId = Number(localStorage.getItem('hebergementId')) || null;
    this.activiteId = Number(localStorage.getItem('activiteId')) || null;
    this.restaurantId = Number(localStorage.getItem('restaurantId')) || null;
    this.transportId = Number(localStorage.getItem('transportId')) || null;
  }

  // ✅ Charger les transports depuis l'API
  chargerTransports() {
    this.transportService.getTransports().subscribe({
      next: (data) => {
        this.transports = data;
        console.log("✅ Transports chargés :", this.transports);
      },
      error: (error) => {
        console.error("❌ Erreur chargement transports", error);
      }
    });
  }

  // ✅ Ajouter tout dans le panier
  ajouterToutAuPanier() {
    const panierData = {
      userId: this.userId,
      destinationId: this.destinationId,
      hebergementId: this.hebergementId,
      activiteId: this.activiteId,
      restaurantId: this.restaurantId,
      transportId: this.transportId,
    };

    this.panierUserService.ajouterToutAuPanier(panierData).subscribe({
      next: (response: any) => {
        console.log("✅ Panier enregistré avec succès :", response);
        alert("Panier enregistré avec succès !");
        this.router.navigate(['/confirmation']);
      },
      error: (error: HttpErrorResponse) => {
        console.error("❌ Erreur lors de l'enregistrement du panier", error.message);
      }
    });
  }
}
