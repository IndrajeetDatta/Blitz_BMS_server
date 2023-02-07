import { formatDate } from '@angular/common';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';

@Component({
  selector: 'app-ocpp-message-dialog',
  templateUrl: './ocpp-message-dialog.component.html',
  styleUrls: ['./ocpp-message-dialog.component.scss']
})
export class OcppMessageDialogComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<OcppMessageDialogComponent>, @Inject(MAT_DIALOG_DATA) data : BMSWebApiClientModule.OcppMessage) {
    this.message = data;
}

  message: BMSWebApiClientModule.OcppMessage;

  ngOnInit(): void {}

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return "-"
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

}
