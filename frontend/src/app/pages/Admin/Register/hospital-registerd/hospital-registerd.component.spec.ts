import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HospitalRegisterdComponent } from './hospital-registerd.component';

describe('HospitalRegisterdComponent', () => {
  let component: HospitalRegisterdComponent;
  let fixture: ComponentFixture<HospitalRegisterdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HospitalRegisterdComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HospitalRegisterdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
