import { TestBed } from '@angular/core/testing';

import { SendReferralService } from './send-referral.service';

describe('SendReferralService', () => {
  let service: SendReferralService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SendReferralService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
