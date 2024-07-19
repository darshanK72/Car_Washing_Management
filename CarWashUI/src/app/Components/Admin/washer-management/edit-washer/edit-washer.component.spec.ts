import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWasherComponent } from './edit-washer.component';

describe('EditWasherComponent', () => {
  let component: EditWasherComponent;
  let fixture: ComponentFixture<EditWasherComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditWasherComponent]
    });
    fixture = TestBed.createComponent(EditWasherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
