import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/user.model';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent {
  users: User[] = [];
  userRatings: { [key: number]: number } = {};

  constructor(private adminService: AdminService,private router:Router) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.adminService.getUsers().subscribe(users => {
      this.users = users;
      this.loadUserRatings();
    });
  }

  loadUserRatings(): void {
    this.users.forEach(user => {
      this.adminService.getUserReviews(user.userId).subscribe(reviews => {
        if (reviews.length > 0) {
          const totalRating = reviews.reduce((acc, review) => acc + review.rating, 0);
          this.userRatings[user.userId] = totalRating / reviews.length;
        }
      });
    });
  }

  toggleStatus(userId: number): void {
    const user = this.users.find(u => u.userId === userId);
    if (user) {
      this.adminService.updateUserStatus(userId, !user.isActive).subscribe(() => {
        user.isActive = !user.isActive;
      });
    }
  }

  viewDetails(userId: number): void {
    this.router.navigate([`/admin/user-management/user-details/${userId}`]);
  }
}


