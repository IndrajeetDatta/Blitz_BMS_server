import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import { CommandHistoryDialogComponent } from './command-history-dialog/command-history-dialog/command-history-dialog.component';
@Component({
  selector: 'app-charge-station-command-history',
  templateUrl: './charge-station-command-history.component.html',
  styleUrls: ['./charge-station-command-history.component.scss']
})
export class ChargeStationCommandHistoryComponent implements OnInit {
  constructor(
    private commandHistoryDialog: MatDialog,
    private chargeStationService: ChargeStationService,
    private router: Router,
    private route: ActivatedRoute) {
    this.route.queryParams
      .subscribe(params => {
        this.masterId = params['masterId']
        this.id = parseInt(params['id'])
      }
    );
   }

  id: number = -1
  masterId: string = "";
  dtOptions: DataTables.Settings = {};
  navigationBtns: NavBtnType[];

  commandHistoryData: BMSWebApiClientModule.CommandHistory[] = [];
  chargeController: BMSWebApiClientModule.ChargeController;

  async ngOnInit(): Promise<void> {
    this.dtOptions = {
      order:[[0, 'desc']] // '0' is the timestamp column(1st column) and 'desc' is the sorting order
    }

    this.navigationBtns = [{text: this.masterId}, {text: 'Commands History'}]

    try {
      const response: BMSWebApiClientModule.Anonymous2 = await this.chargeStationService.getCommandHistory(this.masterId, this.id);
      this.chargeController = response.chargeController!;
      this.commandHistoryData = response.commandsHistoryList!;

    } catch (error) {
      console.log("ERROR fetch chargeController on detials page")
    }
  }

  async exportCommandsHistory() {
    //define the heading for each row of the data
    let csv = 'Timestamp,Command Id,ChargePointUid,ChargeControllerUid,Status,Processed Date,Master Url, Method, Payload, Name, Port, Token Required, Error Message\n';
    //merge the data with CSV
    const that = this;
    const allCommandsHistory = await this.chargeStationService.getCommands(this.masterId, false)
    allCommandsHistory.sort((a, b) => (that.getDate(a.createdDate) > that.getDate(b.createdDate)) ? -1 : ((that.getDate(a.createdDate) < that.getDate(b.createdDate)) ? 1 : 0))
    allCommandsHistory.forEach(function(command) {
      let {createdDate, commandId, chargePointUid, chargeControllerUid, status, processedDate, masterUrl, method, payload, name, port, tokenRequired, errorMessage } = command
      if (errorMessage) {
        errorMessage = errorMessage.replace(/(\r\n|\n|\r|\s+|\t|&nbsp;)/gm,' ');
        errorMessage = errorMessage.replace(/"/g, '""');
        errorMessage = errorMessage.replace(/ +(?= )/g,'');
      }
      if (payload) {
        payload = payload.replace(/(\r\n|\n|\r|\s+|\t|&nbsp;)/gm,' ');
        payload = payload.replace(/"/g, '""');
        payload = payload.replace(/ +(?= )/g,'');
      }
      csv += `${that.getDate(createdDate)},${commandId},${chargePointUid},${chargeControllerUid},${status},${that.getDate(processedDate)},${masterUrl},${method},"${payload}",${name},"${port}","${tokenRequired}","${errorMessage}"`
      csv += "\n";
    });

    const hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';

    //provide the name for the CSV file to be downloaded
    hiddenElement.download = `ChargeController-${this.chargeController.serialNumber}_CommandsHistory.csv`;
    hiddenElement.click();
  }

  showCommand(commandHistory: BMSWebApiClientModule.CommandHistory) {
    this.commandHistoryDialog.open(CommandHistoryDialogComponent, {data: commandHistory});
  }

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return "-"
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (valueEmitted.text === this.chargeController.serialNumber && this.id !== -1) {
        this.router.navigate(['/portal/charge-station/details'], {queryParams: {id: this.id}});
    } else if (valueEmitted.text === 'Commands History') {
      window.location.reload();
    }
  }
}
