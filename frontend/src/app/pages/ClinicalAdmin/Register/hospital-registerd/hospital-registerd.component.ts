import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterService } from '../register.service';
@Component({
  selector: 'app-hospital-registerd',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hospital-registerd.component.html',
  styleUrl: './hospital-registerd.component.scss'
})
export class HospitalRegisterdComponent {
  hospitalCredentials: { email: string; password: string } | null = null;
  hospitalName: string = 'Example Hospital'; // Replace with actual hospital data

  constructor(private hospitalService: RegisterService) {}

  ngOnInit() {
    this.hospitalService.hospitalCredentials$.subscribe(credentials => {
      this.hospitalCredentials = credentials;
    });
  }
  
}
