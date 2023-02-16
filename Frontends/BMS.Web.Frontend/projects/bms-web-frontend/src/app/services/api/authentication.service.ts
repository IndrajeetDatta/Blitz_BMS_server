import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/lib/bms-web-api-client.module';
import { firstValueFrom, Observable } from 'rxjs';
import { StorageService } from '../storage.service';
import { WebApiService } from '../web-api.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  public authRequested: boolean;
  public isLoading$: Observable<boolean>;

  constructor(
    private webApiService: WebApiService,
    private storageService: StorageService,
    private auth: AuthService,
    private router: Router,
    private http: HttpClient,
    @Inject(DOCUMENT) private doc: Document
  ) {}

  get isAuth() {
    return this.auth.isAuthenticated$;
  }

  public async isAdmin() {
    const user = await this.user();
    const role = this.getRole(user);
    return role === 'admin';
  }

  public getRole(user: any) {
    try {
      return user['https://app.blitzpower.com/roles'][0];
    } catch {}
    return undefined;
  }

  public async authenticateBasic(
    appUser: BMSWebApiClientModule.AuthenticationBasicRequest
  ): Promise<BMSWebApiClientModule.AuthenticationResponse> {
    const request = new BMSWebApiClientModule.AuthenticationBasicRequest(
      appUser
    );
    return await this.webApiService.getClient().authenticationBasic(request);
  }

  public login() {
    this.auth.loginWithRedirect({ redirect_uri: this.doc.location.origin });
  }

  public logout() {
    this.auth.logout({ returnTo: this.doc.location.origin });
    this.storageService.setUserEmail();
    this.storageService.setExternalId();
  }

  async user() {
    let user = await firstValueFrom(this.auth.user$.pipe());
    if (!user) user = {};

    return user;
  }

  sendUserToApi() {
    this.auth.user$.subscribe(async (user) => {
      if (user && this.storageService.getUserEmail() !== user.email) {
        if (user.email && user.sub) {
          try {
            const { email, nickname, sub } = user;
            const role = this.getRole(user);
            const appUser =
              new BMSWebApiClientModule.AuthenticationBasicRequest({
                email,
                externalId: sub,
                nickname,
                role,
              });
            await this.authenticateBasic(appUser);
            this.storageService.setUserEmail(email);
            this.storageService.setExternalId(sub);
          } catch (ex) {
            this.auth.logout({ returnTo: this.doc.location.origin });
            this.storageService.setUserEmail();
            this.storageService.setExternalId();
          }
        }
      }
    });
  }
}
