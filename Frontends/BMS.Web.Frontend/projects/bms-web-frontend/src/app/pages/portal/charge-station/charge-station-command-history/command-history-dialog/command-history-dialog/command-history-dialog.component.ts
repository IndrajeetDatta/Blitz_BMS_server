import { formatDate } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
@Component({
  selector: 'app-command-history-dialog',
  templateUrl: './command-history-dialog.component.html',
  styleUrls: ['./command-history-dialog.component.scss']
})
export class CommandHistoryDialogComponent implements OnInit {
  constructor(private dialogRef: MatDialogRef<CommandHistoryDialogComponent>, @Inject(MAT_DIALOG_DATA) data : BMSWebApiClientModule.CommandHistory) {
    this.command = data;
  }

  command: BMSWebApiClientModule.CommandHistory;

  ngOnInit(): void {}

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return "-"
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }
}
