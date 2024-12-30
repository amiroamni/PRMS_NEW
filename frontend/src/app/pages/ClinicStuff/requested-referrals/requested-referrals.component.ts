import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReferralStateService } from 'src/app/core/services/referral-state.service';
import { Referral } from '../../Doctor/send-referral/referral';
import { SendReferralService } from '../../Doctor/send-referral/send-referral.service';
import { UserserviceService } from 'src/app/service/userservice.service';

@Component({
  selector: 'app-requested-referrals',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './requested-referrals.component.html',
  styleUrls: ['./requested-referrals.component.scss'],
})
export class RequestedReferralsComponent implements OnInit {
  pendingReferrals: Referral[] = [];
  clinicID: string = '';

  constructor(
    private referralStateService: ReferralStateService,
    private sendReferralService: SendReferralService,
    private userservice: UserserviceService
  ) {}

  ngOnInit(): void {
    this.clinicID = this.userservice.getClinicID(); // Fetch ClinicID from the service
    if (!this.clinicID) {
      console.error('Clinic ID not found in the token');
      return;
    }
    this.loadPendingReferrals();
  }

  loadPendingReferrals(): void {
    // Fetch pending referrals from the state service
    this.pendingReferrals = this.referralStateService.getPendingReferrals(this.clinicID);
  }

  approveReferral(referral: Referral): void {
    referral.status = "Approved"; // Update status to "Approved"
    this.sendReferralService.SaveReferral(referral).subscribe(
      (response) => {
        console.log('Referral approved and saved successfully:', response);
        this.removeReferral(referral); // Remove referral from the pending list
      },
      (error) => {
        console.error('Error approving referral:', error);
      }
    );
  }

  viewAttachment(referral: Referral): void {
    if (referral.attachment) {
      // Open the file in a new window or a modal (based on how you handle attachments)
      const fileURL = URL.createObjectURL(referral.attachment);
      window.open(fileURL, '_blank');
    } else {
      console.log('No attachment found for this referral.');
    }
  }

  rejectReferral(referral: Referral): void {
    referral.status = "Rejected"; // Update status to "Rejected"
    console.log('Referral rejected:', referral);
    this.removeReferral(referral); // Remove referral from the pending list
  }

  removeReferral(referral: Referral): void {
    // Remove the referral from the state
    this.referralStateService.removeReferral(referral);
    this.loadPendingReferrals(); // Reload the updated pending referrals
  }
}
