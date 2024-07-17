import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-washer',
  templateUrl: './washer.component.html',
  styleUrls: ['./washer.component.css']
})
export class WasherComponent {
  status: boolean = false;
  constructor(private router : Router) { }

  ngOnInit(): void {

  }
}
