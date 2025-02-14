import { TestBed } from '@angular/core/testing';

import { ReactionService } from './reaction.service';

describe('ReactionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ReactionService = TestBed.get(ReactionService);
    expect(service).toBeTruthy();
  });
});
