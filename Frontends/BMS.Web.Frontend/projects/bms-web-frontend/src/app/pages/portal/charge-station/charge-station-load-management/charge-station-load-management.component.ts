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
  measuringDeviceType: FormControl
  loadStrategy: FormControl
  current: FormControl
  plannedCurrent: FormControl
  supervisionMeterCurrent: FormControl

  selectedHighDeviceType: string
  selectedDeviceType: string
  selectedLoadStrategy: string

  navigationBtns: NavBtnType[];

  async saveConfiguration(): Promise<void> {
    try {
      this.configuration.loadManagementActive = this.loadManagementActive.value;
      this.configuration.chargePoints = this.chargingPoints.value;
      this.configuration.monitoredCps = this.monitoredCPs.value;

      this.configuration.loadCircuitFuse = this.loadCircuitFuse.value;
      this.configuration.chargingParkName = this.chargingParkName.value;
      this.configuration.highLevelMeasuringDeviceModbus = this.selectedHighDeviceType;
      this.configuration.loadStrategy = this.selectedLoadStrategy;
      this.configuration.eth0IPAddress = this.eth0IPAddress.value;
      this.configuration.measuringDeviceType = this.selectedDeviceType;

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
    this.configuration.chargePoints?.forEach((value) => {
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
      this.eth0IPAddress = new FormControl(this.configuration.eth0IPAddress);
      this.current = new FormControl([new FormControl(this.configuration.currentI1), new FormControl(this.configuration.currentI2), new FormControl(this.configuration.currentI3)]);
      this.plannedCurrent = new FormControl([new FormControl(this.configuration.plannedCurrentI1), new FormControl(this.configuration.plannedCurrentI2), new FormControl(this.configuration.plannedCurrentI3)]);
      this.supervisionMeterCurrent = new FormControl([new FormControl(this.configuration.supervisionMeterCurrentI1), new FormControl(this.configuration.supervisionMeterCurrentI2), new FormControl(this.configuration.supervisionMeterCurrentI3)]);

      this.loadStrategy = new FormControl("Equal Distribution");
      this.highLevelMeasuringDevice = new FormControl([new FormControl("None"), this.eth0IPAddress, new FormControl("RS 485 Modbus")]);
      this.measuringDeviceType = new FormControl([new FormControl("Phoenix Contact EEM377"), new FormControl("Phoenix Contact MA370")]);
      this.chargingPoints = new FormControl(this.configuration.chargePoints);

      this.selectedHighDeviceType = this.eth0IPAddress.value;
      this.selectedDeviceType = "Phoenix Contact MA370";
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
}
