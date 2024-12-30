import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArrivedReferralComponent } from './arrived-referral.component';

describe('ArrivedReferralComponent', () => {
  let component: ArrivedReferralComponent;
  let fixture: ComponentFixture<ArrivedReferralComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArrivedReferralComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ArrivedReferralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
