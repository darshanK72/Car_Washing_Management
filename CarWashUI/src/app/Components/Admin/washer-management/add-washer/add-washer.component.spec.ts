import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWasherComponent } from './add-washer.component';

describe('AddWasherComponent', () => {
  let component: AddWasherComponent;
  let fixture: ComponentFixture<AddWasherComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddWasherComponent]
    });
    fixture = TestBed.createComponent(AddWasherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
