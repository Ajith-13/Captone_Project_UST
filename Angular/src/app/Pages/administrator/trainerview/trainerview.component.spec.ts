import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainerviewComponent } from './trainerview.component';

describe('TrainerviewComponent', () => {
  let component: TrainerviewComponent;
  let fixture: ComponentFixture<TrainerviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainerviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainerviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
