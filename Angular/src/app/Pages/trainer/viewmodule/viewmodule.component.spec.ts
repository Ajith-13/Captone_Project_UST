import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewmoduleComponent } from './viewmodule.component';

describe('ViewmoduleComponent', () => {
  let component: ViewmoduleComponent;
  let fixture: ComponentFixture<ViewmoduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewmoduleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewmoduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
