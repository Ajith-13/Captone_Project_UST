import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserlandingPageComponent } from './userlanding-page.component';

describe('UserlandingPageComponent', () => {
  let component: UserlandingPageComponent;
  let fixture: ComponentFixture<UserlandingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserlandingPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserlandingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
