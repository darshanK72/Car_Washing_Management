import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from 'src/app/Models/user.model';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent {
  userForm: FormGroup;
  user: User | null = null;
  isEditMode = false;
  userRoleId!:any;


  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.userForm = this.fb.group({
      name: [{ value: '', disabled: true }, Validators.required],
      email: [
        { value: '', disabled: true },
        [Validators.required, Validators.email],
      ],
      phoneNumber: [{ value: '', disabled: true }, Validators.required],
      profilePicture: [{ value: '', disabled: true }],
      address: [{ value: '', disabled: true }],
      role: [{ value: '', disabled: true }, Validators.required],
    });
  }

  ngOnInit(): void {
     this.userRoleId =  this.authService.getUserRoleAndId();
    this.getUserProfile();
  }

  getUserProfile(): void {
    this.authService.getUserByRoleAndId(this.userRoleId).subscribe(u => {
      this.user = u;
      this.userForm.patchValue(this.user);
    })
  }

  enableEditMode(): void {
    this.isEditMode = true;
    this.userForm.enable();
  }

  onSubmit(): void {
    if (this.userForm.valid && this.user) {
      const updatedUser: User = { ...this.user, ...this.userForm.value };
      this.authService.updateUser(updatedUser).subscribe(
        (response) => {
          console.log('User updated successfully!', response);
          this.user = response;
          this.isEditMode = false;
          this.userForm.disable();
        },
        (error) => {
          console.error('Error updating user!', error);
        }
      );
    }
  }
}
