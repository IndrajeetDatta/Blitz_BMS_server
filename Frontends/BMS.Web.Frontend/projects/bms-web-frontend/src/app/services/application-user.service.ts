import { Injectable } from '@angular/core';
import { BMSWebApiClientModule } from "../../../../bms-web-api-client/src/lib/bms-web-api-client.module";
import { StorageService } from './storage.service';
import { WebApiService } from './web-api.service';

@Injectable({
  providedIn: 'root'
})
export class ApplicationUserService {

  userEmail: string
  externalId: string

  constructor(private webApiService : WebApiService, private storageService : StorageService) { 
    this.userEmail = storageService.getUserEmail()
    this.externalId = storageService.getExternalId()
  }

  async update(request: BMSWebApiClientModule.ApplicationUserUpdateRequest) : Promise<BMSWebApiClientModule.ApplicationUser> {
    return this.webApiService.getClient().updateUsers(request, this.userEmail, this.externalId);
  }
}
