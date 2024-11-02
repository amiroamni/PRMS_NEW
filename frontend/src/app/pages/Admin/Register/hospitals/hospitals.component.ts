import { Component,OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Hospital,Clinic } from '../Register';
import { RegisterService } from '../register.service';
import { ToastService } from 'src/app/account/login/toast-service';

@Component({
  selector: 'app-hospitals',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './hospitals.component.html',
  styleUrl: './hospitals.component.scss'
})

export class HospitalsComponent implements OnInit{


  hospitalForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router:Router,
    private Registerservice:RegisterService) {}

  ngOnInit(): void {
    // Initialize the form group with validators
    this.hospitalForm = this.fb.group({
      hospitalName: ['', Validators.required],
      hospitalType: ['',Validators.required], 
      hospitalPhone: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]], // Assuming a 10-digit phone number
      hospitalEmail: ['', [Validators.required, Validators.email]],
      hospitalLocation: ['', Validators.required],   
      // workPermit: [null, Validators.required],
      // licence: [null, Validators.required]
    });
  }

  // Handle file selection for work permit and license
  // onFileSelect(event: any, controlName: string): void {
  //   const file = event.target.files[0];
  //   if (file) {
  //     this.hospitalForm.patchValue({ [controlName]: file });
  //     this.hospitalForm.get(controlName)?.updateValueAndValidity();
  //   }
  // }

  // Submit the form and log the values to the console
  onSubmit(): void {
    // if (this.hospitalForm.valid) {
    //   console.log('Form Submitted Successfully');
    //   console.log(this.hospitalForm.value);

    //   // Extracting file values separately since file objects are not part of formControl directly
    //   console.log('Work Permit:', this.hospitalForm.get('workPermit')?.value);
    //   console.log('License:', this.hospitalForm.get('licence')?.value);
    // } else {
    //   console.log('Form is invalid. Please fill in the required fields.');
    // }
    if (this.hospitalForm.valid) {


      const hospitalData: Hospital = this.hospitalForm.value;

      console.log(hospitalData);
      this.Registerservice.saveHospital(hospitalData).subscribe(
        (response)=>{
          console.log('Hospital data saved:', response);

    // Assuming the response contains email and password
          const { email, password } = response;

          this.router.navigate(['/Credential']);
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
