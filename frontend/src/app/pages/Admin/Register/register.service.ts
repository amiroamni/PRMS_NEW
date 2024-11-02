import { Injectable } from '@angular/core';
import { Hospital,Clinic } from './Register';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  apiUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  saveHospital(hospitalData: Hospital): Observable<{ email: string; password: string }> {
    return this.http.post<any>(this.apiUrl + "/api/Hospital/withtoken", hospitalData).pipe(
      map(response => {
       
        const { email , password } = response

        this.updateCredentials(email, password)
        
        // Assuming the response has 'email' and 'password' properties
        return {
          email: response.email,
          password: response.password
        };
      })
    );
  }

  saveClinic(ClinicalData: Clinic): Observable<{ email: string; password: string }>{
    return this.http.post<any>(this.apiUrl + "/api/Clinic/withtoken", ClinicalData).pipe(
      map(response => {
       
        const { email , password } = response

        this.updateCredentials(email, password)
        
        // Assuming the response has 'email' and 'password' properties
        return {
          email: response.email,
          password: response.password
        };
      })
    );
  }

  private hospitalCredentialsSource = new BehaviorSubject<{ email: string; password: string } | null>(null);
  hospitalCredentials$ = this.hospitalCredentialsSource.asObservable();

  updateCredentials(email: string, password: string) {
    this.hospitalCredentialsSource.next({ email, password });
  }


  
}
