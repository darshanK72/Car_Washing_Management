import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent {
  status: boolean = false;
  constructor(private router : Router) { }

  ngOnInit(): void {

  }
}
