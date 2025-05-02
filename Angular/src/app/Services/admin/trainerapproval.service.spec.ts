import { TestBed } from '@angular/core/testing';

import { TrainerapprovalService } from './trainerapproval.service';

describe('TrainerapprovalService', () => {
  let service: TrainerapprovalService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrainerapprovalService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
