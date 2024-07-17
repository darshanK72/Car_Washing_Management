import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  isLoggedIn$!: Observable<boolean>;
  userData$!:Observable<any>;

 constructor(private authService:AuthService){}

 ngOnInit(): void {
  this.isLoggedIn$ = this.authService.isLoggedIn$;
  this.userData$ = this.authService.loggedInUser$;
 }

 logout(){
  this.authService.logout();
 }
}
