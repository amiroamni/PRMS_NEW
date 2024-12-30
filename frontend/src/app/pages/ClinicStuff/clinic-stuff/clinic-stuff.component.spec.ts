import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClinicStuffComponent } from './clinic-stuff.component';

describe('ClinicStuffComponent', () => {
  let component: ClinicStuffComponent;
  let fixture: ComponentFixture<ClinicStuffComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClinicStuffComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClinicStuffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
