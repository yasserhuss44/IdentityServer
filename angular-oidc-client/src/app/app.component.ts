import { Component } from '@angular/core';
import { LoginResponse, OidcSecurityService } from 'angular-auth-oidc-client';
import { LocalStorageService } from './LocalStorage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'angular-16-empty';
  loginResponse: any;

  ngOnInit() {
    this.oidcSecurityService
      .checkAuth()
      .subscribe((loginResponse: LoginResponse) => {
        this.loginResponse = loginResponse;
        console.log(loginResponse);
        this.localStorageService.setItem('loginResponse', loginResponse);
      });
  }

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private localStorageService: LocalStorageService
  ) {}
}
