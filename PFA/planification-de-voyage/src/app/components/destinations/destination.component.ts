import { Component, OnInit } from '@angular/core';
import { DestinationService } from '../../services/destination.service';
import { Router } from '@angular/router';
import { SessionService } from '../../services/session.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-destination',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './destination.component.html',
  styleUrls: ['./destination.component.css']
})
export class DestinationComponent implements OnInit {
  
  destinations: any[] = [];

  constructor(
    private destinationService: DestinationService,
    private router: Router,
    private sessionService: SessionService
  ) {}

  ngOnInit() {
    this.getDestinations();
  }

  getDestinations() {
    this.destinationService.getDestinations().subscribe({
      next: (data: any[]) => {
        this.destinations = data;
        console.log("📌 Destinations chargées : ", this.destinations);
      },
      error: (error) => console.error("❌ Erreur chargement destinations", error)
    });
  }

  choisirDestination(destinationId: number) {
    // ✅ Enregistrer l'ID dans la session
    this.sessionService.setLocalStorage('destinationId', destinationId.toString());

    console.log("📌 Destination enregistrée en session (ID) :", this.sessionService.getLocalStorage('destinationId'));
  
    // 🔥 Rediriger vers la page des hébergements
    this.router.navigate(['/hebergements']);
  }
  
  goToHebergements(destinationId: number) {
    this.sessionService.setLocalStorage('destinationId', destinationId.toString());

    console.log("📌 Redirection vers hébergements pour destination ID :", destinationId);
    this.router.navigate(['/hebergements']);
  }
}
