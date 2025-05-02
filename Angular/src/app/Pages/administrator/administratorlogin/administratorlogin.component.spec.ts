import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorloginComponent } from './administratorlogin.component';

describe('AdministratorloginComponent', () => {
  let component: AdministratorloginComponent;
  let fixture: ComponentFixture<AdministratorloginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdministratorloginComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministratorloginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
