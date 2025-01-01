import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Referral } from '../../Doctor/send-referral/referral';
import { HospitalStuffService } from '../hospital-stuff.service';


@Component({
  selector: 'app-hospital-stuff',
  standalone: true,
  imports: [],
  templateUrl: './hospital-stuff.component.html',
  styleUrl: './hospital-stuff.component.scss'
})
export class HospitalStuffComponent {
  @Input() referral: Referral | null = null;
  @Input() action: 'accept' | 'decline' = 'accept';
  @Output() actionCompleted = new EventEmitter<void>();
  reason: string = '';

  constructor(private hospitalService: HospitalStuffService) {}

  // submitAction(): void {
  //   if (this.action === 'decline' && !this.reason.trim()) {
  //     alert('Reason for declining is required');
  //     return;
  //   }

  //   const data = {
  //     referralID: this.referral?.id,
  //     action: this.action,
  //     reason: this.action === 'decline' ? this.reason : undefined,
  //   };

  //   this.hospitalService.updateReferral(data).subscribe({
  //     next: () => {
  //       alert(`${this.action === 'accept' ? 'Accepted' : 'Declined'} successfully`);
  //       this.actionCompleted.emit();
  //     },
  //     error: (err:any) => console.error('Error updating referral:', err),
  //   });
  // }
}