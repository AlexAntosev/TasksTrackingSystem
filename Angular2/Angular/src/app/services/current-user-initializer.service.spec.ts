import { TestBed } from '@angular/core/testing';

import { CurrentUserInitializerService } from './current-user-initializer.service';

describe('CurrentUserInitializerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CurrentUserInitializerService = TestBed.get(CurrentUserInitializerService);
    expect(service).toBeTruthy();
  });
});
