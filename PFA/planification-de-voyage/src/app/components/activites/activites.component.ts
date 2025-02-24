import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActiviteService, Activite, Destination } from '../../services/activite.service';

@Component({
  selector: 'app-activites',
  standalone: true,
  templateUrl: './activites.component.html',
  styleUrls: ['./activites.component.css'],
  imports: [CommonModule, FormsModule]
})
export class ActivitesComponent implements OnInit {
  activites: Activite[] = [];
  destinations: Destination[] = [];
  newActivite: Activite = this.getDefaultActivite();
  editingActivite: Activite | null = null;

  constructor(private activiteService: ActiviteService) {}

  ngOnInit(): void {
    this.loadActivites();
    this.loadDestinations();
  }

  getDefaultActivite(): Activite {
    return {
      nom: '',
      description: '',
      type: '',
      prix: 0,
      duree: 0,
      emplacement: '',
      evaluation: 0,
      nombreMaxParticipants: 0,
      estDisponible: true,
      dateDebut: '',
      dateFin: '',
      coordonneesContact: '',
      destinationId: 0
    };
  }

  resetForm(): void {
    this.newActivite = this.getDefaultActivite();
    this.editingActivite = null;
  }

  loadActivites(): void {
    this.activiteService.getActivites().subscribe({
      next: (data: Activite[]) => {
        this.activites = data;
      },
      error: (error) => {
        console.error("❌ Erreur de chargement :", error);
      }
    });
  }

  loadDestinations(): void {
    this.activiteService.getDestinations().subscribe({
      next: (data: Destination[]) => {
        this.destinations = data;
      },
      error: (error) => {
        console.error("❌ Erreur chargement destinations :", error);
      }
    });
  }

  addActivite(): void {
    this.activiteService.addActivite(this.newActivite).subscribe({
      next: () => {
        alert("✅ Activité ajoutée !");
        this.resetForm();
        this.loadActivites();
      },
      error: (error) => console.error("❌ Erreur ajout :", error)
    });
  }

  startEdit(activite: Activite): void {
    this.editingActivite = { ...activite }; // Copie pour éviter modification directe
  }

  updateActivite(): void {
    if (!this.editingActivite) return;
    this.activiteService.updateActivite(this.editingActivite.id!, this.editingActivite).subscribe({
      next: () => {
        alert("✅ Activité mise à jour !");
        this.resetForm();
        this.loadActivites();
      },
      error: (error) => console.error("❌ Erreur mise à jour :", error)
    });
  }

  deleteActivite(id: number): void {
    if (!confirm("⚠️ Confirmer la suppression ?")) return;
    this.activiteService.deleteActivite(id).subscribe({
      next: () => {
        alert("✅ Activité supprimée !");
        this.loadActivites();
      },
      error: (error) => console.error("❌ Erreur suppression :", error)
    });
  }
}
