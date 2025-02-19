import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { VoyagesComponent } from './components/voyages/voyages.component';
import { ActivitesComponent } from './components/activites/activites.component';
import { RestaurantsComponent } from './components/restaurants/restaurants.component';
import { ClientsComponent } from './components/clients/clients.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'voyages', component: VoyagesComponent, canActivate: [AuthGuard] },
  { path: 'activites', component: ActivitesComponent, canActivate: [AuthGuard] },
  { path: 'restaurants', component: RestaurantsComponent, canActivate: [AuthGuard] },
  { path: 'clients', component: ClientsComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' } // 🔹 Redirection pour toute page inconnue
];
