import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HospitalstuffComponent } from './hospitalstuff.component';

describe('HospitalstuffComponent', () => {
  let component: HospitalstuffComponent;
  let fixture: ComponentFixture<HospitalstuffComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HospitalstuffComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HospitalstuffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
