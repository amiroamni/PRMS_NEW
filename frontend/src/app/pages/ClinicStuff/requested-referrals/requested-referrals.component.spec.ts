import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestedReferralsComponent } from './requested-referrals.component';

describe('RequestedReferralsComponent', () => {
  let component: RequestedReferralsComponent;
  let fixture: ComponentFixture<RequestedReferralsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RequestedReferralsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RequestedReferralsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
