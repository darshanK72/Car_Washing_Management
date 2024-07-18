import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookWashComponent } from './book-wash.component';

describe('BookWashComponent', () => {
  let component: BookWashComponent;
  let fixture: ComponentFixture<BookWashComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookWashComponent]
    });
    fixture = TestBed.createComponent(BookWashComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
