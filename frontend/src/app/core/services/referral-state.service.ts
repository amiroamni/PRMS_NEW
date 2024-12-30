import { Injectable } from '@angular/core';
import { Referral } from 'src/app/pages/Doctor/send-referral/referral';
@Injectable({
  providedIn: 'root',
})
export class ReferralStateService {
  private referrals: Referral[] = [];

  addReferral(referral: Referral): void {
    // Check for duplicates by unique criteria (e.g., PatientID + ClinicID)
    const isDuplicate = this.referrals.some(
      (r) =>
        r.PatientID === referral.PatientID && r.ClinicID === referral.ClinicID
    );

    if (isDuplicate) {
      console.warn('Duplicate referral detected, not adding.');
    } else {
      this.referrals.push(referral);
      console.log('Referral added:', referral);
    }
  }
 
  
  getPendingReferrals(clinicID: string): Referral[] {
    // Filter referrals based on the ClinicID and approval status
    return this.referrals.filter(
      (referral) => referral.ClinicID === clinicID && referral.status === 'Pending'
    );
  }

  getReferrals(): Referral[] {
    return [...this.referrals]; // Return a copy to avoid direct mutations
  }

  removeReferral(referral: Referral): void {
    this.referrals = this.referrals.filter(
      (r) =>
        r.PatientID !== referral.PatientID || r.ClinicID !== referral.ClinicID
    );
    console.log('Referral removed:', referral);
  }
}
