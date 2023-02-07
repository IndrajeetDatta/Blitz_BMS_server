// Core
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// BMS
import { AccountComponent } from './pages/portal/account/account.component';
import { ChargePointConfigurationComponent } from './pages/portal/charge-station/charge-point-configuration/charge-point-configuration.component';
import { ChargeStationConfigurationComponent } from './pages/portal/charge-station/charge-station-configuration/charge-station-configuration.component';
import { ChargeStationDetailsComponent } from './pages/portal/charge-station/charge-station-details/charge-station-details.component';
import { ChargeStationLoadManagementComponent } from './pages/portal/charge-station/charge-station-load-management/charge-station-load-management.component';
import { ChargeStationUserDataComponent } from './pages/portal/charge-station/charge-station-user-data/charge-station-user-data.component';
import { ChargeStationOccpComponent } from './pages/portal/charge-station/charge-station-occp/charge-station-occp.component';
import { ChargeStationWhitelistComponent } from './pages/portal/charge-station/charge-station-whitelist/charge-station-whitelist.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { OverviewComponent } from './pages/portal/charge-station/overview/overview.component';
import { OverviewResolver } from './pages/portal/charge-station/overview/overview.resolver';
import { PortalComponent } from './pages/portal/portal.component';
import { UsersManagementComponent } from './pages/portal/admin/users-management/users-management.component';
import { ChargeStationComponent } from './pages/portal/charge-station/charge-station.component';
import { ChargingPointEditComponent } from './pages/portal/charge-station/charging-point-edit/charging-point-edit.component';
import { ChargeStationCommandHistoryComponent } from './pages/portal/charge-station/charge-station-command-history/charge-station-command-history.component';
import { ChargeStationEmailsComponent } from './pages/portal/charge-station/charge-station-emails/charge-station-emails.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'auth/login',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    children: [
      {
        path: 'login',
        component: LoginComponent,
      }
    ]
  },
  {
    path: 'portal',
    component: PortalComponent,
    children: [
      {
        path: 'charge-station',
        component: ChargeStationComponent,
        children: [
          {
            path: 'overview',
            component: OverviewComponent,
            resolve: { data: OverviewResolver },
          },
          {
            path: 'details',
            component: ChargeStationDetailsComponent,
          },
          {
            path: 'configuration',
            component: ChargeStationConfigurationComponent,
          },
          {
            path: 'commands-history',
            component: ChargeStationCommandHistoryComponent,
          },
          {
            path: 'whitelist',
            component: ChargeStationWhitelistComponent,
          },
          {
            path: 'load-management',
            component: ChargeStationLoadManagementComponent,
          },
          {
            path: 'occp',
            component: ChargeStationOccpComponent,
          },
          {
            path: 'user-data',
            component: ChargeStationUserDataComponent,
          },
          {
            path: 'charge-point-configuration',
            component: ChargePointConfigurationComponent,
          },
          {
            path: 'charge-point-edit',
            component: ChargingPointEditComponent
          },
          {
            path: 'emails',
            component: ChargeStationEmailsComponent
          }
        ]
      },
      {
        path: 'account',
        component: AccountComponent,
      },
      {
        path: 'users-management',
        component: UsersManagementComponent,
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
