import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WasherComponent } from './washer.component';

describe('WasherComponent', () => {
  let component: WasherComponent;
  let fixture: ComponentFixture<WasherComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WasherComponent]
    });
    fixture = TestBed.createComponent(WasherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
