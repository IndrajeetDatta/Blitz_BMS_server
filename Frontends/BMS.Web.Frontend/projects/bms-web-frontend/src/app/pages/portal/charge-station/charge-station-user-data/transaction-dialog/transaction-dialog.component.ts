import { formatDate } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';

@Component({
  selector: 'app-transaction-dialog',
  templateUrl: './transaction-dialog.component.html',
  styleUrls: ['./transaction-dialog.component.scss']
})
export class TransactionDialogComponent implements OnInit {
  constructor(private dialogRef: MatDialogRef<TransactionDialogComponent>, @Inject(MAT_DIALOG_DATA) data : BMSWebApiClientModule.Transaction) {
    this.transaction = data;
  }

  transaction: BMSWebApiClientModule.Transaction;
  startDate: Date;
  endDate: Date;
  durationDays: number;
  timeConnected: string;
  timeCharged: string

  ngOnInit(): void {
    if (this.transaction.connectedTimeSec != undefined) {
      console.log(this.transaction.connectedTimeSec)
      this.timeConnected = this.convertSecondsToHours(this.transaction.connectedTimeSec);
    }
    if (this.transaction.chargeTimeSec != undefined) {
      console.log(this.transaction.chargeTimeSec)
      this.timeCharged = this.convertSecondsToHours(this.transaction.chargeTimeSec);
    }
    if (this.transaction.startDay != undefined && this.transaction.startMonth != undefined && this.transaction.startYear != undefined) {
      const startDateAsString = this.transaction.startYear + '/' + this.transaction.startMonth + '/' + this.transaction.startDay;
      this.startDate = new Date(startDateAsString);
      this.endDate = new Date(startDateAsString);
      if (this.transaction.durationDays != undefined) {
        this.endDate.setDate(this.endDate.getDate() + Number(this.transaction.durationDays));
        this.durationDays = Number(this.transaction.durationDays)
      }
    }
  }

	convertSecondsToHours(seconds: number) {
		var h = Math.floor(seconds / 3600);
		var m = Math.floor(seconds % 3600 / 60);
		var s = Math.floor(seconds % 3600 % 60);
		var hDisplay = h > 9 ? h + ":" : "0" + h + ":";
		var mDisplay = m > 9 ? m + ":" : "0" + m + ":";
		var sDisplay = s > 9 ? s + "" : "0" + s;
    if (hDisplay + mDisplay + sDisplay === "00:00:00") {
      return '-';
    }
		return hDisplay + mDisplay + sDisplay;
	}

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return "-"
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

}
