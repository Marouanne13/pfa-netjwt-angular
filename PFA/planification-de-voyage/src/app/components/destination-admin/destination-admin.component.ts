import { Component, OnInit } from '@angular/core';
import { DestinationAdminService, Destination } from '../../services/destination-admin.service';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-destination-admin',
  templateUrl: './destination-admin.component.html',
  styleUrls: ['./destination-admin.component.css'],
  standalone: true, // 👈 Activer standalone component
  imports: [CommonModule, FormsModule], // 👈 Import local
})
export class DestinationAdminComponent implements OnInit {
  destinations: Destination[] = [];
  form: Destination = this.initForm();
  editing = false;

  constructor(private service: DestinationAdminService) {}

  ngOnInit(): void {
    this.loadDestinations();
  }
  initForm(): Destination {
    return {
      nom: '',
      description: '',
      region: '',
      latitude: 0,
      longitude: 0,
      imageUrl: '',
      estPopulaire: false,
      nombreVisites: 0,
      ville: ''
    };
  }

  loadDestinations(): void {
    this.service.getAll().subscribe(data => this.destinations = data);
  }

  save(): void {
    if (this.editing && this.form.id) {
      this.service.update(this.form.id, this.form).subscribe(() => {
        this.reset();
        this.loadDestinations();
      });
    } else {
      this.service.create(this.form).subscribe(() => {
        this.reset();
        this.loadDestinations();
      });
    }
  }

  edit(destination: Destination): void {
    this.form = { ...destination };
    this.editing = true;
  }

  delete(id: number): void {
    if (confirm("Voulez-vous vraiment supprimer cette destination ?")) {
      this.service.delete(id).subscribe(() => this.loadDestinations());
    }
  }

  reset(): void {
    this.form = this.initForm();
    this.editing = false;
  }
}
