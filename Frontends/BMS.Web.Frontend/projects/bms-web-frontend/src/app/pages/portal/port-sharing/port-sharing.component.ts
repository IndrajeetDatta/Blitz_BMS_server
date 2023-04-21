import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NavBtnType } from '../../../compoenents/header-navigation/header-navigation/header-navigation.component';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { ActivatedRoute, Router } from '@angular/router';
import { ChargeStationService } from '../../../services/charge-station.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-port-sharing',
  templateUrl: './port-sharing.component.html',
  styleUrls: ['./port-sharing.component.scss'],
})
export class PortSharingComponent implements OnInit {
  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute,
    private chargeStationService: ChargeStationService,
    public translate: TranslateService
  ) {
    this.route.queryParams.subscribe((params) => {
      // this.masterId = params['masterId'];
      this.id = parseInt(params['id']);
    });
  }
  id: number = -1;

  chargeController: BMSWebApiClientModule.ChargeController;
  navigationBtns: NavBtnType[];

  ports = [
    { port: '22', description: 'SSH Access' },
    { port: '80', description: 'Charx HTTP Access' },
    { port: '81', description: 'Custom Website' },
    { port: '502', description: 'MODBUS Server' },
    { port: '1603', description: 'Loadmanagement' },
    { port: '1883', description: 'MQTT' },
    { port: '2106', description: 'OCPP Remote' },
    { port: '5000', description: 'Charx Web Server' },
    { port: '5353', description: 'mDNS' },
    { port: '5555', description: 'Jupicore' },
    { port: '9502', description: 'MODBUS Client Configuration' },
    { port: '3000', description: 'Blitz HTTP Access' },
  ];

  incomingData: { incoming: string; outgoing: string } = {
    incoming: '',
    outgoing: '',
  };
  outgoing = [''];
  incoming = [''];

  readonlyPorts = ['80', '5000', '3000'];

  async ngOnInit(): Promise<void> {
    this.incomingData = {
      incoming: '22,80,502,1603,1883,5000,2106,5555,5353,9502,3000',
      outgoing: '',
    };
    this.incoming = this.incomingData.incoming.split(',');
    this.outgoing = this.incomingData.outgoing.split(',');

    try {
      this.chargeController =
        await this.chargeStationService.getChargeControllerWithWhitelist(
          this.id
        );

      this.navigationBtns = [
        { text: this.chargeController.serialNumber! },
        { text: this.translate.instant('Port Sharing') },
      ];
    } catch (error) {
      console.log('ERROR fetch chargeController on detials page');
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
    } else if (valueEmitted.text === 'Commands History') {
      window.location.reload();
    }
  }
}
