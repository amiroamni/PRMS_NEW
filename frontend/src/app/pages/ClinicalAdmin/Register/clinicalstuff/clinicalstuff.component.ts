import { Component,OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterService } from '../register.service';
import { ToastService } from 'src/app/account/login/toast-service';
import { ClinicStuff } from '../Register';

@Component({
  selector: 'app-clinicalstuff',
  standalone: true,
  imports: [CommonModule , ReactiveFormsModule],
  templateUrl: './clinicalstuff.component.html',
  styleUrl: './clinicalstuff.component.scss'
})
export class ClinicalstuffComponent implements OnInit{


  StuffForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router:Router,
    private Registerservice:RegisterService) {}

  ngOnInit(): void {
    // Initialize the form group with validators
    this.StuffForm = this.fb.group({
      firstname: ['', Validators.required],
      middlename: ['',Validators.required], 
      lastname: ['', [Validators.required ]], // Assuming a 10-digit phone number
      gender: ['', [Validators.required]],
      clinicPhone: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]], 
      emailAddress: ['', [Validators.required, Validators.email]],
      role: ['', Validators.required],   
      // workPermit: [null, Validators.required],
      // licence: [null, Validators.required]
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
    // if (this.StuffForm.valid) {
    //   console.log('Form Submitted Successfully');
    //   console.log(this.StuffForm.value);

    //   // Extracting file values separately since file objects are not part of formControl directly
    //   console.log('Work Permit:', this.StuffForm.get('workPermit')?.value);
    //   console.log('License:', this.StuffForm.get('licence')?.value);
    // } else {
    //   console.log('Form is invalid. Please fill in the required fields.');
    // }
    if (this.StuffForm.valid) {


      const StuffData: ClinicStuff = this.StuffForm.value;

      console.log(StuffData);

      this.Registerservice.saveStuff(StuffData).subscribe(
        (response)=>{
          console.log('Clinic Stuff data saved:', response);

    // Assuming the response contains email and password
          const { email, password } = response;

          this.router.navigate(['/Credential']);
        },
        (error)=>{
          console.log('Error saving ClinicStuff Data', error);
        }
      );
      // You can add your form submission logic here
    } else {
      console.log('Form not valid');
    }
  }
}

