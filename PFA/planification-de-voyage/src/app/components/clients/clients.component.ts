
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ClientsService } from '../../services/clients.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-clients',
  standalone: true,
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css'],
  imports: [CommonModule, ReactiveFormsModule, FormsModule]
})
export class ClientsComponent implements OnInit {
  clients: any[] = [];
  selectedClientForm: FormGroup;





  constructor(private clientsService: ClientsService, private fb: FormBuilder) {
    this.selectedClientForm = this.fb.group({
      id: [null],
      nom: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      telephone: ['', [Validators.required, Validators.pattern(/^[0-9]{8,15}$/)]],
      adresse: ['', Validators.required],
      dateDeNaissance: ['', Validators.required],
      genre: ['', Validators.required],
      motDePasse: ['', Validators.required],
      estActif: [true],
      dateDeCreation: [''],
      admins: [[]]
    });
  }

  ngOnInit(): void {
    this.fetchClients();
  }

  fetchClients() {
    this.clientsService.getClients().subscribe({
      next: (data) => {
        this.clients = data;
      },
      error: (err) => {
        console.error('❌ Erreur lors du chargement des clients:', err);
      }
    });
  }

  updateClient(client: any) {
    if (!client || !client.id) {
      console.error("❌ Client invalide pour la mise à jour !");
      return;
    }
    this.selectedClientForm.patchValue({
      id: Number(client.id),
      nom: client.nom,
      email: client.email,
      telephone: client.telephone,
      adresse: client.adresse,
      dateDeNaissance: client.dateDeNaissance || '',
      genre: client.genre || '',
      motDePasse: client.motDePasse || '',
      estActif: client.estActif !== undefined ? client.estActif : true,
      dateDeCreation: client.dateDeCreation || new Date(),
      admins: client.admins || []
    });
  }

  saveClient() {
    if (this.selectedClientForm.invalid) {
      console.error("❌ Formulaire invalide !");
      return;
    }

    let clientData = { ...this.selectedClientForm.value };
    clientData.id = Number(clientData.id);
    clientData.dateDeCreation = clientData.dateDeCreation || new Date();

    console.log(`📤 Données envoyées :`, clientData);
    console.log(`🔗 URL API utilisée : ${this.clientsService.apiUrl}/${clientData.id}`);

    this.clientsService.updateClient(clientData.id, clientData).subscribe({
      next: () => {
        console.log("✅ Client mis à jour !");
        this.fetchClients();
        this.selectedClientForm.reset();
      },
      error: (err) => {
        console.error("❌ Erreur lors de la modification du client :", err);
      }
    });
  }

  deleteClient(clientId: number) {
    if (!clientId) {
      console.error("❌ ID du client invalide !");
      return;
    }

    this.clientsService.deleteClient(clientId).subscribe({
      next: () => {
        console.log("✅ Client supprimé avec succès !");
        this.fetchClients();
      },
      error: (err) => {
        console.error("❌ Erreur lors de la suppression du client:", err);
      }
    });
  }
}
