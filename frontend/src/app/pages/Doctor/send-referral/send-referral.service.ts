import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, tap, throwError } from 'rxjs';
import { Map } from 'leaflet';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Patient, Referral } from './referral';
 @Injectable({
  providedIn: 'root'
})
export class SendReferralService {

  apiUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getHospitals(): Observable<any[]> {
      return this.http.get<any[]>(this.apiUrl + "/api/Hospital");
  }

  findPatientByPhone(phoneNumber: string): Observable<any> {
    const url = `${this.apiUrl}/api/patients/search/${phoneNumber}`;
    return this.http.get<any>(url);
  }
  // edit the url 
  SavePatient(PatientData: Patient):  Observable<{ PatientID: string}>{
      return this.http.post<any>(this.apiUrl + "/api/ClinicStaff/withtoken", PatientData).pipe(
        map(response => {
         
          const { PatientID } = response
          console.log(response)
            
          // Assuming the response has 'email' and 'password' properties
          return {
            PatientID: response.PatientID
          };
        })
      );
    }
  
    SaveReferral(ReferralData: Referral): Observable<any> {
      return this.http.post<any>(this.apiUrl + '/api/ClinicStaff/withtoken', ReferralData).pipe(
        tap((response) => {
          console.log('Referral saved successfully:', response);
        }),
        catchError((error) => {
          console.error('Error saving referral:', error);
          // Rethrow the error after logging it, so the component can handle it too
          return throwError(error);
        })
      );
    }
    

}
