import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewassignmentComponent } from './reviewassignment.component';

describe('ReviewassignmentComponent', () => {
  let component: ReviewassignmentComponent;
  let fixture: ComponentFixture<ReviewassignmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReviewassignmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReviewassignmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
