import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
// import { Token, User, UserView } from './user';
// import { ResponseMessage, SelectList } from '../common/common';
// import { UserManagment } from '../common/user-management/user-managment';
// import { Employee } from '../common/organization/employee/employee';
import { jwtDecode } from 'jwt-decode';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserserviceService {
  readonly BaseURI = environment.baseUrl;

  constructor(private http: HttpClient) {}

  // comparePasswords(fb: FormGroup): void {
  //   const confirmPswrdCtrl = fb.get('ConfirmPassword');
  //   if (!confirmPswrdCtrl?.errors || 'passwordMismatch' in confirmPswrdCtrl.errors) {
  //     confirmPswrdCtrl.setErrors(
  //       fb.get('Password')!.value !== confirmPswrdCtrl.value ? { passwordMismatch: true } : null
  //     );
  //   }
  // // }

  // register(body: User) {
  //   return this.http.post(this.BaseURI + '/ApplicationUser/Register', body)
  //     .pipe(catchError(this.handleError));
  // }

  // login(formData: User) {
  //   return this.http.post<any>(this.BaseURI + '/ApplicationUser/Login', formData)
  //     .pipe(catchError(this.handleError));
  // }

  getUserProfile() {
    return this.http.get(this.BaseURI + '/UserProfile')
      .pipe(catchError(this.handleError));
  }

  roleMatch(allowedRoles: string[]): boolean {
    const token = sessionStorage.getItem('token');
    if (!token) return false;

    const payLoad = jwtDecode<any>(token);
    const userRoles = payLoad.Role.split(",");

    return allowedRoles.some(role => userRoles.includes(role));
  }

  // getCurrentUser(): UserView | null {
  //   const token = sessionStorage.getItem('token');
  //   if (!token) return null;

  //   const payLoad = jwtDecode<any>(token);
  //   return {
  //     UserID: payLoad.UserID,
  //     FullName: payLoad.FullName,
  //     role: payLoad.role.split(","),
  //     EmployeeId: payLoad.EmployeeId,
  //     SubOrgId: payLoad.SubsidiaryOrganizationId,
  //     StrucId: payLoad.StructureId,
  //     Photo: payLoad.Photo
  //   };
  // }

  // Other methods remain unchanged...


// Mock function to get user roles from token or session storage
getUserRoles(): Array<string> {
  const token = sessionStorage.getItem('token');
  if (token) {
    const decodedToken = this.decodeToken(token);
    return decodedToken.Role; // Assuming roles are part of token payload
  }
  return [];
}

// Function to check if the user has a specific role
hasRole(role: string): boolean {
  const roles = this.getUserRoles();
  return roles.includes(role);
}

// Decode JWT token
decodeToken(token: string) {
  return JSON.parse(atob(token.split('.')[1]));
}



  private handleError(error: any) {
    // Handle error here, log it or transform it
    return throwError(error);
  }
}
