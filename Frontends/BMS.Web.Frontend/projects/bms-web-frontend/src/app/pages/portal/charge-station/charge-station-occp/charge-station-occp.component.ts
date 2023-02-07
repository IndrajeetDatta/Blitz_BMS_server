import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import { directionSort, sortArray } from 'projects/bms-web-frontend/src/app/sort-tables';
import { TranslateService } from '@ngx-translate/core';
import { OcppMessageDialogComponent } from './ocpp-message-dialog/ocpp-message-dialog.component';
import { formatDate } from '@angular/common';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { CommandService } from 'projects/bms-web-frontend/src/app/services/command.service';

@Component({
  selector: 'app-charge-station-occp',
  templateUrl: './charge-station-occp.component.html',
  styleUrls: ['./charge-station-occp.component.scss']
})
export class ChargeStationOccpComponent implements OnInit {
  commandType = BMSWebApiClientModule.CommandType;
  
  constructor(
    public commandService: CommandService,
    private ocppMessageDialog: MatDialog,
    private route: ActivatedRoute,
    private chargeStationService: ChargeStationService,
    private router: Router,
    private snackBar: MatSnackBar,
    public translate: TranslateService,
  ) {
    this.route.queryParams
      .subscribe(params => {
        this.id = parseInt(params['id'])
      }
    );
  }

  page = 1;
  pageSize = 50;
  collectionSize = 0;
  pageSizes = [50, 100, 150]
  directionSorting: { [key in keyof BMSWebApiClientModule.OcppStatus]?: directionSort } =
  {
    id: '',
    status: '',
    occpStatusSentDate: '',
    occpStatus: '',
  }
  
  dtOptions: DataTables.Settings = {};

  id = -1;
  ocppStatusData: BMSWebApiClientModule.OcppStatus[] = [];
  ocppStatusDataMockup: BMSWebApiClientModule.OcppStatus[] = [];
  ocppMessageData: BMSWebApiClientModule.OcppMessage[] = [];
  ocppMessageDataMockup: BMSWebApiClientModule.OcppMessage[] = [];
  ocppConfigData: BMSWebApiClientModule.OcppConfig;
  chargeController: BMSWebApiClientModule.ChargeController;

  formOCPPVersion: FormControl
  formNetworkInterface: FormControl
  formBackendURL: FormControl
  formServiceRFID: FormControl
  formFreeModeRfid: FormControl
  formModel: FormControl
  formVendor: FormControl
  formSerialNumber: FormControl
  formChargeBoxId: FormControl
  formChargeBoxSerialNumber: FormControl
  formIccid: FormControl
  formImsi: FormControl
  formMeterType: FormControl
  formMeterSerialNumber: FormControl

  navigationBtns: NavBtnType[];
  
  async ngOnInit(): Promise<void> {
    this.dtOptions = {
      order:[[0, 'desc']] // '0' is the timestamp column(1st column) and 'desc' is the sorting order
    }

    try {
      const response: BMSWebApiClientModule.Anonymous = await this.chargeStationService.getChargeStationOcpp(this.id);
      this.ocppConfigData = response.ocppConfig!;
      this.ocppStatusData = response.ocppStatus!;
      this.ocppStatusDataMockup = response.ocppStatus!;
      this.ocppMessageData = response.ocppMessages!;
      this.ocppMessageDataMockup = response.ocppMessages!;
      this.chargeController = this.ocppConfigData.chargeController!;
      if (!this.chargeController) {
        this.router.navigate(['/portal/charge-station/details'], {queryParams: {id: this.id}});
        return
      } else {
        this.navigationBtns = [{text: this.chargeController.serialNumber!}, {text: 'OCCP'}]
      }
      this.collectionSize = this.ocppStatusData.length

      this.formOCPPVersion = new FormControl(this.ocppConfigData.ocppProtocolVersion);
      this.formNetworkInterface = new FormControl(this.ocppConfigData.networkInterface);
      this.formBackendURL = new FormControl(this.ocppConfigData.backendURL)
      this.formServiceRFID = new FormControl(this.ocppConfigData.serviceRFID)
      this.formFreeModeRfid = new FormControl(this.ocppConfigData.freeModeRFID)
      this.formModel = new FormControl(this.ocppConfigData.chargeStationModel)
      this.formVendor = new FormControl(this.ocppConfigData.chargeStationVendor)
      this.formSerialNumber = new FormControl(this.ocppConfigData.chargeStationSerialNumber)
      this.formChargeBoxId = new FormControl(this.ocppConfigData.chargeBoxID)
      this.formChargeBoxSerialNumber = new FormControl(this.ocppConfigData.chargeBoxSerialNumber)
      this.formIccid = new FormControl(this.ocppConfigData.iccid)
      this.formImsi = new FormControl(this.ocppConfigData.imsi)
      this.formMeterType = new FormControl(this.ocppConfigData.meterType)
      this.formMeterSerialNumber = new FormControl(this.ocppConfigData.meterSerialNumber)

      this.refreshStationData()
    } catch (error) {
      console.log("ERROR fetch chargeController on detials page")
    }
  }

  refreshStationData() {
    this.ocppStatusData = this.ocppStatusDataMockup.slice(
      (this.page - 1) * this.pageSize,
      (this.page - 1) * this.pageSize + this.pageSize
    );
  }

  sortTable(columnSorting: keyof BMSWebApiClientModule.OcppStatus) {
    if (this.directionSorting[columnSorting] === '') this.directionSorting[columnSorting] = 'asc'
    else if (this.directionSorting[columnSorting] === 'asc') this.directionSorting[columnSorting] = 'desc'
    else this.directionSorting[columnSorting] = ''
    
    this.ocppStatusDataMockup = sortArray(this.ocppStatusDataMockup, this.directionSorting[columnSorting]!, columnSorting);
    this.refreshStationData();
  }

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return "-"
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

  showMessage(message: BMSWebApiClientModule.OcppMessage) {
    this.ocppMessageDialog.open(OcppMessageDialogComponent, {data: message});
  }

  async saveConfiguration(): Promise<void> {
    try {
      this.ocppConfigData.ocppProtocolVersion = this.formOCPPVersion.value;
      this.ocppConfigData.networkInterface = this.formNetworkInterface.value;
      this.ocppConfigData.backendURL = this.formBackendURL.value;
      this.ocppConfigData.serviceRFID = this.formServiceRFID.value;
      this.ocppConfigData.freeModeRFID = this.formFreeModeRfid.value;
      this.ocppConfigData.chargeStationModel = this.formModel.value;
      this.ocppConfigData.chargeStationVendor = this.formVendor.value;
      this.ocppConfigData.chargeStationSerialNumber = this.formSerialNumber.value;
      this.ocppConfigData.chargeBoxID = this.formChargeBoxId.value;
      this.ocppConfigData.chargeBoxSerialNumber = this.formChargeBoxSerialNumber.value
      this.ocppConfigData.iccid = this.formIccid.value
      this.ocppConfigData.imsi = this.formImsi.value
      this.ocppConfigData.meterType = this.formMeterType.value
      this.ocppConfigData.meterSerialNumber = this.formMeterSerialNumber.value

      await this.commandService.postCommand(this.chargeController.id, undefined, JSON.stringify(this.ocppConfigData), BMSWebApiClientModule.CommandType.OcppConfig)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }
  }

  async exportOcppMessages() {
    //define the heading for each row of the data
    let csv = 'Timestamp,Type,Action,Message Data\n';
    //merge the data with CSV
    const that = this;
    const allOcppMessage = await this.chargeStationService.getChargeStationAllOcppMessages(this.chargeController.id!)
    allOcppMessage.forEach(function(ocppMessage) {
      let {timestamp, action, type, messageData } = ocppMessage

      messageData = messageData?.replace(/(\r\n|\n|\r|\s+|\t|&nbsp;)/gm,' ');
      messageData = messageData?.replace(/"/g, '""');
      messageData = messageData?.replace(/ +(?= )/g,'');
      
      csv += `${that.getDate(timestamp)},${type},${action},"${messageData}"`
      csv += "\n";
    });

    const hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';

    //provide the name for the CSV file to be downloaded
    hiddenElement.download = `ChargeController-${this.chargeController.serialNumber}_OCPPMessages.csv`;
    hiddenElement.click();
  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (valueEmitted.text === this.chargeController.serialNumber && this.id !== -1) {
        this.router.navigate(['/portal/charge-station/details'], {queryParams: {id: this.id}});
    } else if (valueEmitted.text === 'OCCP') {
      window.location.reload();
    } 
  }
}