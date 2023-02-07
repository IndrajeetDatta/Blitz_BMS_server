// Core
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { HttpClient, HttpClientModule } from '@angular/common/http';

// BMS
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { PortalComponent } from './pages/portal/portal.component';
import { OverviewComponent } from './pages/portal/charge-station/overview/overview.component';
import { ChargeStationDetailsComponent } from './pages/portal/charge-station/charge-station-details/charge-station-details.component';
import { ChargeStationConfigurationComponent } from './pages/portal/charge-station/charge-station-configuration/charge-station-configuration.component';
import { ChargeStationWhitelistComponent } from './pages/portal/charge-station/charge-station-whitelist/charge-station-whitelist.component';
import { ChargeStationLoadManagementComponent } from './pages/portal/charge-station/charge-station-load-management/charge-station-load-management.component';
import { ChargeStationOccpComponent } from './pages/portal/charge-station/charge-station-occp/charge-station-occp.component';
import { ChargeStationUserDataComponent } from './pages/portal/charge-station/charge-station-user-data/charge-station-user-data.component';
import { ChargePointConfigurationComponent } from './pages/portal/charge-station/charge-point-configuration/charge-point-configuration.component';
import { AccountComponent } from './pages/portal/account/account.component';
import { UsersManagementComponent } from './pages/portal/admin/users-management/users-management.component';
import { ChargeStationComponent } from './pages/portal/charge-station/charge-station.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {DataTablesModule} from 'angular-datatables';
import { MatSelectModule } from '@angular/material/select';
import {MatTooltipModule} from '@angular/material/tooltip';

//Google maps
import { AgmCoreModule } from '@agm/core';


//Translation
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { Injector, APP_INITIALIZER } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LOCATION_INITIALIZED} from '@angular/common';
import { NgbdSortableHeader } from './pages/portal/charge-station/overview/sortable.directive';
import { AccountSettingsDialogComponent } from './pages/portal/account-settings-dialog/account-settings-dialog.component';
import { CellTableStationConfigurationComponent } from './compoenents/station-configuration/cell-table-station-configuration/cell-table-station-configuration.component';
import { ChargingPointEditComponent } from './pages/portal/charge-station/charging-point-edit/charging-point-edit.component';
import { OcppMessageDialogComponent } from './pages/portal/charge-station/charge-station-occp/ocpp-message-dialog/ocpp-message-dialog.component';
import { HeaderNavigationComponent } from './compoenents/header-navigation/header-navigation/header-navigation.component';
import { DatePickerComponent } from './compoenents/date-picker/date-picker/date-picker.component';
import { ChargeStationCommandHistoryComponent } from './pages/portal/charge-station/charge-station-command-history/charge-station-command-history.component';
import { CommandHistoryDialogComponent } from './pages/portal/charge-station/charge-station-command-history/command-history-dialog/command-history-dialog/command-history-dialog.component';
import { TransactionDialogComponent } from './pages/portal/charge-station/charge-station-user-data/transaction-dialog/transaction-dialog.component';
import { CommandService } from './services/command.service';

//Auth0
import { AuthModule } from '@auth0/auth0-angular';
import {environment as env} from '../environments/environment';
import { ChargeStationEmailsComponent } from './pages/portal/charge-station/charge-station-emails/charge-station-emails.component'

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PortalComponent,
    OverviewComponent,
    ChargeStationDetailsComponent,
    ChargeStationConfigurationComponent,
    ChargeStationWhitelistComponent,
    ChargeStationLoadManagementComponent,
    ChargeStationOccpComponent,
    ChargeStationUserDataComponent,
    ChargePointConfigurationComponent,
    AccountComponent,
    UsersManagementComponent,
    ChargeStationComponent,
    NgbdSortableHeader,
    AccountSettingsDialogComponent,
    CellTableStationConfigurationComponent,
    ChargingPointEditComponent,
    OcppMessageDialogComponent,
    HeaderNavigationComponent,
    DatePickerComponent,
    ChargeStationCommandHistoryComponent,
    CommandHistoryDialogComponent,
    ChargeStationCommandHistoryComponent,
    CommandHistoryDialogComponent,
    TransactionDialogComponent,
    ChargeStationEmailsComponent
  ],
  imports: [
    // Core
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    //@angular/material
    MatDialogModule,
    MatSnackBarModule,
    MatCheckboxModule,
    MatIconModule,
    // BMS
    AppRoutingModule,
    NgbModule,
    DataTablesModule,
    MatSelectModule,
    MatTooltipModule,
    // Bootstrap
    TimepickerModule.forRoot(),
    PopoverModule.forRoot(),
    // Google maps
    AgmCoreModule.forRoot({
      apiKey: ''
    }),

    // Translation
    TranslateModule.forRoot({
      loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
      }
    }),
    AuthModule.forRoot({
      ...env.auth0,
      cacheLocation: 'localstorage',
    })
  ],
  providers: [
  {
    provide: APP_INITIALIZER,
    useFactory: ApplicationInitializerFactory,
    deps: [TranslateService, Injector],
    multi: true
  },
  CommandService],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}

export function ApplicationInitializerFactory(translate: TranslateService, injector: Injector) {
  console.log(translate);
  return () => new Promise<any>((resolve: any) => {
    const locationInitialized = injector.get(LOCATION_INITIALIZED, Promise.resolve(null));
    locationInitialized.then(() => {
      translate.addLangs(['en']);
      let selectedLanguage = localStorage.getItem('language');
      if (selectedLanguage) {
        translate.setDefaultLang(selectedLanguage);
      }
      else {
        translate.setDefaultLang('en');
        selectedLanguage = 'en';
        localStorage.setItem("language", 'en');
      }

      translate.use(selectedLanguage).subscribe(() => {
        console.info(`Successfully initialized '${selectedLanguage}' language.'`);
      }, err => {
        console.error(`There was a problem with '${selectedLanguage}' language initialization.'`);
      }, () => {
        resolve(null);
      });
    });
  });
}
