import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LoginUserComponent } from './components/login-user/login-user.component';
import { VoyagesComponent } from './components/voyages/voyages.component';
import { ActivitesComponent } from './components/activites/activites.component';

import { ClientsComponent } from './components/clients/clients.component';
import { TransportsComponent } from './components/transports/transports.component'; // ✅ Transport
import { GestionVoyagesComponent } from './components/gestion-voyages/gestion-voyages.component'; // ✅ Gestion des voyages
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';

import { RestaurantFormComponent } from './components/restaurant-form/restaurant-form.component';
import { RestaurantListComponent } from './components/restaurant-list/restaurant-list.component'; // ✅ Liste des restaurants
import { AboutComponent } from './components/about/about.component';
import { ActivitesUserComponent  } from './components/activitesUser/activitesUser.component';
import { DestinationComponent } from './components/destinations/destination.component';
import { HebergementsComponent } from './components/hebergements/hebergements.component';
import { UserMessageComponent } from './components/user-message/user-message.component';
import { AdminMessagesComponent } from './components/admin-messages/admin-messages.component';


export const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'loginUser', component: LoginUserComponent },
  { path: 'voyages', component: VoyagesComponent, canActivate: [AuthGuard] },
  { path: 'activites', component: ActivitesComponent, canActivate: [AuthGuard] },
  { path: 'restaurant-form', component: RestaurantFormComponent, canActivate: [AuthGuard] },
  { path: 'restaurant-form/:id', component: RestaurantFormComponent, canActivate: [AuthGuard] },
  { path: 'restaurant-list', component: RestaurantListComponent, canActivate: [AuthGuard] }, // ✅ Nouvelle route pour liste des restaurants
  { path: 'clients', component: ClientsComponent, canActivate: [AuthGuard] },
  { path: 'transports', component: TransportsComponent, canActivate: [AuthGuard] }, // ✅ Transport
  { path: 'gestion-voyages', component: GestionVoyagesComponent, canActivate: [AuthGuard] }, // ✅ Gestion des voyages
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'about', component: AboutComponent , canActivate: [AuthGuard]},
  { path: 'destinations', component: DestinationComponent },
  { path: 'hebergements/:nom', component: HebergementsComponent },
  { path: 'activites/:nom', component: ActivitesUserComponent },
  { path: 'user-messages', component: UserMessageComponent },
  { path: 'admin-messages', component: AdminMessagesComponent },
  { path: '**', redirectTo: '/home' }
];
