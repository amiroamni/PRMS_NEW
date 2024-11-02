import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index/index.component';

import { AuthGuard } from '../core/guards/auth.guard';

// Component pages
import { HospitalsComponent } from './Admin/Register/hospitals/hospitals.component';
import { ClinicsComponent } from './Admin/Register/clinics/clinics.component';
import { HospitalDoctorComponent } from './HospitalAdmin/Register/doctor/doctor.component';
import { ClinicDoctorComponent } from './ClinicalAdmin/Register/doctor/doctor.component';
import { HospitalRegisterdComponent } from './Admin/Register/hospital-registerd/hospital-registerd.component';
import { HospitalstuffComponent } from './HospitalAdmin/Register/hospitalstuff/hospitalstuff.component';
const routes: Routes = [

   {
    path: '', 
    component: IndexComponent
  },
  {
    path:'Credential',
    component:HospitalRegisterdComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['Adminstrator','ClinicAdmin','HospitalAdmin']}

  },
  {
    path: 'hospitals', 
    component: HospitalsComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['Adminstrator']}
  },
 
  {
    path: 'clinics',
    component: ClinicsComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['Adminstrator']}
  },
  {
    path: 'hospitalsStuff', 
    component: HospitalstuffComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['HospitalAdmin']}
  },

  {
    path:'hospitaldoctor',
    component:HospitalDoctorComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['HospitalAdmin']}

  },
  {
    path: 'clinicStuff', 
    component: ClinicDoctorComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['ClinicAdmin']}
  },
  {
    path: 'clinicdoctor', 
    component: ClinicDoctorComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['ClinicAdmin']}
  },
  
 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
 