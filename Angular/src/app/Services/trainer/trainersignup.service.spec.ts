import { TestBed } from '@angular/core/testing';

import { TrainersignupService } from './trainersignup.service';

describe('TrainersignupService', () => {
  let service: TrainersignupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrainersignupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
