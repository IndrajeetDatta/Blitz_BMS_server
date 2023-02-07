import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AuthModel } from '../models/authModel';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  private authLocalStorageToken = `${environment.appVersion}-${environment.USERDATA_KEY}`;

  public setAuth(auth: string): boolean {
    if (auth) {
      localStorage.setItem(this.authLocalStorageToken, auth);
      return true;
    }
    return false;
  }
  public getAuth(): string {
    try {
      const authData = localStorage.getItem(this.authLocalStorageToken)!;
      return authData;
    } catch (error) {
      console.error(error);
      return "undefined";
    }
  }
  public getAuthModel(): AuthModel | null {
    const authToken = this.getAuth();
    if (authToken) {
      const parsedToken: AuthModel = jwt_decode(authToken);
      return parsedToken;
    }
    return null;
  }
  public getAuthIdentityId(): string | null {
    const authModel = this.getAuthModel();
    if (authModel) {
      return authModel.identityUserId;
    }
    return null;
  }
  public getAuthEmail(): string | null {
    const authModel = this.getAuthModel();

    if (authModel) {
      return authModel.authEmail;
    }
    return null;
  }

  public getUserEmail() {
    return localStorage.getItem('userEmail') || '';
  }

  public setUserEmail(value?: string) {
    localStorage.setItem('userEmail', value || '-');
  }

  public getExternalId() {
    return localStorage.getItem('externalId') || '';
  }

  public setExternalId(value?: string) {
    localStorage.setItem('externalId', value || '-');
  }
}
