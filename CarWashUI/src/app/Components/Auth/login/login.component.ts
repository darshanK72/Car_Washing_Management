import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, LoginDto } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  isServerError: boolean = false;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  showpass(x: any, text: any): void {
    if (x.type === 'password') {
      x.type = 'text';
      text.text = 'Hide';
    } else {
      text.text = 'Show';
      x.type = 'password';
    }
  }

  login(): void {
    if (this.loginForm.valid) {
      const loginDto: LoginDto = this.loginForm.value;
      this.authService.login(loginDto).subscribe(
        () => {
          this.router.navigate(['/home']);
        },
        error => {
          this.isServerError = true;
          this.errorMessage = error.error;
        }
      );
    } else {
      this.loginForm.markAllAsTouched();
    }
  }
}
