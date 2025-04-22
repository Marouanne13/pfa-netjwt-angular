import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { loadStripe } from '@stripe/stripe-js';
import Swal from 'sweetalert2';
import { NgIf, NgFor, CommonModule } from '@angular/common';
import { PanierUserService } from '../../services/panier-user.service';

@Component({
  selector: 'app-paiement',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './paiement.component.html',
  styleUrls: ['./paiement.component.css']
})
export class PaiementComponent implements OnInit {
  userId: number = 0;
  panierFiltre: any[] = [];
  total: number = 0;

  constructor(private http: HttpClient, private panierService: PanierUserService) {}

  ngOnInit(): void {
    this.userId = Number(localStorage.getItem('userId')) || 0;
    this.chargerPanier();
  }

  chargerPanier() {
    this.panierService.getPanier(this.userId).subscribe({
      next: (res) => {
        console.log("✅ Panier complet :", res);
        // Ne prendre que la dernière ligne
        this.panierFiltre = res.length ? [res[res.length - 1]] : [];
        console.log("🧾 Panier filtré :", this.panierFiltre);
        this.calculerTotal();
      },
      error: (err) => {
        console.error("❌ Erreur récupération panier :", err);
      }
    });
  }

  calculerTotal() {
    this.http.get<number>(`http://localhost:5278/api/panier/total/${this.userId}`).subscribe({
      next: (res) => {
        this.total = res;
        console.log("💰 Total corrigé :", this.total);
      },
      error: () => {
        this.total = 0;
      }
    });
  }

  async payer() {
    const stripe = await loadStripe('pk_test_51R5yV6JjjakS6xpBeagbSYnlQ4d7AllVn0EGSMIj3jGc9hF0FlKqRg7wHqWy4lR1o6ymwnbPpAEen2WEI8f2ta5T00MmcmLNA2'); // Remplace par ta clé publique

    if (!stripe) {
      Swal.fire({ icon: 'error', title: 'Erreur Stripe', text: 'Stripe non initialisé' });
      return;
    }

    this.http.post<any>(`http://localhost:5278/api/paiement/create-checkout-session/${this.userId}`, {}).subscribe({
      next: async (res) => {
        if (res.sessionId) {
          await stripe.redirectToCheckout({ sessionId: res.sessionId });
        } else {
          Swal.fire({ icon: 'error', title: 'Erreur session', text: 'Session ID manquant' });
        }
      },
      error: () => {
        Swal.fire({ icon: 'error', title: 'Erreur API', text: 'Impossible de créer une session Stripe' });
      }
    });
  }
}
