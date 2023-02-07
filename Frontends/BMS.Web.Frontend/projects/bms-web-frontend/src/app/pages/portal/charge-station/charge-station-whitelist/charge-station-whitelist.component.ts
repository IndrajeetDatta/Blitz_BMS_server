import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import { CommandService } from 'projects/bms-web-frontend/src/app/services/command.service';
import { directionSort, sortArray } from 'projects/bms-web-frontend/src/app/sort-tables';

@Component({
  selector: 'app-charge-station-whitelist',
  templateUrl: './charge-station-whitelist.component.html',
  styleUrls: ['./charge-station-whitelist.component.scss']
})
export class ChargeStationWhitelistComponent implements OnInit {
  commandType = BMSWebApiClientModule.CommandType;

  constructor(
    public commandService: CommandService,
    private route: ActivatedRoute, 
    private chargeStationService: ChargeStationService, 
    private router: Router,
    private snackBar: MatSnackBar,
    public translate: TranslateService
    ) {
    this.route.queryParams
      .subscribe(params => {
        this.id = parseInt(params['id'])
      }
    );
  }

  searchContol: FormControl = new FormControl();

  page = 1;
  pageSize = 50;
  collectionSize = 0;
  pageSizes = [50, 100, 150];
  isInEditMode = false;

  directionSorting: { [key in keyof BMSWebApiClientModule.Rfid]?: directionSort } =
  {
    serialNumber: '',
    name: '',
    type: '',
    expiryDate: '',
    evConsumptionRateKWhPer100KM: '',
  }

  id: number = -1;
  chargeController: BMSWebApiClientModule.ChargeController;
  whiteListData: BMSWebApiClientModule.Rfid[] = [];
  whiteListDataMockup: BMSWebApiClientModule.Rfid[] = [];

  navigationBtns: NavBtnType[];
  
  async ngOnInit(): Promise<void> {
    this.searchContol.valueChanges.subscribe((val: string | null) => {
        this.refreshStationData()
        if (!val) return

        this.whiteListData = this.whiteListData.filter(whiteListElem =>
          (whiteListElem.serialNumber && whiteListElem.serialNumber.toLocaleLowerCase().includes(val.toLocaleLowerCase())) ||
          (whiteListElem.name && whiteListElem.name.toLocaleLowerCase().includes(val.toLocaleLowerCase())))
      }
    )

    try {
      this.chargeController = await this.chargeStationService.getChargeControllerWithWhitelist(this.id);
      this.whiteListData = this.chargeController.whitelistRFIDs || [];
      this.whiteListDataMockup = this.whiteListData;

      this.navigationBtns = [{text: this.chargeController.serialNumber!}, {text: this.translate.instant("whitelist.whitelist")}]

      this.collectionSize = this.whiteListData.length
    } catch (error) {
      console.log("ERROR fetch chargeController on detials page")
    }
  }

  cancelEdit() {
    this.isInEditMode = false;
  }

  startEditMode() {
    this.isInEditMode = true;
  }

  async deleteRFID(position: number): Promise<void> {
    try {
      if (!this.whiteListData[position].serialNumber)
        throw "No serialNumber";

      await this.commandService.postCommand(this.chargeController.id, undefined, undefined, BMSWebApiClientModule.CommandType.DeleteRFID, this.whiteListData[position].serialNumber)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }
  }

  async saveEditChanges(position: number): Promise<void> {
    this.isInEditMode = false;

    try {
      if (!this.whiteListData[position].serialNumber)
        throw "No serialNumber";

      await this.commandService.postCommand(this.chargeController.id, undefined, JSON.stringify(this.whiteListData[position]), BMSWebApiClientModule.CommandType.EditRFID, this.whiteListData[position].serialNumber)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }

  }

  async importRFIDs(): Promise<void> {
    try {
      await this.commandService.postCommand(this.chargeController.id, undefined, undefined, BMSWebApiClientModule.CommandType.ImportRFIDs)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }

  }

  refreshStationData() {
    this.whiteListData = this.whiteListDataMockup.slice(
      (this.page - 1) * this.pageSize,
      (this.page - 1) * this.pageSize + this.pageSize
    );
  }

  sortTable(columnSorting: keyof BMSWebApiClientModule.Rfid) {
    if (this.directionSorting[columnSorting] === '') this.directionSorting[columnSorting] = 'asc'
    else if (this.directionSorting[columnSorting] === 'asc') this.directionSorting[columnSorting] = 'desc'
    else this.directionSorting[columnSorting] = ''

    this.whiteListDataMockup = sortArray(this.whiteListDataMockup, this.directionSorting[columnSorting]!, columnSorting);
  }

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return "-"
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

  exportList() {
    //define the heading for each row of the data
    let csv = 'Name,ID,Type,Expiry Date,EV consumption rate,Allow Charging\n';
    //merge the data with CSV
    this.whiteListDataMockup.forEach(function(whiteListElem) {
      const {name, serialNumber, type, expiryDate, evConsumptionRateKWhPer100KM, allowCharging} = whiteListElem
      csv += `${name},${serialNumber},${type},${expiryDate},${evConsumptionRateKWhPer100KM},${allowCharging},`
      csv += "\n";
    });

    const hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';

    //provide the name for the CSV file to be downloaded
    hiddenElement.download = `ChargeController-${this.id}_WhiteList.csv`;
    hiddenElement.click();
  }

  async deleteAll() : Promise<void> {
    try {
      await this.commandService.postCommand(this.chargeController.id, undefined, undefined, BMSWebApiClientModule.CommandType.DeleteAllRFIDs)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }

  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (valueEmitted.text === this.chargeController.serialNumber && this.id !== -1) {
        this.router.navigate(['/portal/charge-station/details'], {queryParams: {id: this.id}});
    } else if (valueEmitted.text === this.translate.instant("whitelist.whitelist")) {
      window.location.reload();
    } 
  }

  watchLastMaintenance(position: number, valueEmmited: string) {
    this.whiteListData[position].expiryDate = new Date(valueEmmited);
  }
}
