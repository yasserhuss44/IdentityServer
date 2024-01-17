import { Component } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { LocalStorageService } from '../LocalStorage.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  loginResponse: any;

  ngOnInit() {
    this.loginResponse = this.localStorageService.getItem('loginResponse');
  }
  logout() {
    this.oidcSecurityService
      .logoff()
      .subscribe((result) => console.log(result));
  }

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private localStorageService: LocalStorageService
  ) {}
}
