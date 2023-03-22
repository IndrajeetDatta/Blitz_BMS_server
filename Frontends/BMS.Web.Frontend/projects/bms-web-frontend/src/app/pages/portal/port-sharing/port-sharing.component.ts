import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-port-sharing',
  templateUrl: './port-sharing.component.html',
  styleUrls: ['./port-sharing.component.scss'],
})
export class PortSharingComponent implements OnInit {
  constructor() {}
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

  readonlyPorts = ['80', '5000', '3000'];
  incoming = [
    '22',
    '80',
    '502',
    '1603',
    '1883',
    '5000',
    '2106',
    '5555',
    '5353',
    '9502',
    '3000',
  ];
  outgoing = [''];

  ngOnInit(): void {}
}
