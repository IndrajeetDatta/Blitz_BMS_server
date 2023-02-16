import { formatDate } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';

@Component({
  selector: 'app-ocpp-message-dialog',
  templateUrl: './ocpp-message-dialog.component.html',
  styleUrls: ['./ocpp-message-dialog.component.scss'],
})
export class OcppMessageDialogComponent implements OnInit {
  message: BMSWebApiClientModule.OcppMessage;

  constructor(
    private dialogRef: MatDialogRef<OcppMessageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: BMSWebApiClientModule.OcppMessage
  ) {
    this.message = data;
  }

  ngOnInit(): void {}

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return '-';
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }
}
