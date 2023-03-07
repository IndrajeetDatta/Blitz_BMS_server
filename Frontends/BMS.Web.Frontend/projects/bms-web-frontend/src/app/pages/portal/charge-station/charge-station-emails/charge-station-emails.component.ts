import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import { Subject } from 'rxjs';


@Component({
  selector: 'app-charge-station-emails',
  templateUrl: './charge-station-emails.component.html',
  styleUrls: ['./charge-station-emails.component.scss']
})
export class ChargeStationEmailsComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private chargeStationService: ChargeStationService,
    public translate: TranslateService,
    private router: Router
  ) {
    this.route.queryParams
      .subscribe(params => {
        this.id = parseInt(params['id'])
      }
    );
   }


  dtOptions: DataTables.Settings = {};
  id: number = -1;
  navigationBtns: NavBtnType[];
  emailData: BMSWebApiClientModule.Email[] =[];
  dtTrigger: Subject<any> = new Subject<any>();

  chargeController: BMSWebApiClientModule.ChargeController;

  async ngOnInit(): Promise<void> {
    this.dtOptions = {
      order:[[0, 'desc']] // '0' is the transactionId column(1st column) and 'desc' is the sorting order
    }

    try {
      this.chargeController = await this.chargeStationService.getChargeController(this.id);
      this.emailData = await this.chargeStationService.getEmailsList(this.id);
      this.dtTrigger.next(void 0);
      this.navigationBtns = [{text: this.chargeController.serialNumber != undefined? this.chargeController.serialNumber : ''}, {text: 'Emails'}];
    } catch (error) {
      console.log("ERROR fetch emails on emails page")
    }
  }


  ngAfterViewInit(): void {
    this.dtTrigger.next(void 0);
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (valueEmitted.text === this.chargeController.serialNumber && this.id !== -1) {
        this.router.navigate(['/portal/charge-station/details'], {queryParams: {id: this.id}});
    } else if (valueEmitted.text === this.translate.instant("charge-station.details.emails")) {
      window.location.reload();
    }
  }
}
