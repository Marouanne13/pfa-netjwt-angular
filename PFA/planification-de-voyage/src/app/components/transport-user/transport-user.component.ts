import { Component, OnInit } from '@angular/core';
import { PanierUserService } from '../../services/panier-user.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { TransportService } from '../../services/transport.service';

@Component({
  selector: 'app-transport-user',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transport-user.component.html',
  styleUrls: ['./transport-user.component.css'],
})
export class TransportUserComponent implements OnInit {
  userId: number = 0;
  transportId: number | null = null;
  destinationId: number | null = null;
  hebergementId: number | null = null;
  activiteId: number | null = null;
  restaurantId: number | null = null;

  transports: any[] = [];

  constructor(
    private panierUserService: PanierUserService,
    private transportService: TransportService,
    private router: Router
  ) {}

  ngOnInit() {
    this.recupererChoix();       // 🔁 Récupération des données utilisateur
    this.chargerTransports();    // 🚍 Chargement des transports
  }

  // 🔁 Récupération des valeurs en session/localStorage
  recupererChoix() {
    this.userId = Number(localStorage.getItem('userId')) || 0;
    this.destinationId = Number(localStorage.getItem('destinationId')) || null;
    this.hebergementId = Number(localStorage.getItem('hebergementId')) || null;
    this.activiteId = Number(localStorage.getItem('activiteId')) || null;
    this.restaurantId = null; // Tu as skippé cette étape
    this.transportId = null; // On attend le choix de l’utilisateur

    console.log("📦 Données récupérées depuis le localStorage :", {
      userId: this.userId,
      destinationId: this.destinationId,
      hebergementId: this.hebergementId,
      activiteId: this.activiteId,
      restaurantId: this.restaurantId,
      transportId: this.transportId
    });
  }

  // 🚍 Chargement des transports disponibles
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

  // 🚌 Enregistrer le transport sélectionné
  choisirTransport(id: number) {
    this.transportId = id;
    localStorage.setItem('transportId', id.toString());
    console.log("🚌 Transport sélectionné :", id);
  }

  // 📤 Envoi final au backend avec tous les choix
  ajouterToutAuPanier() {
    // Assurer que les données sont bien récupérées
    const panierData = {
      userId: this.userId,
      destinationId: this.destinationId,
      hebergementId: this.hebergementId,
      activiteId: this.activiteId,
      restaurantId: this.restaurantId,
      transportId: this.transportId
    };

    console.log("📤 Données envoyées au backend :", panierData);

    this.panierUserService.ajouterToutAuPanier(panierData).subscribe({
      next: (res) => {
        console.log("✅ Panier stocké avec succès :", res);
        alert("Panier enregistré avec tous les choix !");
        this.router.navigate(['/confirmation']);
      },
      error: (err: HttpErrorResponse) => {
        console.error("❌ Erreur lors de l'enregistrement du panier :", err.message);
      }
    });
  }
}
