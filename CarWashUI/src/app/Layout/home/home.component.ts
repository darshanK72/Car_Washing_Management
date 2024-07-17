import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  isLoggedIn$!: Observable<boolean>;
  userData$!:Observable<any>;
  constructor(private router:Router,private authService:AuthService){}

 ngOnInit(): void {
  this.isLoggedIn$ = this.authService.isLoggedIn$;
  this.userData$ = this.authService.loggedInUser$;
 }


  navigate(){
    this.userData$.subscribe(u =>{
      console.log(u);
       if(u.Role == 'User'){
        this.router.navigate(['user']);
       }else if(u.Role == 'Admin'){
        this.router.navigate(['admin']);
       }else{
        this.router.navigate(['washer']);
       }
    })
  }

}
