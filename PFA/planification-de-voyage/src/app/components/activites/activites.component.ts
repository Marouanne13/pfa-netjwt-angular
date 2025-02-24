import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActiviteService } from '../../services/activite.service';

@Component({
  selector: 'app-activites',
  standalone: true,
  templateUrl: './activites.component.html',
  styleUrls: ['./activites.component.css'],
  imports: [CommonModule, FormsModule]
})
export class ActivitesComponent implements OnInit {
  activites: any[] = [];
  destinations: any[] = []; // ✅ Ajout de la liste des destinations

  newActivite: any = this.getDefaultActivite();
  editingActivite: any = null;
  formActivite: any = this.getDefaultActivite(); // ✅ Variable unique pour le formulaire

  constructor(private activiteService: ActiviteService) {}

  ngOnInit(): void {
    this.loadActivites();
    this.loadDestinations(); // ✅ Charger les destinations
  }

  getDefaultActivite() {
    return {
      id: undefined,
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
      destinationId: null
    };
  }

  resetForm(): void {
    this.editingActivite = null;
    this.formActivite = this.getDefaultActivite();
  }

  loadActivites(): void {
    this.activiteService.getActivites().subscribe({
      next: (data) => this.activites = data,
      error: (error) => console.error("❌ Erreur chargement :", error)
    });
  }

  loadDestinations(): void {
    this.activiteService.getDestinations().subscribe({
      next: (data) => this.destinations = data, // ✅ Charger les destinations
      error: (error) => console.error("❌ Erreur chargement destinations :", error)
    });
  }

  addOrUpdateActivite(): void {
    if (this.editingActivite) {
      this.activiteService.updateActivite(this.formActivite.id, this.formActivite).subscribe({
        next: () => {
          alert("✅ Activité mise à jour !");
          this.loadActivites();
          this.resetForm();
        },
        error: (error) => console.error("❌ Erreur mise à jour :", error)
      });
    } else {
      this.activiteService.addActivite(this.formActivite).subscribe({
        next: () => {
          alert("✅ Activité ajoutée !");
          this.loadActivites();
          this.resetForm();
        },
        error: (error) => console.error("❌ Erreur ajout :", error)
      });
    }
  }

  startEdit(activite: any): void {
    this.editingActivite = activite;
    this.formActivite = { ...activite }; // ✅ Remplir `formActivite` avec l'activité existante
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
