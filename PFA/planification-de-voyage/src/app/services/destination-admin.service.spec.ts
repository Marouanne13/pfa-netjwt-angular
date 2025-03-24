import { TestBed } from '@angular/core/testing';

import { DestinationAdminService } from './destination-admin.service';

describe('DestinationAdminService', () => {
  let service: DestinationAdminService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DestinationAdminService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
