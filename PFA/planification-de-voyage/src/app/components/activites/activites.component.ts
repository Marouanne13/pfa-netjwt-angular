import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActiviteService } from '../../services/activite.service';

@Component({
  selector: 'app-activites',
  standalone: true,
  templateUrl: './activites.component.html',
  styleUrls: ['./activites.component.css'],
  imports: [CommonModule, FormsModule] // ✅ Assure que FormsModule est bien importé pour `ngModel`
})
export class ActivitesComponent {
  activites: any[] = [];
  newActivite: any = {
    nom: '',
    description: '',
    type: '',
    prix: null,
    duree: null,
    emplacement: '',
    evaluation: 0,
    nombreMaxParticipants: 0,
    estDisponible: true,
    dateDebut: '',
    dateFin: '',
    coordonneesContact: '',
    destinationId: null
  };

  constructor(private activiteService: ActiviteService) {
    this.loadActivites();
  }

  // ✅ Charger toutes les activités
  loadActivites() {
    this.activiteService.getActivites().subscribe({
      next: (data) => {
        this.activites = data;
      },
      error: (error) => {
        console.error("❌ Erreur lors du chargement des activités :", error);
        alert("❌ Problème lors du chargement des activités !");
      }
    });
  }

  // ✅ Ajouter une activité avec validation des champs
  addActivite() {
    if (!this.validateActivite(this.newActivite)) return;
  
    // ✅ Formatage des dates avant l'envoi
    this.newActivite.dateDebut = this.formatDate(this.newActivite.dateDebut);
    this.newActivite.dateFin = this.formatDate(this.newActivite.dateFin);
  
    this.activiteService.addActivite(this.newActivite).subscribe({
      next: () => {
        alert("✅ Activité ajoutée avec succès !");
        this.newActivite = {}; // Réinitialisation du formulaire
        this.loadActivites();
      },
      error: (error) => {
        console.error("❌ Erreur lors de l'ajout :", error);
        alert("❌ Erreur lors de l'ajout de l'activité !");
      }
    });
  }
  
  formatDate(dateString: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toISOString().slice(0, 19); // Format "YYYY-MM-DDTHH:mm:ss"
  }
  
  
  // ✅ Modifier une activité
  updateActivite(activite: any) {
    if (!this.validateActivite(activite)) return;

    this.activiteService.updateActivite(activite.id, activite).subscribe({
      next: () => {
        alert("✅ Activité mise à jour !");
        this.loadActivites();
      },
      error: (error) => {
        console.error("❌ Erreur de mise à jour :", error);
        alert("❌ Échec de la mise à jour !");
      }
    });
  }

  // ✅ Supprimer une activité
  deleteActivite(id: number) {
    if (!confirm("⚠️ Êtes-vous sûr de vouloir supprimer cette activité ?")) return;

    this.activiteService.deleteActivite(id).subscribe({
      next: () => {
        alert("✅ Activité supprimée !");
        this.loadActivites();
      },
      error: (error) => {
        console.error("❌ Erreur de suppression :", error);
        alert("❌ Impossible de supprimer cette activité !");
      }
    });
  }

  // ✅ Vérification des champs obligatoires
  validateActivite(activite: any): boolean {
    if (!activite.nom || !activite.description || !activite.type ||
        activite.prix == null || activite.duree == null ||
        !activite.emplacement || !activite.dateDebut || !activite.dateFin ||
        !activite.coordonneesContact || activite.destinationId == null) {
      alert("❌ Tous les champs sont obligatoires !");
      return false;
    }

    // ✅ Conversion des dates en format ISO avant envoi au backend
    activite.dateDebut = new Date(activite.dateDebut).toISOString();
    activite.dateFin = new Date(activite.dateFin).toISOString();

    return true;
  }
}
