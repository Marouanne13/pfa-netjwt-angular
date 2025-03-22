import { Component, OnInit } from '@angular/core';
import { PanierUserService } from '../../services/panier-user.service';
import { SessionService } from '../../services/session.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-panier-user',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './panier-user.component.html',
  styleUrls: ['./panier-user.component.css']
})
export class PanierUserComponent implements OnInit {
  panier: any[] = [];
  userId: number = 1; // ⚠️ Modifier avec l'authentification dynamique

  constructor(
    private panierService: PanierUserService,
    private sessionService: SessionService
  ) {}

  ngOnInit() {
    this.loadPanier();
  }

  loadPanier() {
    this.panierService.getPanier(this.userId).subscribe({
      next: (data) => {
        this.panier = data;
        console.log("✅ Panier chargé :", this.panier);
      },
      error: (err) => {
        console.error("❌ Erreur chargement panier", err);
      }
    });
  }

  supprimerElement(id: number) {
    this.panierService.supprimerDuPanier(id).subscribe(() => {
      this.panier = this.panier.filter(item => item.id !== id);
      console.log("🗑️ Élément supprimé !");
    });
  }

  viderPanier() {
    this.panierService.viderPanier(this.userId).subscribe(() => {
      this.panier = [];
      console.log("🗑️ Panier vidé !");
    });
  }
}
