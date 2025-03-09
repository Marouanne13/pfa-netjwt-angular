import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ActiviteService } from '../../services/activite.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-activites-user',
  standalone: true,
  templateUrl: './activitesUser.component.html',
  styleUrls: ['./activitesUser.component.css'],
  imports: [CommonModule] // Ajoute ceci pour activer *ngIf et *ngFor
})
export class ActivitesUserComponent implements OnInit {
  activites: any[] = [];
  nomDestination: string = '';

  constructor(
    private route: ActivatedRoute,
    private activiteService: ActiviteService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.nomDestination = params.get('nom') || '';
      if (this.nomDestination) {
        this.getActivites();
      }
    });
  }

  getActivites() {
    this.activiteService.getActivitesParDestination(this.nomDestination).subscribe(
      (data) => {
        this.activites = data;
      },
      (error) => {
        console.error('Erreur lors du chargement des activités', error);
      }
    );
  }
}
