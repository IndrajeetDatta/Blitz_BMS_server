import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NavBtnType } from '../../../compoenents/header-navigation/header-navigation/header-navigation.component';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { ActivatedRoute, Router } from '@angular/router';
import { ChargeStationService } from '../../../services/charge-station.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-edit-client-info',
  templateUrl: './edit-client-info.component.html',
  styleUrls: ['./edit-client-info.component.scss'],
})
export class EditClientInfoComponent implements OnInit {
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
      'eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJ1c2VyIjoibWFudWZhY3R1cmVyIiwiZXhwIjoxNjgxODMzMjc2LCJyb2xlIjoibWFudWZhY3R1cmVyIiwiaWF0IjoxNjgxODI5Njc2LCJuYmYiOjE2ODE4Mjk2NzZ9.JaI5tD_su8RdSQUBFnV-t83JB40LDxHqQ2_RHWPUrRuDU2x3UX9S14idloej1uXV2w6Xe267vP46DID-DH7yAP3wzr9WgVjPq-DuWZuQc5fb7IpcRfkluYWVHa0Mg5wqYIRx38raxbU2Olwmsahpxtthypi_zI39PrBoNzPblc3VVK1C-t7PTM8YHNmpgTZ4Mxek29EhXTlLN0u4QdJaCUAbCRJa_ppNUmV5kvXrY2GSmHfY-dkZqfQPwE_57g0urnHjNPpKi_zjChyAyKuPJukVieTj9ye37Em72E5zAuOaRkFw0sPqekIQshnu4poT78ocQ2gCEs34Dq_pz98BKw';
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
        {
          text: this.translate.instant(
            'charge-station.details.edit-client-info'
          ),
        },
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
