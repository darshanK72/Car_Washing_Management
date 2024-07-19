import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Washer } from 'src/app/Models/washer.model';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-washer-management',
  templateUrl: './washer-management.component.html',
  styleUrls: ['./washer-management.component.css']
})
export class WasherManagementComponent implements OnInit{
  washers: Washer[] = [];

  constructor(private adminService: AdminService,private router:Router) {}

  ngOnInit(): void {
    this.loadWashers();
  }

  loadWashers(): void {
    this.adminService.getWashers().subscribe(washers => {
      this.washers = washers;
    });
  }

  toggleStatus(washerId: number): void {
    const washer = this.washers.find(w => w.washerId === washerId);
    if (washer) {
      this.adminService.updateWasherStatus(washerId, !washer.isActive).subscribe(() => {
        washer.isActive = !washer.isActive;
      });
    }
  }

  viewDetails(washerId: number): void {
    this.router.navigate([`/admin/washer-management/washer-details/${washerId}`]);
  }

  addWasher(): void {
    this.router.navigate([`/admin/washer-management/add-washer`]);
  }

  editWasher(washerId: number): void {
    this.router.navigate([`/admin/washer-management/edit-washer/${washerId}`]);
  }
}
