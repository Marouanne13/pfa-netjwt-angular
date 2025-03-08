import { Component } from '@angular/core';
import { FooterComponent } from "../../footer/footer.component";
import { HeaderComponent } from "../../header/header.component";

@Component({
  selector: 'app-about',  // Assure-toi que ce selector est unique
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
  imports: [FooterComponent, HeaderComponent]
})
export class AboutComponent { }
