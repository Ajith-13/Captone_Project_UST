import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainerlandingpageComponent } from './trainerlandingpage.component';

describe('TrainerlandingpageComponent', () => {
  let component: TrainerlandingpageComponent;
  let fixture: ComponentFixture<TrainerlandingpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainerlandingpageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainerlandingpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
