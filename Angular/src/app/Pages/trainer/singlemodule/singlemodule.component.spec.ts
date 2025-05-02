import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SinglemoduleComponent } from './singlemodule.component';

describe('SinglemoduleComponent', () => {
  let component: SinglemoduleComponent;
  let fixture: ComponentFixture<SinglemoduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SinglemoduleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SinglemoduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
