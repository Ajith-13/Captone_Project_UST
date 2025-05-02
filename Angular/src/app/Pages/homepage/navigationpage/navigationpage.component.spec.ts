import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationpageComponent } from './navigationpage.component';

describe('NavigationpageComponent', () => {
  let component: NavigationpageComponent;
  let fixture: ComponentFixture<NavigationpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavigationpageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavigationpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
