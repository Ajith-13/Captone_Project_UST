import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SinglemodulesComponent } from './singlemodules.component';

describe('SinglemodulesComponent', () => {
  let component: SinglemodulesComponent;
  let fixture: ComponentFixture<SinglemodulesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SinglemodulesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SinglemodulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
