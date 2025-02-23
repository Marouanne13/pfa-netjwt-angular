import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { routes } from './app/app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { FormsModule } from '@angular/forms';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes, withComponentInputBinding()), // ✅ Ajout d'un meilleur binding des composants
    provideHttpClient(withInterceptors([])), // ✅ Configuration HTTP avec possibilité d'intercepteurs
    importProvidersFrom(FormsModule) // ✅ Importation correcte de FormsModule pour ngModel
  ]
}).catch(err => console.error('❌ Erreur lors du bootstrap de l\'application:', err));
