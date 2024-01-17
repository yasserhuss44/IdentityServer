import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { ProfileComponent } from './profile/profile.component';
import { AuthorizationGuard } from './authGuard.directive';
import { NotAuthorizedComponent } from './not-authorized/not-authorized.component';
import { WeatherComponent } from './weather/weather.component';
// ...
@NgModule({
  declarations: [AppComponent, ProfileComponent, NotAuthorizedComponent, WeatherComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthModule.forRoot({
      config: {
        authority: 'https://dev-ysallam.us.auth0.com',
        redirectUrl: window.location.origin + '/callback1',
        postLogoutRedirectUri: window.location.origin,
        clientId: 'Qppoms0Iehi73UQzDRS824RP7u8QNm7U',
        scope: 'openid profile email offline_access',
        responseType: 'code',
        silentRenew: true,
        useRefreshToken: true,
        logLevel: LogLevel.Debug,
      },
    }),
  ],
  providers: [AuthorizationGuard],
  bootstrap: [AppComponent],
})
export class AppModule {}

// config: {
//   authority: 'https://localhost:44318',
//   redirectUrl: window.location.origin + '/callback1',
//   postLogoutRedirectUri: window.location.origin,
//   clientId: 'angular',
//   scope: 'openid profile email offline_access',
//   responseType: 'code',
//   silentRenew: true,
//   useRefreshToken: true,
//   logLevel: LogLevel.Debug,
// },

// options.Authority = "https://localhost:44318";
// options.ClientId = "mvc3";
// options.ClientSecret = "secret";
// options.ResponseType = "code";

// config: {
//   authority: 'https://dev-ysallam.us.auth0.com',
//   redirectUrl: window.location.origin + '/callback1',
//   postLogoutRedirectUri: window.location.origin,
//   clientId: 'Qppoms0Iehi73UQzDRS824RP7u8QNm7U',
//   scope: 'openid profile email offline_access',
//   responseType: 'code',
//   silentRenew: true,
//   useRefreshToken: true,
//   logLevel: LogLevel.Debug,
// },
