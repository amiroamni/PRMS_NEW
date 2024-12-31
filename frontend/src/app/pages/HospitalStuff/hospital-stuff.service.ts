import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Referral } from '../Doctor/send-referral/referral';

@Injectable({
  providedIn: 'root'
})
export class HospitalStuffService {

  apiUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getPendingReferrals(hospitalID: string): Observable<Referral[]> {
    return this.http.get<Referral[]>(`${this.apiUrl}/pending/${hospitalID}`);
  }

  updateReferral(data: { referralID: string; action: string; reason?: string }): Observable<any> {
    return this.http.put(`${this.apiUrl}/update`, data);
  }

}
