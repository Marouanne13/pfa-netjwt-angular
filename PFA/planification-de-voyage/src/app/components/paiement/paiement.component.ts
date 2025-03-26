import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { loadStripe } from '@stripe/stripe-js';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-paiement',
  templateUrl: './paiement.component.html'
})
export class PaiementComponent {
  constructor(private http: HttpClient) {}

  async payer() {
    const stripe = await loadStripe('pk_test_51R5yV6JjjakS6xpBeagbSYnlQ4d7AllVn0EGSMIj3jGc9hF0FlKqRg7wHqWy4lR1o6ymwnbPpAEen2WEI8f2ta5T00MmcmLNA2');

    if (!stripe) {
      Swal.fire({
        icon: 'error',
        title: 'Erreur Stripe',
        text: 'Impossible de charger Stripe.',
      });
      return;
    }

    this.http.post<any>('http://localhost:5278/api/paiement/create-checkout-session', {
      productName: 'Voyage test',
      amount: 99.99
    }).subscribe({
      next: async (res) => {
        if (res.sessionId) {
          const result = await stripe.redirectToCheckout({ sessionId: res.sessionId });

          if (result.error) {
            Swal.fire({
              icon: 'error',
              title: 'Paiement refusé ❌',
              text: result.error.message || 'Une erreur est survenue pendant le paiement.',
            });
          }
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Erreur de session',
            text: 'Aucune sessionId reçue depuis le backend.',
          });
        }
      },
      error: () => {
        Swal.fire({
          icon: 'error',
          title: 'Erreur API',
          text: 'Impossible de créer la session de paiement.',
        });
      }
    });
  }
}
