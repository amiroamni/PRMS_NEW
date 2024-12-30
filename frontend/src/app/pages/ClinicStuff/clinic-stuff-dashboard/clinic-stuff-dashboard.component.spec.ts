import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClinicStuffDashboardComponent } from './clinic-stuff-dashboard.component';

describe('ClinicStuffDashboardComponent', () => {
  let component: ClinicStuffDashboardComponent;
  let fixture: ComponentFixture<ClinicStuffDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClinicStuffDashboardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClinicStuffDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
