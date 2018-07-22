import { TestBed, inject } from '@angular/core/testing';

import { VaultsService } from './vaults.service';

describe('VaultsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VaultsService]
    });
  });

  it('should be created', inject([VaultsService], (service: VaultsService) => {
    expect(service).toBeTruthy();
  }));
});
