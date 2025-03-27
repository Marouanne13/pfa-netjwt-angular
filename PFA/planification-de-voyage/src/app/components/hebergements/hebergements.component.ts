import { Component, OnInit } from '@angular/core';
import { HebergementService } from '../../services/hebergement.service';
import { SessionService } from '../../services/session.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { routes } from '../../app.routes';
import { PanierUserService } from '../../services/panier-user.service'; 

@Component({
  selector: 'app-hebergements',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hebergements.component.html',
  styleUrls: ['./hebergements.component.css']
})
export class HebergementsComponent implements OnInit {
  hebergements: any[] = [];
  destinationId: number | null = null;

  constructor(
    private hebergementService: HebergementService, // ✅ Injection du service des hébergements
    private sessionService: SessionService, // ✅ Service session
    private router: Router ,
    private panierService: PanierUserService// ✅ Pour la navigation après choix de l'hébergement
  ) {}

  ngOnInit() {
    // Récupération depuis la session et conversion en nombre
    this.destinationId = Number(this.sessionService.getLocalStorage('destinationId'));
  
    if (!this.destinationId || isNaN(this.destinationId)) {
      console.error("❌ Aucun ID de destination valide trouvé !");
      return;
    }
  
    console.log("📌 Destination ID récupéré :", this.destinationId);
  
    this.loadHebergements();
  }
  
  loadHebergements() {
    this.hebergementService.getHebergementsByDestination(this.destinationId!).subscribe({
      next: (data) => {
        this.hebergements = data;
        console.log("✅ Hébergements chargés :", this.hebergements);
      },
      error: (error) => console.error("❌ Erreur chargement hébergements", error)
    });
  }
  
  choisirHebergement(hebergementId: number) {
    this.sessionService.setLocalStorage('hebergementId', hebergementId.toString());
    console.log("🏨 Hébergement enregistré en session (ID) :", hebergementId);
  
    // Redirection vers les activités sans encore stocker dans le panier
    this.router.navigate(['/activitesUser', hebergementId]);
  }

}
