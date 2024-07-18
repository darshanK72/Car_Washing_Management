import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './Layout/home/home.component';
import { LoginComponent } from './Components/Auth/login/login.component';
import { RegisterComponent } from './Components/Auth/register/register.component';
import { UserComponent } from './Components/User/user/user.component';
import { WasherComponent } from './Components/Washer/washer/washer.component';
import { AdminComponent } from './Components/Admin/admin/admin.component';
import { UserGuard } from './Utils/user.guard';
import { WasherGuard } from './Utils/washer.guard';
import { AuthGuard } from './Utils/auth.guard';
import { AdminGuard } from './Utils/admin.guard';
import { ProfileComponent } from './Components/Auth/profile/profile.component';
import { BookWashComponent } from './Components/User/book-wash/book-wash.component';
import { MyOrdersComponent } from './Components/User/my-orders/my-orders.component';
import { MyReviewsComponent } from './Components/User/my-reviews/my-reviews.component';
import { ReceiptsComponent } from './Components/User/receipts/receipts.component';
import { LeaderboardComponent } from './Components/User/leaderboard/leaderboard.component';
import { MyCarsComponent } from './Components/User/my-cars/my-cars.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'user',
    component: UserComponent,
    canActivate: [AuthGuard, UserGuard],
    children: [
      {
        path: 'book-wash',
        component: BookWashComponent,
        canActivate: [AuthGuard, UserGuard],
      },
      {
        path: 'my-car',
        component: MyCarsComponent,
        canActivate: [AuthGuard, UserGuard],
      },
      {
        path: 'my-orders',
        component: MyOrdersComponent,
        canActivate: [AuthGuard, UserGuard],
      },
      {
        path: 'my-reviews',
        component: MyReviewsComponent,
        canActivate: [AuthGuard, UserGuard],
      },
      {
        path: 'receipts',
        component: ReceiptsComponent,
        canActivate: [AuthGuard, UserGuard],
      },
      {
        path: 'my-cars',
        component: MyCarsComponent,
        canActivate: [AuthGuard, UserGuard],
      },
      {
        path: 'leaderboard',
        component: LeaderboardComponent,
        canActivate: [AuthGuard, UserGuard],
      },
    ],
  },
  {
    path: 'washer',
    component: WasherComponent,
    canActivate: [AuthGuard, WasherGuard],
  },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [AuthGuard, AdminGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
