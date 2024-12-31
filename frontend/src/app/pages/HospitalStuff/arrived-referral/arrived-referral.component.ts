import { Component , OnInit } from '@angular/core';
import { Referral } from '../../Doctor/send-referral/referral';
import { HospitalStuffService } from '../hospital-stuff.service';


@Component({
  selector: 'app-arrived-referral',
  standalone: true,
  imports: [],
  templateUrl: './arrived-referral.component.html',
  styleUrl: './arrived-referral.component.scss'
})
export class ArrivedReferralComponent implements OnInit {
  pendingReferrals: Referral[] = [];

  constructor(private hospitalService: HospitalStuffService) {}

  ngOnInit(): void {
    this.loadPendingReferrals();
  }

  loadPendingReferrals(): void {
    const hospitalID = localStorage.getItem('hospitalID'); // Fetch from auth token
    if (!hospitalID) {
      console.error('Hospital ID not found');
      return;
    }

    this.hospitalService.getPendingReferrals(hospitalID).subscribe({
      next: (referrals) => (this.pendingReferrals = referrals),
      error: (err) => console.error('Error fetching referrals:', err),
    });
  }
}