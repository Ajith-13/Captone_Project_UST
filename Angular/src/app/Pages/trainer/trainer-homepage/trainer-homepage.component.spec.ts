import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainerHomepageComponent } from './trainer-homepage.component';

describe('TrainerHomepageComponent', () => {
  let component: TrainerHomepageComponent;
  let fixture: ComponentFixture<TrainerHomepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainerHomepageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainerHomepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
