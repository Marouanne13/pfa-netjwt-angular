import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransportService, Transport } from '../../services/transport.service';

@Component({
  selector: 'app-transport-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transport-list.component.html',
  styleUrls: ['./transport-list.component.css']
})
export class TransportListComponent implements OnInit {
  transports: Transport[] = [];

  constructor(private transportService: TransportService) {}

  ngOnInit(): void {
    this.loadTransports();
  }

  loadTransports(): void {
    this.transportService.getTransports().subscribe((data) => {
      this.transports = data;
    });
  }
}
