import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { BMSWebApiClientModule } from '../../../../../../../bms-web-api-client/src/lib/bms-web-api-client.module';
import { WebApiService } from '../../../../services/web-api.service';

@Injectable({ providedIn: 'root' })
export class OverviewResolver
  implements Resolve<BMSWebApiClientModule.ApplicationUser[]>
{
  constructor(private webApiService: WebApiService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<BMSWebApiClientModule.ApplicationUser[]> {
    // sample api call
    return this.webApiService.getClient().getUsers();
  }
}
