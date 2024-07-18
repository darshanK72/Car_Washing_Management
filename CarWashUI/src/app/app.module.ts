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
import { LeaderboardComponent } from './Components/User/leaderboard/leaderboard.component';
import { MyCarsComponent } from './Components/User/my-cars/my-cars.component';

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
