import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainersigninComponent } from './trainersignin.component';

describe('TrainersigninComponent', () => {
  let component: TrainersigninComponent;
  let fixture: ComponentFixture<TrainersigninComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainersigninComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainersigninComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
