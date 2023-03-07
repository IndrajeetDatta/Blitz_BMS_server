import { Component, OnInit } from '@angular/core';
import { NavigationStart, ActivatedRoute } from '@angular/router';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import { filter, first } from 'rxjs';
import { Router } from '@angular/router';
import { directionSort, sortArray } from 'projects/bms-web-frontend/src/app/sort-tables';
import { formatDate } from '@angular/common';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { CommandService } from 'projects/bms-web-frontend/src/app/services/command.service';
import { AuthenticationService } from 'projects/bms-web-frontend/src/app/services/api/authentication.service';
import { MatDialog } from '@angular/material/dialog';
import { UserAccessDialogComponent } from './user-access-dialog/user-access-dialog.component';

@Component({
  selector: 'app-charge-station-details',
  templateUrl: './charge-station-details.component.html',
  styleUrls: ['./charge-station-details.component.scss']
})
export class ChargeStationDetailsComponent implements OnInit {
  commandType = BMSWebApiClientModule.CommandType;

  page = 1;
  pageSize = 50;
  collectionSize = 0;
  pageSizes = [50, 100, 150]
  directionSorting: { [key in keyof BMSWebApiClientModule.ChargePoint]?: directionSort } =
  {
    serialNumber: '',
    chargeControllerUid: '',
    name: '',
    state: '',
    chargingTimeInSeconds: '',
    chargingRate: '',
  }

  lat = 51.678418;
  lng = 7.809007;

  id: number = -1;
  chargeController: BMSWebApiClientModule.ChargeController;
  stationsPointData: BMSWebApiClientModule.ChargePoint[] = [];
  stationsPointDataMockup: BMSWebApiClientModule.ChargePoint[] = [];
  installers: BMSWebApiClientModule.ApplicationUser[] = [];
  newInstaller: BMSWebApiClientModule.ApplicationUser;

  navigationBtns: NavBtnType[];

  lastMaintenance: Date;
  isAdmin: boolean = true;
  noOptionsArray = ["No options available"];
  noOption = '';

  constructor(
    public commandService: CommandService,
    private route: ActivatedRoute,
    private chargeStationService: ChargeStationService,
    public authenticationService: AuthenticationService,
    private router: Router,
    private snackBar: MatSnackBar,
    public translate: TranslateService,
    private installersDialog: MatDialog,
  ) {
    this.route.queryParams
      .subscribe(params => {
        this.id = parseInt(params['id'])
      }
    );
  }

  async ngOnInit(): Promise<void> {
    try {
      this.isAdmin = await this.authenticationService.isAdmin();
      this.chargeController = await this.chargeStationService.getChargeController(this.id);
      this.installers = await this.chargeStationService.getInstallersForChargeStation(this.id);
      if (this.chargeController.installer != undefined && this.chargeController.installer != null) {
        this.installers.forEach(installer => {
          if(installer.id === this.chargeController.installer?.id)
          this.newInstaller = installer;
        });
      }
      else {
        this.newInstaller = this.installers[0];
      }
      this.stationsPointData = this.chargeController.chargePoints || [];
      this.stationsPointDataMockup = this.stationsPointData;

      this.navigationBtns = [{text: this.chargeController.serialNumber!}]
      this.collectionSize = this.stationsPointData.length
    } catch (error) {
      console.log("ERROR fetch chargeController on detials page")
    }
  }

  printNewInstaller() {
    this.chargeController.installer = this.newInstaller;
  }

  refreshStationData() {
    this.stationsPointData = this.stationsPointDataMockup.slice(
      (this.page - 1) * this.pageSize,
      (this.page - 1) * this.pageSize + this.pageSize
    );
  }

  sortTable(columnSorting: keyof BMSWebApiClientModule.ChargePoint) {
    if (this.directionSorting[columnSorting] === '') this.directionSorting[columnSorting] = 'asc'
    else if (this.directionSorting[columnSorting] === 'asc') this.directionSorting[columnSorting] = 'desc'
    else this.directionSorting[columnSorting] = ''

    this.stationsPointDataMockup = sortArray(this.stationsPointDataMockup, this.directionSorting[columnSorting]!, columnSorting);
    this.refreshStationData();
  }

  async rebootCommand(): Promise<void> {
    try {
      await this.commandService.postCommand(this.chargeController.id, undefined, undefined, BMSWebApiClientModule.CommandType.RestartApp)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }
  }

  async restartApp(commandType: BMSWebApiClientModule.CommandType): Promise<void> {
    if (this.commandService.disableCommandButtons)
      return
    try {
      await this.commandService.postCommand(this.chargeController.id, undefined, undefined, commandType)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }
  }

  async toggleChargePoint(chargePointIndex: number): Promise<void> {
    try {
      const chargePoint = new BMSWebApiClientModule.ChargePoint(this.stationsPointData[chargePointIndex]);
      chargePoint.externalRelease =  !chargePoint.externalRelease;

      await this.commandService.postCommand(undefined, chargePoint.id, JSON.stringify(chargePoint), BMSWebApiClientModule.CommandType.EnableDisableChargePoint, chargePoint.chargePointUid)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }
  }

  async changeTestMode() {
    if (this.chargeController.allowTestModeCommands) this.chargeController.allowTestModeCommands = false
    else this.chargeController.allowTestModeCommands = true
    
    try {
      const response = await this.chargeStationService.updateConfiguration(this.chargeController, this.chargeController.id!);

      this.snackBar.open(this.translate.instant("general.change-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }
  }

  editChargePoint(id?: number) {
    this.router.navigate(['/portal/charge-station/charge-point-edit'], {queryParams: {id}});
  }

  getConfiguration(id?: number) {
    this.router.navigate(['/portal/charge-station/configuration'], {queryParams: {id}});
  }

  getLoadManagement(id?: number) {
    this.router.navigate(['/portal/charge-station/load-management'], {queryParams: {id}});
  }

  getWhiteList(id?: number) {
    this.router.navigate(['/portal/charge-station/whitelist'], {queryParams: {id}});
  }

  getOCCP(id?: number) {
    this.router.navigate(['/portal/charge-station/occp'], {queryParams: {id}});
  }

  getUserData(id?: number) {
    this.router.navigate(['/portal/charge-station/user-data'], {queryParams: {id}});
  }

  getTransactions(id?: number) {
    this.router.navigate(['/portal/charge-station/user-data'], {queryParams: {id}});
  }

  getCommandHistory(masterId?: string, id?: number) {
    this.router.navigate(['/portal/charge-station/commands-history'], {queryParams: {masterId, id}});
  }

  getLogs(id?: number) {
    this.router.navigate(['/portal/charge-station/logs'], {queryParams: {id}});
  }

  getEmails(id?: number) {
    this.router.navigate(['/portal/charge-station/emails'], {queryParams: {id}});
  }

  showAllInstallers(id?: number) {
    this.installersDialog.open(UserAccessDialogComponent, {data: this.installers});
  }

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return "-"
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

  convertToTimeString(date? : Date) : string {
    if (!date) return '-';

    date = new Date(date.toLocaleString())

    let returnStringDate: string = date.toTimeString()
    for (let i = 0; i < 9; i++) {
      const auxDate: string = returnStringDate.replace(`+0${i}00`, `+0${i}:00`);
      if (returnStringDate !== auxDate) {
        returnStringDate = auxDate
        break;
      }
    }
    return returnStringDate
  }

  secondsToDays(seconds? : number) : string {
    if (!seconds) return '-';

    var d = Math.floor(seconds / (3600*24));
    var h = Math.floor(seconds % (3600*24) / 3600);
    var m = Math.floor(seconds % 3600 / 60);
    var s = Math.floor(seconds % 60);

    var dDisplay = d > 0 ? d + "d " : "";
    var hDisplay = h > 0 ? h + "h " : "";
    var mDisplay = m > 0 ? m + "m " : "";
    var sDisplay = s > 0 ? s + "s" : "";
    return dDisplay + hDisplay + mDisplay + sDisplay;
  }

  getStatusBgColor(state?: string) {
    if (state === 'A1')
      return 'green';
    else if (state === 'C2')
    return 'cyan';
    else if (state === 'E')
    return 'red';
    else if (state === 'B2')
    return 'orange';

    return 'white';
  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (valueEmitted.text === this.chargeController.serialNumber) {
      window.location.reload();
    }
  }

  watchLastMaintenance(valueEmmited: string) {
    this.lastMaintenance = new Date(valueEmmited);
  }

  async setLastMaintenance(): Promise<void> {
    try {
      this.chargeController.installer = this.newInstaller;
      this.chargeController.lastMaintenance = this.lastMaintenance;

      const newChargeController = await this.chargeStationService.updateConfiguration(this.chargeController, this.chargeController.id!);
      if (newChargeController.id) this.chargeController.lastMaintenance = newChargeController.lastMaintenance;
      else throw "Could not save";

      this.snackBar.open(this.translate.instant("charge-station.details.last-maintenance-success-set"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }
  }
}
