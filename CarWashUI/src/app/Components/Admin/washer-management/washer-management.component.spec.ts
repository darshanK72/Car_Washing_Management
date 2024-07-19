import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WasherManagementComponent } from './washer-management.component';

describe('WasherManagementComponent', () => {
  let component: WasherManagementComponent;
  let fixture: ComponentFixture<WasherManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WasherManagementComponent]
    });
    fixture = TestBed.createComponent(WasherManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
