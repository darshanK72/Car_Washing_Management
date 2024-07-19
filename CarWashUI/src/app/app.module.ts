import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Layout/header/header.component';
import { FooterComponent } from './Layout/footer/footer.component';
import { HomeComponent } from './Layout/home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './Components/Auth/login/login.component';
import { RegisterComponent } from './Components/Auth/register/register.component';
import { ProfileComponent } from './Components/Auth/profile/profile.component';
import { UserComponent } from './Components/User/user/user.component';
import { WasherComponent } from './Components/Washer/washer/washer.component';
import { AdminComponent } from './Components/Admin/admin/admin.component';
import { BookWashComponent } from './Components/User/book-wash/book-wash.component';
import { MyOrdersComponent } from './Components/User/my-orders/my-orders.component';
import { ReceiptsComponent } from './Components/User/receipts/receipts.component';
import { MyReviewsComponent } from './Components/User/my-reviews/my-reviews.component';
import { MyCarsComponent } from './Components/User/my-cars/my-cars.component';
import { UserManagementComponent } from './Components/Admin/user-management/user-management.component';
import { WasherManagementComponent } from './Components/Admin/washer-management/washer-management.component';
import { ReportsComponent } from './Components/Admin/reports/reports.component';
import { OrderManagementComponent } from './Components/Admin/order-management/order-management.component';
import { UserDetailsComponent } from './Components/Admin/user-management/user-details/user-details.component';
import { WasherDetailsComponent } from './Components/Admin/washer-management/washer-details/washer-details.component';
import { AddWasherComponent } from './Components/Admin/washer-management/add-washer/add-washer.component';
import { EditWasherComponent } from './Components/Admin/washer-management/edit-washer/edit-washer.component';
import { OrderDetailsComponent } from './Components/Admin/order-management/order-details/order-details.component';
import { LeaderboardComponent } from './Components/Auth/leaderboard/leaderboard.component';
import { AddPackageComponent } from './Components/Admin/add-package/add-package.component';
import { WashRequestComponent } from './Components/Washer/wash-request/wash-request.component';
import { UserReviewsComponent } from './Components/Washer/user-reviews/user-reviews.component';
import { WashOrdersComponent } from './Components/Washer/wash-orders/wash-orders.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    UserComponent,
    WasherComponent,
    AdminComponent,
    BookWashComponent,
    MyOrdersComponent,
    ReceiptsComponent,
    MyReviewsComponent,
    LeaderboardComponent,
    MyCarsComponent,
    UserManagementComponent,
    WasherManagementComponent,
    ReportsComponent,
    OrderManagementComponent,
    UserDetailsComponent,
    WasherDetailsComponent,
    AddWasherComponent,
    EditWasherComponent,
    OrderDetailsComponent,
    AddPackageComponent,
    WashRequestComponent,
    UserReviewsComponent,
    WashOrdersComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
