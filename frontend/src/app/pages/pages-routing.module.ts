import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index/index.component';

import { AuthGuard } from '../core/guards/auth.guard';

// Component pages
import { ChatComponent } from './chat/chat.component';
import { HospitalsComponent } from './Admin/Register/hospitals/hospitals.component';
import { ClinicsComponent } from './Admin/Register/clinics/clinics.component';
import { HospitalDoctorComponent } from './HospitalAdmin/Register/doctor/doctor.component';
import { ClinicDoctorComponent } from './ClinicalAdmin/Register/doctor/doctor.component';
import { HospitalRegisterdComponent } from './Admin/Register/hospital-registerd/hospital-registerd.component';
import { HospitalstuffComponent } from './HospitalAdmin/Register/hospitalstuff/hospitalstuff.component';
import { ClinicalstuffComponent } from './ClinicalAdmin/Register/clinicalstuff/clinicalstuff.component';
import { ClinicRegisterdComponent } from './ClinicalAdmin/Register/hospital-registerd/hospital-registerd.component';
import { HospitalsRegisterdComponent } from './HospitalAdmin/Register/hospital-registerd/hospital-registerd.component';
const routes: Routes = [

   {
    path: '', 
    component: IndexComponent
  },
  {
    path:'Credential',
    component:HospitalRegisterdComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['Adminstrator']}

  },
  {
    path:'Clinic-Doctor-Credential',
    component:ClinicRegisterdComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['ClinicAdmin']}

  },
    {
    path:'Hospitals-Doctor-Credential',
    component: HospitalsRegisterdComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['HospitalAdmin']}

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
    component: ClinicalstuffComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['ClinicAdmin']}
  },
  {
    path: 'clinicdoctor', 
    component: ClinicDoctorComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['ClinicAdmin']}
  },
  {
    path: 'message',
    component: ChatComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['Doctor']}
  }
 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
 