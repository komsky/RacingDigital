import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RacesComponent } from './races/races.component';
import { LoginComponent } from './login/login.component';
import { LoginCallbackComponent } from './login-callback/login-callback.component';
import { AuthGuard } from './services/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'races', component: RacesComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'login-callback', component: LoginCallbackComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
