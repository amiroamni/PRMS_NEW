import { Component,OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { RegisterService } from '../register.service';
import { Doctor } from '../register';
@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './doctor.component.html',
  styleUrl: './doctor.component.scss'
})
export class HospitalDoctorComponent implements OnInit{

  StuffForm!: FormGroup;
  specializations: any[] = []; // Array to hold specialization options

  
  constructor(private fb: FormBuilder,
    private router:Router,
    private Registerservice:RegisterService) {
        this.StuffForm = this.fb.group({
      doctorFirstName: ['', Validators.required],
      doctorMiddleName: ['', Validators.required],
      doctorLastName: ['', Validators.required],
      gender: ['', Validators.required],
      doctorphone: ['', Validators.required],
      doctorEmail: ['', [Validators.required, Validators.email]],
      specializationID: ['', Validators.required]
    });
  }
 

  ngOnInit(): void {
    this.fetchSpecializations(); // Fetch specializations on component initialization
  }

  fetchSpecializations(): void {
    this.Registerservice.getSpecializations().subscribe(data => {
      this.specializations = data; // Store the fetched specializations
    });
  }

  // Handle file selection for work permit and license
  // onFileSelect(event: any, controlName: string): void {
  //   const file = event.target.files[0];
  //   if (file) {
  //     this.StuffForm.patchValue({ [controlName]: file });
  //     this.StuffForm.get(controlName)?.updateValueAndValidity();
  //   }
  // }

  // Submit the form and log the values to the console
  onSubmit(): void {
    // if (this.clincalForm.valid) {
    //   console.log('Form Submitted Successfully');
    //   console.log(this.clincalForm.value);

    //   // Extracting file values separately since file objects are not part of formControl directly
    //   console.log('Work Permit:', this.clincalForm.get('workPermit')?.value);
    //   console.log('License:', this.clincalForm.get('licence')?.value);
    // } else {
    //   console.log('Form is invalid. Please fill in the required fields.');
    // }

    if (this.StuffForm.valid) {
      const  StuffData: Doctor = this.StuffForm.value;
      this.Registerservice.saveDoctor(StuffData).subscribe(
        (response)=>{
          // console.log('Hospital data saved:',response);
          const { email, password } = response;

          this.router.navigate(['/Hospitals-Doctor-Credential']);
        },
        (error)=>{
          console.log('Error saving Hospital Data', error);
        }
      );
      // You can add your form submission logic here
    } else {
      console.log('Form not valid');
    }
  }
}
