import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddassignmentquestionComponent } from './addassignmentquestion.component';

describe('AddassignmentquestionComponent', () => {
  let component: AddassignmentquestionComponent;
  let fixture: ComponentFixture<AddassignmentquestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddassignmentquestionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddassignmentquestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
