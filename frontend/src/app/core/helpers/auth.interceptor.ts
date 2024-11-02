import { HTTP_INTERCEPTORS, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';

import { TokenStorageService } from '../../core/services/token-storage.service';
import { JwtUtilService } from 'src/app/jwt-util.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

// const TOKEN_HEADER_KEY = 'Authorization';       // for Spring Boot back-end
const TOKEN_HEADER_KEY = 'x-access-token';   // for Node.js Express back-end

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private TokenStorageService: TokenStorageService,
    private router: Router,
    private jwtUtilService: JwtUtilService,
   ) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Get the JWT token from the AuthService
    const token = this.TokenStorageService.getToken();

    // Clone the request and add the token to the headers

    if (token) {
     // Decode the JWT token to get the user information
     const userInfo = this.jwtUtilService.decodeToken(token);



   // sessionStorage.setItem('userdecoded', JSON.stringify(userInfo));
     // Store the username and role in the session storage or secure storage
    sessionStorage.setItem('role', userInfo.Role);
     // Clone the request and add the JWT token to the headers
     request = request.clone({
       setHeaders: {
         Authorization: `Bearer ${token}`
       }
     });
   }

    return next.handle(request);
 }
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
];