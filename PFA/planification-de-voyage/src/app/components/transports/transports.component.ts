import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TransportService, Transport, Destination } from '../../services/transport.service';

@Component({
  selector: 'app-transports',
  standalone: true,
  templateUrl: './transports.component.html',
  styleUrls: ['./transports.component.css'],
  imports: [CommonModule, FormsModule]
})
export class TransportsComponent implements OnInit {
  transports: Transport[] = [];
  destinations: Destination[] = [];
  editingTransport: Transport | null = null;
  formTransport: Transport = this.getDefaultTransport();

  constructor(private transportService: TransportService) {}

  ngOnInit(): void {
    this.loadTransports();
    this.loadDestinations();
  }

  getDefaultTransport(): Transport {
    return {
      capacite: 0,
      estDisponible: true,
      typeTransport: '',
      prix: 0,
      dateCreation: new Date().toISOString(), // Valeur actuelle en ISO
      nomCompagnie: '',
      modeDeTransport: '',
      numeroImmatriculation: '',
      heureDepart: '00:00', // Format HH:mm pour Angular
      heureArrivee: '00:00', // Format HH:mm
      numeroServiceClient: '',
      destinationId: 0
    };
  }

  resetForm(): void {
    this.editingTransport = null;
    this.formTransport = this.getDefaultTransport();
  }

  loadTransports(): void {
    this.transportService.getTransports().subscribe({
      next: (data) => this.transports = data,
      error: (error) => console.error("❌ Erreur chargement transports :", error)
    });
  }

  loadDestinations(): void {
    this.transportService.getDestinations().subscribe({
      next: (data) => this.destinations = data,
      error: (error) => console.error("❌ Erreur chargement destinations :", error)
    });
  }

  addOrUpdateTransport(): void {
    console.log("📤 Données envoyées :", this.formTransport);

    if (this.editingTransport) {
      this.transportService.updateTransport(this.formTransport.id!, this.formTransport).subscribe({
        next: () => {
          alert("✅ Transport mis à jour !");
          this.loadTransports();
          this.resetForm();
        },
        error: (error) => console.error("❌ Erreur mise à jour :", error)
      });
    } else {
      this.transportService.addTransport(this.formTransport).subscribe({
        next: () => {
          alert("✅ Transport ajouté !");
          this.loadTransports();
          this.resetForm();
        },
        error: (error) => console.error("❌ Erreur ajout :", error)
      });
    }
  }

  startEdit(transport: Transport): void {
    this.editingTransport = transport;
    this.formTransport = { ...transport };
  }

  deleteTransport(id: number): void {
    if (!confirm("⚠️ Confirmer la suppression ?")) return;
    this.transportService.deleteTransport(id).subscribe({
      next: () => {
        alert("✅ Transport supprimé !");
        this.loadTransports();
      },
      error: (error) => console.error("❌ Erreur suppression :", error)
    });
  }
}
