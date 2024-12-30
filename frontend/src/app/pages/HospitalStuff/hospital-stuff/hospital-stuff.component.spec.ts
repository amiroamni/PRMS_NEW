import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HospitalStuffComponent } from './hospital-stuff.component';

describe('HospitalStuffComponent', () => {
  let component: HospitalStuffComponent;
  let fixture: ComponentFixture<HospitalStuffComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HospitalStuffComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HospitalStuffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
