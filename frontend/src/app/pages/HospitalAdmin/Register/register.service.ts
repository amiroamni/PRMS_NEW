import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { Doctor } from './register';
import { Stuff } from './register';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  apiUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getSpecializations(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + "/Api/Specialization");
  }

  saveDoctor(hospitalData: Doctor):  Observable<{ email: string; password: string }>{
    return this.http.post<any>(this.apiUrl + "/api/Doctor/withtoken", hospitalData).pipe(
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
  hospitalStuff(StuffData: Stuff): Observable<any> {
    return this.http.post(this.apiUrl + "/hospitals", StuffData);
  }


  
  private hospitalCredentialsSource = new BehaviorSubject<{ email: string; password: string } | null>(null);
  hospitalCredentials$ = this.hospitalCredentialsSource.asObservable();

  updateCredentials(email: string, password: string) {
    this.hospitalCredentialsSource.next({ email, password });
  }

}
 