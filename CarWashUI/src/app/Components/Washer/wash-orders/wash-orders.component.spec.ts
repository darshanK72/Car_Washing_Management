import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WashOrdersComponent } from './wash-orders.component';

describe('WashOrdersComponent', () => {
  let component: WashOrdersComponent;
  let fixture: ComponentFixture<WashOrdersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WashOrdersComponent]
    });
    fixture = TestBed.createComponent(WashOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
