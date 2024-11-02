import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

// Auth Services
import { AuthenticationService } from '../services/auth.service';
 import { environment } from '../../../environments/environment';
import { UserserviceService } from 'src/app/service/userservice.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard  {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
         private service: UserserviceService
    ) { }

    canActivate(
        next: ActivatedRouteSnapshot, 
        state: RouterStateSnapshot):boolean {
        if (sessionStorage.getItem('token') != null && sessionStorage.getItem('token') != "") {
    
          let roles = next.data['permittedRoles'] as Array<string>;
    
          // ['hospital Admin',
          //   'clinic Admin',
          //   'Employee Manager',
          //   'PM Admin',
          //   'Planner',
          //   'Plan Reporting',
          //   'Case Admin',
          //   'Director',
          //   'Member',
          //   'Secretery',
          //   'Encoder']
    
          if (roles) {
            if (this.service.roleMatch(roles)) return true;
            else {
              this.router.navigate(['/auth']);
              return false;
            }
          }
          return true;
        }
        else {
          this.router.navigate(['/auth']);
          return false;
        }
    
      }
      logout() {
        sessionStorage.setItem('token', "")
        window.location.reload()
      }
}
