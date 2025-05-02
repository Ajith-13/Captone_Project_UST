import { TestBed } from '@angular/core/testing';

import { ViewCourseService } from './view-course.service';

describe('ViewCourseService', () => {
  let service: ViewCourseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ViewCourseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
