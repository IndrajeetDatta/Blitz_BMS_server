import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';

@Component({
  selector: 'app-charge-station-emails',
  templateUrl: './charge-station-emails.component.html',
  styleUrls: ['./charge-station-emails.component.scss'],
})
export class ChargeStationEmailsComponent implements OnInit {
  chargeController: BMSWebApiClientModule.ChargeController;
  dtOptions: DataTables.Settings = {};
  id: number = -1;
  navigationBtns: NavBtnType[];
  emails: BMSWebApiClientModule.Email[] = [];

  constructor(
    private route: ActivatedRoute,
    private chargeStationService: ChargeStationService,
    public translate: TranslateService,
    private router: Router
  ) {
    this.route.queryParams.subscribe((params) => {
      this.id = parseInt(params['id']);
    });
  }

  async ngOnInit(): Promise<void> {
    this.dtOptions = {
      order: [[0, 'desc']], // '0' is the transactionId column(1st column) and 'desc' is the sorting order
    };

    try {
      this.chargeController =
        await this.chargeStationService.getChargeController(this.id);
      this.emails = await this.chargeStationService.getEmailsList(this.id);
      this.navigationBtns = [
        {
          text:
            this.chargeController.serialNumber != undefined
              ? this.chargeController.serialNumber
              : '',
        },
        { text: 'Emails' },
      ];
    } catch (error) {
      console.log('ERROR fetch emails on emails page');
    }
  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (
      valueEmitted.text === this.chargeController.serialNumber &&
      this.id !== -1
    ) {
      this.router.navigate(['/portal/charge-station/details'], {
        queryParams: { id: this.id },
      });
    } else if (
      valueEmitted.text ===
      this.translate.instant('charge-station.details.emails')
    ) {
      window.location.reload();
    }
  }
}
