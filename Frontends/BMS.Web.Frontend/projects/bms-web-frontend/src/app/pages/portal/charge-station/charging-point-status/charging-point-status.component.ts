import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import {
  ddEnergyType,
  ddPhaseRotation,
  ddReleaseChargingMode,
  ddHighLvlCommunication,
  ddRfidReaderType,
  ddType,
  ddRfidReader,
} from './drop-downs';
import { CommandService } from 'projects/bms-web-frontend/src/app/services/command.service';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';

@Component({
  selector: 'app-charging-point-status',
  templateUrl: './charging-point-status.component.html',
  styleUrls: ['./charging-point-status.component.scss'],
})
export class ChargingPointStatusComponent implements OnInit {
  commandType = BMSWebApiClientModule.CommandType;

  constructor(
    public commandService: CommandService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
    public translate: TranslateService,
    private chargeStationService: ChargeStationService
  ) {
    this.route.queryParams.subscribe((params) => {
      this.id = parseInt(params['id']);
    });
  }

  navigationBtns: NavBtnType[];

  id: number = -1;
  chargePoint: BMSWebApiClientModule.ChargePoint;
  formName: FormControl;
  formLocation: FormControl;
  formEnergyType: FormControl;
  formPhaseRotation: FormControl;
  formCurrentMinimumInAmpers: FormControl;
  formCurrentMaximumInAmpers: FormControl;
  formFallbackCurrentInAmpers: FormControl;
  formFallbackTimeInSeconds: FormControl;
  formChargingMode: FormControl;
  formRfidReader: FormControl;
  formRfidReaderType: FormControl;
  formRfidTimeoutInSeconds: FormControl;
  formOcppConnectorId: FormControl;

  async ngOnInit(): Promise<void> {
    try {
      this.chargePoint = await this.chargeStationService.getChargePoint(
        this.id
      );

      this.navigationBtns = [
        { text: this.chargePoint.chargeController?.serialNumber! },
        { text: this.translate.instant('overview.cp-status') },
      ];

      this.formName = new FormControl(this.chargePoint.name);
      this.formLocation = new FormControl(this.chargePoint.location);
      this.formEnergyType = new FormControl(this.chargePoint.energyType);
      this.formPhaseRotation = new FormControl(this.chargePoint.phaseRotation);
      this.formCurrentMinimumInAmpers = new FormControl(
        this.chargePoint.chargeCurrentMinimumInAmpers
      );
      this.formCurrentMaximumInAmpers = new FormControl(
        this.chargePoint.chargeCurrentMaximumInAmpers
      );
      this.formFallbackCurrentInAmpers = new FormControl(
        this.chargePoint.fallbackCurrentInAmpers
      );
      this.formFallbackTimeInSeconds = new FormControl(
        this.chargePoint.fallbackTimeInSeconds
      );
      this.formChargingMode = new FormControl(
        this.chargePoint.releaseChargingMode
      );
      this.formRfidReader = new FormControl(this.chargePoint.rfidReader);
      this.formRfidReaderType = new FormControl(
        this.chargePoint.rfidReaderType
      );
      this.formRfidTimeoutInSeconds = new FormControl(
        this.chargePoint.rfidTimeoutInSeconds
      );
      this.formOcppConnectorId = new FormControl(
        this.chargePoint.ocppConnectorId
      );

      setTimeout(() => {
        this.fillDropDown(
          'selectEnergyType',
          ddEnergyType,
          this.chargePoint.energyType
        );
        this.fillDropDown(
          'selectPhaseRotation',
          ddPhaseRotation,
          this.chargePoint.phaseRotation
        );
        this.fillDropDown(
          'selectChargingMode',
          ddReleaseChargingMode,
          this.chargePoint.releaseChargingMode
        );
        this.fillDropDown(
          'selectRfidReader',
          ddRfidReader,
          this.chargePoint.rfidReader
        );
        this.fillDropDown(
          'selectRfidReaderType',
          ddRfidReaderType,
          this.chargePoint.rfidReaderType
        );
        this.fillDropDown(
          'selectHighLevelCommunication',
          ddHighLvlCommunication,
          this.chargePoint.highLevelCommunication
        );
      }, 1500);
    } catch (error) {
      console.log('ERROR fetch chargeController on detials page', error);
    }
  }

  async saveConfiguration(): Promise<void> {
    try {
      this.chargePoint.name = this.formName.value;
      this.chargePoint.location = this.formLocation.value;
      this.chargePoint.energyType =
        this.getSelectedValueDropDown('selectEnergyType');
      this.chargePoint.phaseRotation = this.getSelectedValueDropDown(
        'selectPhaseRotation'
      );
      this.chargePoint.chargeCurrentMinimumInAmpers =
        this.formCurrentMinimumInAmpers.value;
      this.chargePoint.chargeCurrentMaximumInAmpers =
        this.formCurrentMaximumInAmpers.value;
      this.chargePoint.fallbackCurrentInAmpers =
        this.formFallbackCurrentInAmpers.value;
      this.chargePoint.fallbackTimeInSeconds =
        this.formFallbackTimeInSeconds.value;
      this.chargePoint.releaseChargingMode =
        this.getSelectedValueDropDown('selectChargingMode');
      // this.chargePoint.rfidReader = this.formRfidReader.value;
      this.chargePoint.rfidReader =
        this.getSelectedValueDropDown('selectRfidReader');
      this.chargePoint.rfidReaderType = this.getSelectedValueDropDown(
        'selectRfidReaderType'
      );
      this.chargePoint.rfidTimeoutInSeconds =
        this.formRfidTimeoutInSeconds.value;
      this.chargePoint.ocppConnectorId = this.formOcppConnectorId.value;
      this.chargePoint.highLevelCommunication = this.getSelectedValueDropDown(
        'selectHighLevelCommunication'
      );

      const command: BMSWebApiClientModule.Command =
        new BMSWebApiClientModule.Command({
          type: BMSWebApiClientModule.CommandType.SaveChargingPoint,
          payload: JSON.stringify(this.chargePoint),
          chargePointId: this.chargePoint.id,
        });

      await this.commandService.postCommand(
        undefined,
        this.chargePoint.id,
        JSON.stringify(this.chargePoint),
        BMSWebApiClientModule.CommandType.SaveChargingPoint,
        this.chargePoint.chargePointUid
      );
      this.snackBar.open(
        this.translate.instant('general.command-submitted'),
        this.translate.instant('general.ok'),
        { duration: 3000 }
      );
    } catch (e) {
      this.snackBar.open(
        this.translate.instant('general.wrong'),
        this.translate.instant('general.ok'),
        { duration: 3000 }
      );
    }
  }

  // Drop Downs
  getSelectedValueDropDown(selectId: string): string {
    const selectElement = <HTMLSelectElement>document.getElementById(selectId);
    return selectElement.value;
  }

  appendOptions(
    selectId: string,
    dropdown: ddType,
    selectedValue: string | undefined
  ) {
    const selectElement = <HTMLSelectElement>document.getElementById(selectId);
    dropdown.forEach((option) => {
      const optionElement = document.createElement('option');
      optionElement.value = option.fetchValue;
      optionElement.text = option.displayedValue;
      selectElement.append(optionElement);
      if (selectedValue === option.fetchValue) {
        selectElement.value = selectedValue;
      }
    });
  }

  chooseOption(selectId: string, selectedValue: string | undefined) {
    const selectElement = <HTMLSelectElement>document.getElementById(selectId);

    for (let i = 0; i < selectElement.options.length; i++) {
      if (selectedValue === selectElement.options[i].value) {
        selectElement.value = selectedValue;
        break;
      }
    }
  }

  fillDropDown(
    id: string,
    dropdown: ddType,
    selectedValue: string | undefined
  ) {
    this.appendOptions(id, dropdown, selectedValue);
    // this.chooseOption(id, selectedValue);
  }

  getChargeController() {
    this.router.navigate(['/portal/charge-station/details'], {
      queryParams: { id: this.chargePoint.chargeController!.id },
    });
  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (
      valueEmitted.text === this.chargePoint.chargeController!.serialNumber &&
      this.id !== -1
    ) {
      this.router.navigate(['/portal/charge-station/details'], {
        queryParams: { id: this.chargePoint.chargeController!.id },
      });
    } else if (
      valueEmitted.text === this.translate.instant('overview.cp-status')
    ) {
      window.location.reload();
    }
  }
}
