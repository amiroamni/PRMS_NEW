import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
import { DoctorDashboardComponent } from './Doctor/doctor-dashboard/doctor-dashboard.component';
import { AdminDashboardComponent } from './Admin/admin-dashboard/admin-dashboard.component';
import { ClinicAdminDashboardComponent } from './ClinicalAdmin/clinic-admin-dashboard/clinic-admin-dashboard.component';
import { ClinicStuffComponent } from './ClinicStuff/clinic-stuff/clinic-stuff.component';
import { ClinicStuffDashboardComponent } from './ClinicStuff/clinic-stuff-dashboard/clinic-stuff-dashboard.component';
import { HospitalAdminDashboardComponent } from './HospitalAdmin/hospital-admin-dashboard/hospital-admin-dashboard.component';
import { HospitalStuffDashboardComponent } from './HospitalStuff/hospital-stuff-dashboard/hospital-stuff-dashboard.component';
import { SendReferralComponent } from './Doctor/send-referral/send-referral.component';
const routes: Routes = [
    // empty routes
   {
    path: '', 
    component: AdminDashboardComponent
    },
  {
    path: '', 
    component: ClinicAdminDashboardComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['ClinicAdmin']}
  },
  {
    path: '', 
    component: ClinicStuffDashboardComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['ClinicStuff']}
  },
  {
    path: '', 
    component: DoctorDashboardComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['Doctor']}
  },
  {
    path: '', 
    component: HospitalAdminDashboardComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['HospitalAdmin']}
  },
  {
    path: '', 
    component: HospitalStuffDashboardComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['HospitalStuff']}
  },
    // Admin routes

  {
    path:'Credential',
    component:HospitalRegisterdComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['Adminstrator']}

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
   // Clinic Route
  {
    path:'Clinic-Doctor-Credential',
    component:ClinicRegisterdComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['ClinicAdmin']}

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
    path: 'Send-referral', 
    component: SendReferralComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['Doctor']}
  },
 

  // Hospitl Route
  {
    path:'Hospitals-Doctor-Credential',
    component: HospitalsRegisterdComponent,
    canActivate: [AuthGuard],data:{permittedRoles : ['HospitalAdmin']}

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

  //Doctors Route
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
 