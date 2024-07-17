import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  status: boolean = false;
  constructor(private router : Router) { }

  ngOnInit(): void {

  }
}
