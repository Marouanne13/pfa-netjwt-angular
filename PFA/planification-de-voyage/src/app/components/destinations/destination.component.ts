import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-destination',
  standalone: true,
  templateUrl: './destination.component.html',
  styleUrls: ['./destination.component.css']
})
export class DestinationComponent {
  destinations = [
    { id: 1, nom: 'Marrakech', description: 'Ville touristique marocaine' },
    { id: 2, nom: 'Casablanca', description: 'Capitale économique du Maroc' },
    { id: 3, nom: 'Rabat', description: 'Capitale administrative du Maroc' }
  ];

  constructor(private router: Router) {}

  goToHebergements(destinationNom: string) {
    this.router.navigate(['/hebergements', destinationNom]);
  }
}
