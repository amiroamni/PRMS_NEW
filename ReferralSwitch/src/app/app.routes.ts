import { Routes } from '@angular/router';
import { HomeComponent } from './page/home/home.component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    // Add other routes here for referrals, hospitals, etc.
    { path: '', redirectTo: '/home', pathMatch: 'full' }
];
