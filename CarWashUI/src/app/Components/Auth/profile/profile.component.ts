import { ChangeDetectorRef, Component } from '@angular/core';
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


  constructor(private fb: FormBuilder, private authService: AuthService,private cdr:ChangeDetectorRef) {
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
      userId:[null],
      adminId:[null],
      washerId:[null]
    });
  }

  ngOnInit(): void {
     this.userRoleId =  this.authService.getUserRoleAndId();
    this.getUserProfile();
  }

  getUserProfile(): void {
    this.authService.getUserByRoleAndId(this.userRoleId).subscribe(u => {
      this.user = u;
      console.log(this.user);
      this.cdr.markForCheck();
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
      console.log(updatedUser);
      if(this.user.role == 'User'){
        this.addUser(updatedUser);
      }else if(this.user.role == 'Admin'){
        this.addAdmin(updatedUser);
      }else{
        this.addWasher(updatedUser);
      }
    }
  }

  addUser(user:any){
    this.authService.updateUser(user).subscribe(
      (response) => {
        console.log('User updated successfully!', response);
        this.isEditMode = false;
        this.userForm.disable();
        this.cdr.markForCheck();
      },
      (error) => {
        console.error('Error updating user!', error);
      }
    );
  }

  addWasher(user:any){
    this.authService.updateWasher(user).subscribe(
      (response) => {
        console.log('User updated successfully!', response);
        this.isEditMode = false;
        this.userForm.disable();
        this.cdr.markForCheck();
      },
      (error) => {
        console.error('Error updating user!', error);
      }
    );
  }

  addAdmin(user:any){
    this.authService.updateAdmin(user).subscribe(
      (response) => {
        console.log('User updated successfully!', response);
        this.isEditMode = false;
        this.userForm.disable();
        this.cdr.markForCheck();
      },
      (error) => {
        console.error('Error updating user!', error);
      }
    );
  }
}
