import { Component, OnInit } from '@angular/core';
import { BMSWebApiClientModule } from "../../../../../../../bms-web-api-client/src/lib/bms-web-api-client.module";
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { ChargeStationService } from '../../../../services/charge-station.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { CommandService } from 'projects/bms-web-frontend/src/app/services/command.service';
@Component({
  selector: 'app-charge-station-load-management',
  templateUrl: './charge-station-load-management.component.html',
  styleUrls: ['./charge-station-load-management.component.scss']
})
export class ChargeStationLoadManagementComponent implements OnInit {
  commandType = BMSWebApiClientModule.CommandType;

  constructor(
    public commandService: CommandService,
    private route: ActivatedRoute,
    private chargeStationService: ChargeStationService,
    private snackBar: MatSnackBar,
    public translate: TranslateService,
    private router: Router,
  ) {
    this.route.queryParams
      .subscribe(params => {
        this.id = parseInt(params['id'])
      }
    );}

  id: number = -1;
  configuration: BMSWebApiClientModule.ChargeController = new BMSWebApiClientModule.ChargeController();

  chargingPoints: FormControl
  loadManagementActive: FormControl
  selectAllCPs: boolean
  monitoredCPs: FormControl
  limiting: FormControl
  chargingParkName: FormControl
  loadCircuitFuse: FormControl
  highLevelMeasuringDevice: FormControl
  eth0IPAddress: FormControl
  measuringDeviceTypeForIp: FormControl
  measuringDeviceTypeForModbus: FormControl
  loadStrategy: FormControl
  current: FormControl
  plannedCurrent: FormControl
  supervisionMeterCurrent: FormControl

  selectedHighDeviceType: string
  selectedDeviceType: string
  selectedLoadStrategy: string

  navigationBtns: NavBtnType[];

  initialSelectedDeviceType: string = '';
  initialSelectedRS485Controller: string = '';

  async saveConfiguration(): Promise<void> {
    try {
      this.configuration.chargePoints = this.chargingPoints.value;

      this.configuration.loadCircuitFuse = this.loadCircuitFuse.value;
      this.configuration.chargingParkName = this.chargingParkName.value;
      this.configuration.loadStrategy = this.selectedLoadStrategy;
      this.configuration.loadManagementIpAddress = this.eth0IPAddress.value;

      if (this.selectedHighDeviceType === 'Ip Address') {
        this.configuration.highLevelMeasuringDeviceModbus = 'ipdevice';
        this.configuration.highLevelMeasuringDeviceControllerId = '';
        if (this.selectedDeviceType === "Phoenix Contact EEM377") {
          this.configuration.measuringDeviceType = 'EEM377';
        }
        else {
          this.configuration.measuringDeviceType = 'MA370';
        }
      }
      else {
        this.configuration.measuringDeviceType = '';
        this.configuration.highLevelMeasuringDeviceModbus = '';
        if (this.selectedHighDeviceType === 'RS 485 Modbus') {
          if (this.configuration.chargePoints != undefined) {
            if (this.configuration.chargePoints[0].name === this.selectedDeviceType) {
              this.configuration.highLevelMeasuringDeviceControllerId = this.configuration.chargePoints[0].chargeControllerUid;
            }
            else {
              this.configuration.highLevelMeasuringDeviceControllerId = this.configuration.chargePoints[1].chargeControllerUid;
            }
          }
        }
        else {
          this.configuration.highLevelMeasuringDeviceControllerId = '';
        }
      }

      const chargeControllerCommand: any = {}
      Object.assign(chargeControllerCommand, this.configuration);

      chargeControllerCommand.chargePoints = this.configuration.chargePoints?.filter(chargePoint => chargePoint.enabled).map(chargePoint => chargePoint.serialNumber);

      await this.commandService.postCommand(this.configuration.id, undefined, JSON.stringify(chargeControllerCommand), BMSWebApiClientModule.CommandType.SaveLoadManagement)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }
  }

  selectAllCPsFunction() {
    this.configuration.chargePoints?.forEach((value, index) => {
      value.enabled = !this.selectAllCPs;
    });
  }

  selectCPsFunction(valueSelected?: boolean, index?: number) {
    if (valueSelected === false) {
      this.selectAllCPs = false
      return;
    }

    let flagSelectAllCPs = true;
    this.configuration.chargePoints?.forEach((value, nestedIndex) => {
      if (index !== nestedIndex && !value.enabled) flagSelectAllCPs = false;
    });

    this.selectAllCPs = flagSelectAllCPs;
  }

  async ngOnInit(): Promise<void> {
    try {
      this.configuration = await this.chargeStationService.getChargeController(this.id);

      this.navigationBtns = [{text: this.configuration.serialNumber!}, {text: this.translate.instant("charge-station.details.load-management")}]

      this.monitoredCPs = new FormControl(this.configuration.monitoredCps);
      this.loadManagementActive = new FormControl(this.configuration.loadManagementActive);
      this.limiting = new FormControl(this.configuration.loadManagementActive);
      this.loadCircuitFuse = new FormControl(this.configuration.loadCircuitFuse);
      this.chargingParkName = new FormControl(this.configuration.chargingParkName);
      this.eth0IPAddress = new FormControl(this.configuration.loadManagementIpAddress);
      this.current = new FormControl([new FormControl(this.configuration.currentI1), new FormControl(this.configuration.currentI2), new FormControl(this.configuration.currentI3)]);
      this.plannedCurrent = new FormControl([new FormControl(this.configuration.plannedCurrentI1), new FormControl(this.configuration.plannedCurrentI2), new FormControl(this.configuration.plannedCurrentI3)]);
      this.supervisionMeterCurrent = new FormControl([new FormControl(this.configuration.supervisionMeterCurrentI1), new FormControl(this.configuration.supervisionMeterCurrentI2), new FormControl(this.configuration.supervisionMeterCurrentI3)]);

      this.loadStrategy = new FormControl("Equal Distribution");
      this.highLevelMeasuringDevice = new FormControl([new FormControl("None"), new FormControl("Ip Address"), new FormControl("RS 485 Modbus")]);
      this.measuringDeviceTypeForIp = new FormControl([new FormControl("Phoenix Contact EEM377"), new FormControl("Phoenix Contact MA370")]);
      if (this.configuration.chargePoints != undefined) {
        if (this.configuration.chargePoints.length > 0) {
          this.measuringDeviceTypeForModbus = new FormControl([new FormControl(this.configuration.chargePoints[0].name), new FormControl(this.configuration.chargePoints[1].name)]);
        }
        else {
          this.measuringDeviceTypeForModbus = new FormControl([new FormControl(this.configuration.chargePoints[0].name)]);
        }
      }
      this.chargingPoints = new FormControl(this.configuration.chargePoints);

      if (this.configuration.highLevelMeasuringDeviceModbus === 'ipdevice') {
        this.selectedHighDeviceType = "Ip Address";
        if (this.configuration.measuringDeviceType === 'EEM377') {
          this.selectedDeviceType = "Phoenix Contact EEM377";
        }
        else {
          this.selectedDeviceType = "Phoenix Contact MA370";
        }
        this.initialSelectedDeviceType = this.selectedDeviceType
      }
      if (this.configuration.highLevelMeasuringDeviceControllerId != '') {
        if (!this.selectedHighDeviceType)
          this.selectedHighDeviceType = "RS 485 Modbus";
        if (this.configuration.chargePoints != undefined) {
          if (this.configuration.highLevelMeasuringDeviceControllerId === this.configuration.chargePoints[0].chargeControllerUid) {
            if (!this.selectedDeviceType)
              this.selectedDeviceType = this.configuration.chargePoints[0].name!;
            this.initialSelectedRS485Controller = this.configuration.chargePoints[0].name!
          }
          else {
            if (!this.selectedDeviceType)
              this.selectedDeviceType = this.configuration.chargePoints[1].name!;
            this.initialSelectedRS485Controller = this.configuration.chargePoints[1].name!
          }
        }
      }

      if (!this.selectedHighDeviceType)
        this.selectedHighDeviceType = "None";

      this.selectedLoadStrategy = this.loadStrategy.value;

      this.selectCPsFunction(undefined, undefined);
    } catch {}
  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (valueEmitted.text === this.configuration.serialNumber && this.id !== -1) {
        this.router.navigate(['/portal/charge-station/details'], {queryParams: {id: this.id}});
    } else if (valueEmitted.text === this.translate.instant("charge-station.details.load-management")) {
      window.location.reload();
    }
  }

  preselectOption(event: any) {
    if (this.configuration.highLevelMeasuringDeviceModbus === 'ipdevice' && event === 'RS 485 Modbus' && this.configuration.chargePoints != undefined) {
      this.selectedDeviceType = this.configuration.chargePoints[0].name !== undefined ? this.configuration.chargePoints[0].name :
                                this.configuration.chargePoints[1].name !== undefined ? this.configuration.chargePoints[1].name : '';
    }
    if (this.configuration.highLevelMeasuringDeviceModbus === 'ipdevice' && event === 'Ip Address' && this.configuration.chargePoints != undefined) {
      this.selectedDeviceType = this.initialSelectedDeviceType
    }
    if (this.configuration.highLevelMeasuringDeviceModbus === '' && event === 'Ip Address') {
      this.selectedDeviceType = this.initialSelectedDeviceType
    }
    if (this.configuration.highLevelMeasuringDeviceModbus === '' && event === 'RS 485 Modbus') {
      this.selectedDeviceType = this.initialSelectedRS485Controller;
    }
  }
}
