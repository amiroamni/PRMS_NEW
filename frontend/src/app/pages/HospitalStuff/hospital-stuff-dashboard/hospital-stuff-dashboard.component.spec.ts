import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HospitalStuffDashboardComponent } from './hospital-stuff-dashboard.component';

describe('HospitalStuffDashboardComponent', () => {
  let component: HospitalStuffDashboardComponent;
  let fixture: ComponentFixture<HospitalStuffDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HospitalStuffDashboardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HospitalStuffDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
