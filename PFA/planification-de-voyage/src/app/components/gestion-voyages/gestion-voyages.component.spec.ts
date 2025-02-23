import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GestionVoyagesComponent } from './gestion-voyages.component';

describe('GestionVoyagesComponent', () => {
  let component: GestionVoyagesComponent;
  let fixture: ComponentFixture<GestionVoyagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GestionVoyagesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GestionVoyagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
