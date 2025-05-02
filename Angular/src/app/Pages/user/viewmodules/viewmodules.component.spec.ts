import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewmodulesComponent } from './viewmodules.component';

describe('ViewmodulesComponent', () => {
  let component: ViewmodulesComponent;
  let fixture: ComponentFixture<ViewmodulesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewmodulesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewmodulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
