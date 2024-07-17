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

const routes: Routes = [
  {
    path:'',
    redirectTo:'home',
    pathMatch:'full'
  },
  {
    path: 'home',
    component: HomeComponent,
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
    canActivate:[AuthGuard,UserGuard]
  },
  {
    path: 'washer',
    component: WasherComponent,
    canActivate:[AuthGuard,WasherGuard]
  },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate:[AuthGuard,AdminGuard]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
