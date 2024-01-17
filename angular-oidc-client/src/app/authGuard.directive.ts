import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { LocalStorageService } from './LocalStorage.service';
import { LoginResponse, OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable()
export class AuthorizationGuard implements CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    console.log('Guard');
    const loginResponse: LoginResponse =
      this.localStorageService.getItem('loginResponse');

    console.log('loginResponse', loginResponse);

    if (!loginResponse) return this.router.navigateByUrl('not-authorized');

    console.log('Guard');

    if (!loginResponse.isAuthenticated)
      return this.router.navigateByUrl('not-authorized');
    return true;
  }

  constructor(
    private localStorageService: LocalStorageService,
    public oidcSecurityService: OidcSecurityService,
    private router: Router
  ) {}
}
