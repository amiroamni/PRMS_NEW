import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClinicalstuffComponent } from './clinicalstuff.component';

describe('ClinicalstuffComponent', () => {
  let component: ClinicalstuffComponent;
  let fixture: ComponentFixture<ClinicalstuffComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClinicalstuffComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClinicalstuffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
