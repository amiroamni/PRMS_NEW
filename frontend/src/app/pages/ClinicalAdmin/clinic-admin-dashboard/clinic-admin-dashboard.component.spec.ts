import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClinicAdminDashboardComponent } from './clinic-admin-dashboard.component';

describe('ClinicAdminDashboardComponent', () => {
  let component: ClinicAdminDashboardComponent;
  let fixture: ComponentFixture<ClinicAdminDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClinicAdminDashboardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClinicAdminDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
