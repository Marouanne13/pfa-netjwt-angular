import { Component } from '@angular/core';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-success',
  imports: [],
  templateUrl: './success.component.html',
  styleUrl: './success.component.css'
})
export class SuccessComponent {


ngOnInit(): void {
  Swal.fire({
    icon: 'success',
    title: 'Paiement réussi 🎉',
    text: 'Merci pour votre réservation !',
    confirmButtonColor: '#3085d6'
  });
}


}
