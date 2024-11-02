import { Component ,OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { Router } from '@angular/router';
import { Hospital,Clinic } from '../Register';
import { RegisterService } from '../register.service';

@Component({
  selector: 'app-clinics',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './clinics.component.html',
  styleUrl: './clinics.component.scss'
})
export class ClinicsComponent implements OnInit{

  clincalForm!: FormGroup;

  constructor(private fb: FormBuilder,
    private router:Router,
    private Registerservice:RegisterService) {}

  ngOnInit(): void {
    // Initialize the form group with validators
    this.clincalForm = this.fb.group({
      clinicName: ['', Validators.required],
      clinicType: ['', Validators.required],
      clinicPhone: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]], // Assuming a 10-digit phone number
      clinicEmail: ['', [Validators.required, Validators.email]],
      clinicLocation: ['', Validators.required],
     });
  }

  // Handle file selection for work permit and license
  // onFileSelect(event: any, controlName: string): void {
  //   const file = event.target.files[0];
  //   if (file) {
  //     this.clincalForm.patchValue({ [controlName]: file });
  //     this.clincalForm.get(controlName)?.updateValueAndValidity();
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

    if (this.clincalForm.valid) {
      const ClinicalData: Clinic = this.clincalForm.value;
      this.Registerservice.saveClinic(ClinicalData).subscribe(
        (response)=>{
          console.log('Clinic data saved:',response);

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
