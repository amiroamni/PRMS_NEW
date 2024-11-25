import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { Doctor, ClinicStuff } from './Register';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  apiUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getSpecializations(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + "/Api/Specialization");
  }


  saveDoctor(StuffData: Doctor):  Observable<{ email: string; password: string }>{
    return this.http.post<any>(this.apiUrl + "/api/Doctor/withtoken", StuffData).pipe(
      map(response => {
       
        const { email , password } = response
        console.log(response)

        this.updateCredentials(email, password)
        
        // Assuming the response has 'email' and 'password' properties
        return {
          email: response.email,
          password: response.password
        };
      })
    );
  }
  saveStuff(StuffData: ClinicStuff):  Observable<{ email: string; password: string }>{
    return this.http.post<any>(this.apiUrl + "/api/ClinicStaff/withtoken", StuffData).pipe(
      map(response => {
       
        const { email , password } = response
        console.log(response)

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
 