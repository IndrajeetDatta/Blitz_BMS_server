import { Injectable } from '@angular/core';
import { BMSWebApiClientModule } from "../../../../bms-web-api-client/src/lib/bms-web-api-client.module";
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class WebApiService {
  constructor() {}
  
  getClient(): BMSWebApiClientModule.BMSWebApiClient {
    return new BMSWebApiClientModule.BMSWebApiClient(
      environment.bmsWebApiBaseUrl
    );
  }
}
