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
  outgoingData: any = [];
  outgoing = [''];
  incoming = [''];
  getData() {
    let token =
      'eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJ1c2VyIjoibWFudWZhY3R1cmVyIiwiZXhwIjoxNjgxNzI0ODc4LCJyb2xlIjoibWFudWZhY3R1cmVyIiwiaWF0IjoxNjgxNzIxMjc4LCJuYmYiOjE2ODE3MjEyNzh9.x21w1a-S_ArMB4NlvAA_PNtnevcN-MYv77kSM33J2u04C2nNOMsDMvGR-hftFXL8CmopVba6ulfsQErby9fs1aIBFmj7_rIdxPlph4tv_Lf-EEPFRxtT5zv3woknoo7yUOjR_DNnbiM4EvTEYEy6F0xpUqQep380FL4vsRWosl-DsNqi_A_r-DwsFACRD9_My76QBsvLoiw2rW_obIKSAIzXUPgvRs6kXW7RD3fFNJdF2PQBHTFsZWbOclovzMIPtmZ9T6C5u4p4OFQ2J3GSA5sFB2i-Pk0eASXxLHLLk9vYvEs6BxEd2KeThWuKb3xKywacWvXJZLlrzQr_LWLHRA';
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token,
        'Content-Type': 'application/json',
      }),
    };

    this.http
      .get('http://192.168.0.189:5000/api/v1.0/web/firewall', httpOptions)
      .subscribe((data: any) => {
        this.incoming = data.incoming.split(',');
        this.outgoing = data.outgoing.split(',');
      });
  }

  readonlyPorts = ['80', '5000', '3000'];

  async ngOnInit(): Promise<void> {
    this.getData();

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
