import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DestinationAdminComponent } from './destination-admin.component';

describe('DestinationAdminComponent', () => {
  let component: DestinationAdminComponent;
  let fixture: ComponentFixture<DestinationAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DestinationAdminComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DestinationAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
