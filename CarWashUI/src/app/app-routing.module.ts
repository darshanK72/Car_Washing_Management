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
import { MyCarsComponent } from './Components/User/my-cars/my-cars.component';
import { UserManagementComponent } from './Components/Admin/user-management/user-management.component';
import { WasherManagementComponent } from './Components/Admin/washer-management/washer-management.component';
import { OrderManagementComponent } from './Components/Admin/order-management/order-management.component';
import { ReportsComponent } from './Components/Admin/reports/reports.component';
import { UserDetailsComponent } from './Components/Admin/user-management/user-details/user-details.component';
import { WasherDetailsComponent } from './Components/Admin/washer-management/washer-details/washer-details.component';
import { AddWasherComponent } from './Components/Admin/washer-management/add-washer/add-washer.component';
import { EditWasherComponent } from './Components/Admin/washer-management/edit-washer/edit-washer.component';
import { OrderDetailsComponent } from './Components/Admin/order-management/order-details/order-details.component';
import { LeaderboardComponent } from './Components/Auth/leaderboard/leaderboard.component';
import { AddPackageComponent } from './Components/Admin/add-package/add-package.component';
import { WashRequestComponent } from './Components/Washer/wash-request/wash-request.component';
import { WashOrdersComponent } from './Components/Washer/wash-orders/wash-orders.component';
import { UserReviewsComponent } from './Components/Washer/user-reviews/user-reviews.component';

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
    path: 'leaderboard',
    component: LeaderboardComponent,
    canActivate: [AuthGuard],
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
    ],
  },
  {
    path: 'washer',
    component: WasherComponent,
    canActivate: [AuthGuard, WasherGuard],
    children: [
      {
        path: 'wash-request',
        component: WashRequestComponent,
        canActivate: [AuthGuard, WasherGuard],
      },
      {
        path: 'wash-orders',
        component: WashOrdersComponent,
        canActivate: [AuthGuard, WasherGuard],
      },
      {
        path: 'user-reviews',
        component: UserReviewsComponent,
        canActivate: [AuthGuard, WasherGuard],
      },
    ],
  },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [AuthGuard, AdminGuard],
    children: [
      {
        path: 'user-management',
        component: UserManagementComponent,
        canActivate: [AuthGuard, AdminGuard],
        children: [
          {
            path: 'user-details/:id',
            component: UserDetailsComponent,
            canActivate: [AuthGuard, AdminGuard],
          },
        ],
      },
      {
        path: 'washer-management',
        component: WasherManagementComponent,
        canActivate: [AuthGuard, AdminGuard],
        children: [
          {
            path: 'washer-details/:id',
            component: WasherDetailsComponent,
            canActivate: [AuthGuard, AdminGuard],
          },
          {
            path: 'add-washer',
            component: AddWasherComponent,
            canActivate: [AuthGuard, AdminGuard],
          },
          {
            path: 'edit-washer/:id',
            component: EditWasherComponent,
            canActivate: [AuthGuard, AdminGuard],
          },
        ],
      },
      {
        path: 'order-management',
        component: OrderManagementComponent,
        canActivate: [AuthGuard, AdminGuard],
        children: [
          {
            path: 'order-details/:id',
            component: OrderDetailsComponent,
            canActivate: [AuthGuard, AdminGuard],
          }
        ],
      },
      {
        path: 'reports',
        component: ReportsComponent,
        canActivate: [AuthGuard, AdminGuard],
      },
      {
        path: 'leaderboard',
        component: LeaderboardComponent,
        canActivate: [AuthGuard, AdminGuard],
      },
      {
        path: 'add-package',
        component: AddPackageComponent,
        canActivate: [AuthGuard, AdminGuard],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
