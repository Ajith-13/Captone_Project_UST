import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainerapproveComponent } from './trainerapprove.component';

describe('TrainerapproveComponent', () => {
  let component: TrainerapproveComponent;
  let fixture: ComponentFixture<TrainerapproveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainerapproveComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainerapproveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
