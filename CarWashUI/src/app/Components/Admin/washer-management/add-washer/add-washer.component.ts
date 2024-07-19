import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/Services/admin.service';
import { Washer } from 'src/app/Models/washer.model';
import { WasherService } from 'src/app/Services/washer.service';

@Component({
  selector: 'app-add-washer',
  templateUrl: './add-washer.component.html',
  styleUrls: ['./add-washer.component.css']
})
export class AddWasherComponent {
  washer: Washer = {
    washerId: 0,
    name: '',
    email: '',
    password: '',
    phoneNumber: '',
    role: 'Washer',
    isActive: true
  };

  constructor(private adminService: AdminService, private router: Router,private washerService:WasherService) {}

  saveWasher(): void {
    this.washerService.createWasher(this.washer).subscribe(() => {
      this.router.navigate(['/admin/washer-management']);
    });
  }
}
