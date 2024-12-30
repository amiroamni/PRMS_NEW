import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendReferralComponent } from './send-referral.component';

describe('SendReferralComponent', () => {
  let component: SendReferralComponent;
  let fixture: ComponentFixture<SendReferralComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SendReferralComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SendReferralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
