import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from '../../services/session.service';
import { CommonModule } from '@angular/common';
import { ActiviteUserService } from '../../services/activite-user.service';

@Component({
  selector: 'app-activites-user',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './activitesUser.component.html',
  styleUrls: ['./activitesUser.component.css']
})
export class ActivitesUserComponent implements OnInit {
  activites: any[] = [];
  destinationId: number | null = null;

  constructor(
    private router: Router,
    private sessionService: SessionService,
    private activiteUserService: ActiviteUserService
  ) {}

  ngOnInit() {
    this.destinationId = Number(this.sessionService.getLocalStorage('destinationId'));

    if (!this.destinationId || isNaN(this.destinationId)) {
      console.error("❌ Aucun ID de destination valide trouvé !");
      return;
    }

    console.log("📍 Destination ID récupéré :", this.destinationId);
    this.loadActivites();
  }

  loadActivites() {
    this.activiteUserService.getActivitesParDestination(this.destinationId!.toString()).subscribe({
      next: (data: any[]) => {
        this.activites = data;
        console.log("✅ Activités chargées :", this.activites);
      },
      error: (err) => {
        console.error("❌ Erreur chargement activités", err);
      }
    });
  }
  

  choisirActivite(activiteId: number) {
    this.sessionService.setLocalStorage('activiteId', activiteId.toString());
    console.log("🎯 Activité enregistrée dans localStorage :", activiteId);

    this.router.navigate(['/transport-user']);
  }
  choisirTransport() {
    this.router.navigate(['/transport-user']);
  }
}
