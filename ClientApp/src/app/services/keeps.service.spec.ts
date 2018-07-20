import { TestBed, inject } from '@angular/core/testing';

import { KeepsService } from './keeps.service';

describe('KeepsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [KeepsService]
    });
  });

  it('should be created', inject([KeepsService], (service: KeepsService) => {
    expect(service).toBeTruthy();
  }));
});
