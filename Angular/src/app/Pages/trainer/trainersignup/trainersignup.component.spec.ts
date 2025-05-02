import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainersignupComponent } from './trainersignup.component';

describe('TrainersignupComponent', () => {
  let component: TrainersignupComponent;
  let fixture: ComponentFixture<TrainersignupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainersignupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainersignupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
