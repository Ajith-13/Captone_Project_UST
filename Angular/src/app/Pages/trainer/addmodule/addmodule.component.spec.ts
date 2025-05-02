import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddmoduleComponent } from './addmodule.component';

describe('AddmoduleComponent', () => {
  let component: AddmoduleComponent;
  let fixture: ComponentFixture<AddmoduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddmoduleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddmoduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
