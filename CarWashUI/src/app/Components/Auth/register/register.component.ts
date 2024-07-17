import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, RegisterDto } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  isServerError: boolean = false;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      phoneNumber: ['', Validators.pattern(/^(\+\d{1,3}[- ]?)?\d{10}$/)],
      role: ['', [Validators.required, Validators.pattern(/^(Admin|User|Washer)$/)]]
    });
  }

  ngOnInit(): void {}

  register(): void {
    if (this.registerForm.valid) {
      const registerDto: RegisterDto = this.registerForm.value;
      this.authService.register(registerDto).subscribe(
        (resp:any) => {
          console.log(resp.message)
          this.router.navigate(['/login']);
        },
        error => {
          this.isServerError = true;
          this.errorMessage = error.error;
        }
      );
    } else {
      this.registerForm.markAllAsTouched();
    }
  }
}
