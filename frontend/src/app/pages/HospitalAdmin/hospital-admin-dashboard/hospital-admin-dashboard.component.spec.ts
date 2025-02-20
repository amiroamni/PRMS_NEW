import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HospitalAdminDashboardComponent } from './hospital-admin-dashboard.component';

describe('HospitalAdminDashboardComponent', () => {
  let component: HospitalAdminDashboardComponent;
  let fixture: ComponentFixture<HospitalAdminDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HospitalAdminDashboardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HospitalAdminDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
