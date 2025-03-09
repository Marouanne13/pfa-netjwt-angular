import { Component, OnInit } from '@angular/core';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { HebergementService } from '../../services/hebergement.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-hebergements',
  standalone: true,
  templateUrl: './hebergements.component.html',
  styleUrls: ['./hebergements.component.css'],
  imports: [CommonModule, RouterModule] // Ajoute RouterModule ici
})
export class HebergementsComponent implements OnInit {
  hebergements: any[] = [];
  nomDestination: string = '';

  constructor(
    private route: ActivatedRoute, // Injection correcte
    private hebergementService: HebergementService,
    private router: Router
  ) {}


  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.nomDestination = params.get('nom') || '';
      if (this.nomDestination) {
        this.getHebergements();
      }
    });
  }

  getHebergements() {
    this.hebergementService.getHebergementsParDestination(this.nomDestination).subscribe(
      (data) => {
        this.hebergements = data;
      },
      (error) => {
        console.error('Erreur lors du chargement des hébergements', error);
      }
    );
  }

  goToActivites() {
    this.router.navigate(['/activites', this.nomDestination]);
  }
}

