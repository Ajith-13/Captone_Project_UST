import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignmentsubmittedComponent } from './assignmentsubmitted.component';

describe('AssignmentsubmittedComponent', () => {
  let component: AssignmentsubmittedComponent;
  let fixture: ComponentFixture<AssignmentsubmittedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignmentsubmittedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignmentsubmittedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
