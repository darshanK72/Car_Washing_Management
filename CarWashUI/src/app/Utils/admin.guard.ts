import { Injectable } from '@angular/core';
import { CanActivate, Router} from '@angular/router';
import { AuthService } from '../Services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private router: Router,
              private authService : AuthService){}

              canActivate():boolean{

                let user = this.authService.getUserDetails();
                if(user.Role == 'Admin'){
                  return true;
                } else {
                  this.router.navigate(['home']);
                  return false;
              }
            }
}
