import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SendReferralService } from './send-referral.service';
import { Router } from '@angular/router';
import { Patient, Referral } from './referral';
import { UserserviceService } from 'src/app/service/userservice.service';
import { ReferralStateService } from 'src/app/core/services/referral-state.service';
@Component({
  selector: 'app-send-referral',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './send-referral.component.html',
  styleUrls: ['./send-referral.component.scss'], // Corrected key name
})
export class SendReferralComponent implements OnInit {
  referralForm!: FormGroup;
  patientForm!: FormGroup;
  Hospitals: any[] = [];
  patientNotFound: boolean = false;
  selectedFile: File | null = null;

  constructor(
    private fb: FormBuilder,
    private sendReferralService: SendReferralService,
    private router: Router,
    private userservice: UserserviceService,
    private referralstateservice: ReferralStateService
  ) {}

  ngOnInit(): void {
    const clinicID = this.userservice.getClinicID();
    const doctorID = this.userservice.getDoctorID();

    if (!clinicID || !doctorID) {
      console.error('Clinic ID or Doctor ID not found in the token');
      return;
    }

    // Initialize forms
    this.patientForm = this.fb.group({
      FirstName: ['', Validators.required],
      MiddleName: ['', Validators.required],
      LastName: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(0)]],
      gender: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', Validators.required],
      address: ['', Validators.required],
    });

    this.referralForm = this.fb.group({
      DoctorID: [doctorID],
      ClinicID: [clinicID],
      hospitalID: ['', Validators.required],
      department: [''],
      clinicalFindings: ['', Validators.required],
      diagnosisResult: ['', Validators.required],
      attachment: [null, Validators.required],
      reasonsForReferral: ['', Validators.required],
      rxGiven: [''],
    });

    this.fetchHospital(); // Fetch hospitals on component initialization
  }

  fetchHospital(): void {
    this.sendReferralService.getHospitals().subscribe(
      (data) => {
        this.Hospitals = data;
        console.log(this.Hospitals);
      },
      (error) => {
        console.error('Error fetching hospitals', error);
      }
    );
  }

  searchPatient(): void {
    const phoneNumber = this.patientForm.get('phoneNumber')?.value;

    if (!phoneNumber) {
      this.patientNotFound = true;
      return;
    }

    this.sendReferralService.findPatientByPhone(phoneNumber).subscribe({
      next: (patient) => {
        if (patient) {
          this.patientNotFound = false;
          this.patientForm.patchValue(patient);
        } else {
          this.patientNotFound = true;
          this.patientForm.reset({ phoneNumber });
        }
      },
      error: (error) => {
        console.error('Error fetching patient details:', error);
        this.patientNotFound = true;
        this.patientForm.reset({ phoneNumber });
      },
    });
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      if (file.size > 10 * 1024 * 1024) {
        alert('File size exceeds 10MB. Please upload a smaller file.');
        event.target.value = ''; // Reset the file input
      } else {
        this.selectedFile = file; // Store the file in a class-level variable
        console.log('Selected file:', file);
      }
    }
  }

  onSubmit(): void {
    if (this.referralForm.valid && this.patientForm.valid) {
      const patientData: Patient = this.patientForm.value;
      const referralData: Referral = this.referralForm.value;

      // Add selected file to referralData
      referralData.attachment = this.selectedFile || null;

      // Save the patient and use the returned PatientID
      this.sendReferralService.SavePatient(patientData).subscribe(
        (response: any) => {
          referralData.PatientID = response.PatientID;
        },
        (error) => {
          console.error('Error saving patient', error);
        }
      );
      this.referralstateservice.addReferral(referralData); // Add to the state
    console.log('Referral sent for approval');
    } else {
      console.log('Form is invalid');
    }
  }
}
