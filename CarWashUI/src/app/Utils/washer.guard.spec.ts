import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { washerGuard } from './washer.guard';

describe('washerGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => washerGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
