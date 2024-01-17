import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { AuthorizationGuard } from './authGuard.directive';
import { NotAuthorizedComponent } from './not-authorized/not-authorized.component';
import { WeatherComponent } from './weather/weather.component';

const routes: Routes = [
  {
    path: 'not-authorized',
    component: NotAuthorizedComponent,
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthorizationGuard],
  },
  {
    path: 'weather',
    component: WeatherComponent,
    // canActivate: [AuthorizationGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
