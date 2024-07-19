import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from 'src/app/Services/admin.service';
import { Washer } from 'src/app/Models/washer.model';
import { WasherService } from 'src/app/Services/washer.service';

@Component({
  selector: 'app-edit-washer',
  templateUrl: './edit-washer.component.html',
  styleUrls: ['./edit-washer.component.css']
})
export class EditWasherComponent implements OnInit {
  washer: Washer | null = null;

  constructor( private route: ActivatedRoute, private router: Router,private washerService:WasherService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const washerId = Number(params.get('id'));
      this.loadWasher(washerId);
    });
  }

  loadWasher(washerId: number): void {
    this.washerService.getWasher(washerId).subscribe(washer => {
      console.log(washer);
      this.washer = washer;
    });
  }

  updateWasher(): void {
    if (this.washer) {
      this.washerService.updateWasher(this.washer.washerId, this.washer).subscribe(() => {
        this.router.navigate(['/admin/washer-management']);
      });
    }
  }
}
