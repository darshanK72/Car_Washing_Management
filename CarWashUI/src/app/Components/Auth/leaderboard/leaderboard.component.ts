import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/user.model';
import { OrderService } from 'src/app/Services/order.service';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent implements OnInit {
  users: User[] = [];

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.loadLeaderboard();
  }

  loadLeaderboard(): void {
    this.orderService.getLeaderBoard().subscribe({
      next: (data) => {
        this.users = data;
        console.log(this.users);
      },
      error: (error) => console.error('There was an error!', error)
    });
  }
}
