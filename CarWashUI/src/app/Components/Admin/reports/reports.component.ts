import { Component } from '@angular/core';
import { AdminService } from 'src/app/Services/admin.service';
import { Report } from 'src/app/Models/report.model';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css'],
})
export class ReportsComponent {
  startDate!: Date;
  endDate!: Date;
  report!: Report;

  constructor(private adminService: AdminService) {}

  fetchReport() {
    console.log(this.startDate);
    console.log(this.endDate);
    if (this.startDate && this.endDate) {
      const start = new Date(this.startDate);
      const end = new Date(this.endDate);

      // Ensure valid dates
      if (isNaN(start.getTime()) || isNaN(end.getTime())) {
        console.error('Invalid date format.');
        return;
      }

      this.adminService.getReport(start, end).subscribe(
        (data: Report) => {
          this.report = data;
        },
        (error) => {
          console.error('Error fetching report', error);
        }
      );
    } else {
      console.error('Start date and end date are required.');
    }
  }
}
