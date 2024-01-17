import { Component } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { LocalStorageService } from '../LocalStorage.service';

@Component({
  selector: 'app-not-authorized',
  templateUrl: './not-authorized.component.html',
  styleUrls: ['./not-authorized.component.scss'],
})
export class NotAuthorizedComponent {
  ngOnInit() {
    console.log('not-authorized');
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  constructor(public oidcSecurityService: OidcSecurityService) {}
}
