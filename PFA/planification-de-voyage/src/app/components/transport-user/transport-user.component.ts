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
    this.recupererChoix();
    this.chargerTransports();
  }

  recupererChoix() {
    this.userId = Number(localStorage.getItem('userId')) || 0;
    this.destinationId = Number(localStorage.getItem('destinationId')) || null;
    this.hebergementId = Number(localStorage.getItem('hebergementId')) || null;
    this.activiteId = Number(localStorage.getItem('activiteId')) || null;
    this.restaurantId = null; // Skipped volontairement
    this.transportId = null;

    console.log("📦 Données récupérées :", {
      userId: this.userId,
      destinationId: this.destinationId,
      hebergementId: this.hebergementId,
      activiteId: this.activiteId,
      restaurantId: this.restaurantId,
      transportId: this.transportId
    });
  }

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

  choisirTransport(id: number) {
    this.transportId = id;
    localStorage.setItem('transportId', id.toString());
    console.log("🚌 Transport sélectionné :", id);
  }

  ajouterToutAuPanier() {
    const panierData = {
      userId: this.userId,
      destinationId: this.destinationId,
      hebergementId: this.hebergementId,
      activiteId: this.activiteId,
      restaurantId: this.restaurantId,
      transportId: this.transportId
    };

    console.log("📤 Données envoyées au backend :", panierData);

    this.panierUserService.ajouterAuPanier(panierData).subscribe({
      next: (res: any) => {
        console.log("✅ Panier enregistré :", res);

        // ✅ Redirection vers page de paiement après ajout au panier
        this.router.navigate(['/paiement']);
      },
      error: (err: HttpErrorResponse) => {
        console.error("❌ Erreur lors de l'ajout au panier :", err);
      }
    });
  }
}
