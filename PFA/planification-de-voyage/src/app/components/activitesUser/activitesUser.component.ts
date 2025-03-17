import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router'; // Importer ActivatedRoute
import { SessionService } from '../../services/session.service';
import { CommonModule } from '@angular/common';
import { ActiviteUserService } from '../../services/activite-user.service';  // Vérifie l'import

@Component({
  selector: 'app-activites-user',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './activitesUser.component.html',
  styleUrls: ['./activitesUser.component.css']
})
export class ActivitesUserComponent implements OnInit {

  activites: any[] = [];
  destinationId: string | null = null;

  constructor(
    private router: Router,
    private sessionService: SessionService,
    private activiteUserService: ActiviteUserService, // Injection du service
    private activatedRoute: ActivatedRoute // Injection d'ActivatedRoute
  ) {}

  ngOnInit() {
    this.destinationId = this.sessionService.getLocalStorage('destinationId');
  
    if (!this.destinationId) {
      console.error("❌ Aucun ID de destination trouvé !");
      return;
    }
  
    console.log("📍 Destination ID récupéré :", this.destinationId);
    this.loadActivites();
  }
  
  loadActivites() {
    if (!this.destinationId) return;
    this.activiteUserService.getActivitesParDestination(this.destinationId).subscribe({
      next: (data: any[]) => {
        this.activites = data;
        console.log("✅ Activités chargées :", this.activites);
      },
      error: (err: any) => {
        console.error("❌ Erreur chargement activités", err);
      }
    });
  }
  
  choisirActivite(id: number) {
    this.sessionService.setLocalStorage('activiteId', id.toString());
    console.log("🎭 Activité enregistrée en session (ID) :", id);

    // Redirection vers les restaurants
    this.router.navigate(['/restaurants']);
}
voirRestaurants() {
  this.router.navigate(['/restaurant-user']); // 🔥 Redirige vers la page des restaurants
}
}
