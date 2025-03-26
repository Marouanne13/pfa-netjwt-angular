import { Component } from '@angular/core';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-cancel',
  imports: [],
  templateUrl: './cancel.component.html',
  styleUrl: './cancel.component.css'
})
export class CancelComponent {
  ngOnInit(): void {
    Swal.fire({
      icon: 'warning',
      title: 'Paiement annulé',
      text: 'Votre paiement a été annulé. Vous pouvez réessayer.',
      confirmButtonColor: '#f39c12'
    });
  }


}
