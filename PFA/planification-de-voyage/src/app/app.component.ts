import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FooterComponent } from "./footer/footer.component";
import { HeaderComponent } from "./header/header.component";
import { HomeComponent } from "./components/home/home.component";
import { AboutComponent } from "./components/about/about.component";




@Component({
  selector: 'app-root',
  standalone: true,

  imports: [RouterModule, FooterComponent, HeaderComponent, HomeComponent, AboutComponent], // ✅ Ajout du module de routing
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Mon Application Angular';
}
