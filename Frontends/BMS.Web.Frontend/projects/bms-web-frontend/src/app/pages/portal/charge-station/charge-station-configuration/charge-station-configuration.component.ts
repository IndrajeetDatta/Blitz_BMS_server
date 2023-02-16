import { Component, OnInit } from '@angular/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { ChargeStationService } from '../../../../services/charge-station.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { CommandService } from 'projects/bms-web-frontend/src/app/services/command.service';

@Component({
  selector: 'app-charge-station-configuration',
  templateUrl: './charge-station-configuration.component.html',
  styleUrls: ['./charge-station-configuration.component.scss'],
})
export class ChargeStationConfigurationComponent implements OnInit {
  commandType = BMSWebApiClientModule.CommandType;
  configuration: BMSWebApiClientModule.ChargeController =
    new BMSWebApiClientModule.ChargeController();
  id: number = -1;
  eth0IPAddress: FormControl;
  eth0SubnetMask: FormControl;
  eth0Gateway: FormControl;
  modemSimPin: FormControl;
  modemAPN: FormControl;
  modemUsername: FormControl;
  modemPassword: FormControl;
  navigationBtns: NavBtnType[];

  constructor(
    public commandService: CommandService,
    private route: ActivatedRoute,
    private chargeStationService: ChargeStationService,
    private snackBar: MatSnackBar,
    public translate: TranslateService,
    private router: Router
  ) {
    this.route.queryParams.subscribe((params) => {
      this.id = parseInt(params['id']);
    });
  }

  async ngOnInit(): Promise<void> {
    try {
      this.configuration = await this.chargeStationService.getChargeController(
        this.id
      );

      this.navigationBtns = [
        { text: this.configuration.serialNumber! },
        {
          text: this.translate.instant(
            'station-configuration.configure-station'
          ),
        },
      ];

      this.eth0IPAddress = new FormControl(this.configuration.eth0IPAddress);
      this.eth0SubnetMask = new FormControl(this.configuration.eth0SubnetMask);
      this.eth0Gateway = new FormControl(this.configuration.eth0Gateway);
      this.modemSimPin = new FormControl(this.configuration.modemSimPin);
      this.modemAPN = new FormControl(this.configuration.modemAPN);
      this.modemUsername = new FormControl(this.configuration.modemUsername);
      this.modemPassword = new FormControl(this.configuration.modemPassword);
    } catch {}
  }

  async saveConfiguration(): Promise<void> {
    this.configuration.eth0IPAddress = this.eth0IPAddress.value;
    this.configuration.eth0SubnetMask = this.eth0SubnetMask.value;
    this.configuration.eth0Gateway = this.eth0Gateway.value;
    this.configuration.modemSimPin = this.modemSimPin.value;
    this.configuration.modemAPN = this.modemAPN.value;
    this.configuration.modemUsername = this.modemUsername.value;
    this.configuration.modemPassword = this.modemPassword.value;

    try {
      await this.commandService.postCommand(
        this.configuration.id,
        undefined,
        JSON.stringify(this.configuration),
        BMSWebApiClientModule.CommandType.ModemConfig
      );
      await this.commandService.postCommand(
        this.configuration.id,
        undefined,
        JSON.stringify(this.configuration),
        BMSWebApiClientModule.CommandType.NetworkConfig
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

  navBtnClicked(valueEmitted: NavBtnType) {
    if (
      valueEmitted.text === this.configuration.serialNumber &&
      this.id !== -1
    ) {
      this.router.navigate(['/portal/charge-station/details'], {
        queryParams: { id: this.id },
      });
    } else if (
      valueEmitted.text ===
      this.translate.instant('station-configuration.configure-station')
    ) {
      window.location.reload();
    }
  }
}
