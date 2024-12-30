import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestedReferralComponent } from './requested-referral.component';

describe('RequestedReferralComponent', () => {
  let component: RequestedReferralComponent;
  let fixture: ComponentFixture<RequestedReferralComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RequestedReferralComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RequestedReferralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
