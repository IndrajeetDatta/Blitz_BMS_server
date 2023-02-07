import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import { TransactionDialogComponent } from './transaction-dialog/transaction-dialog.component';

@Component({
  selector: 'app-charge-station-user-data',
  templateUrl: './charge-station-user-data.component.html',
  styleUrls: ['./charge-station-user-data.component.scss']
})
export class ChargeStationUserDataComponent implements OnInit {

  constructor(
    private transactionDialog: MatDialog,
    private route: ActivatedRoute,
    private chargeStationService: ChargeStationService,
    public translate: TranslateService,
    private router: Router
  ) {
    this.route.queryParams
      .subscribe(params => {
        this.id = parseInt(params['id'])
        this.masterId = params['masterId']
      }
    );
   }

  chargeController: BMSWebApiClientModule.ChargeController;
  userData: BMSWebApiClientModule.UserData;
  transactions: BMSWebApiClientModule.Transaction[] = [];
  dtOptions: DataTables.Settings = {};
  id: number = -1;
  navigationBtns: NavBtnType[];
  timeConnectedList: string[];
  timeChargedList: string[];
  masterId: string = "";
  startDate: Date[] = [];
  endDate: Date[] = [];
  durationDays: number;

  async ngOnInit(): Promise<void> {
    this.dtOptions = {
      order:[[0, 'desc']] // '0' is the transactionId column(1st column) and 'desc' is the sorting order
    }

    try {
      var timeConnected = '';
      var timeCharged = '';
      this.chargeController = await this.chargeStationService.getChargeControllerWithTransaction(this.id, false);

      this.userData = this.chargeController.userData || new BMSWebApiClientModule.UserData();
      this.chargeController.transactions?.forEach(transaction => {
        if (transaction.connectedTimeSec != undefined && transaction.connectedTimeSec != null) {
          timeConnected += this.convertSecondsToHours(transaction.connectedTimeSec) + " ";
        }
        else {
          timeConnected += "-" + " ";
        }
        if (transaction.chargeTimeSec != undefined && transaction.chargeTimeSec != null) {
          timeCharged += this.convertSecondsToHours(transaction.chargeTimeSec) + " ";
        }
        else {
          timeCharged += "-" + " ";
        }
        if (transaction.startDay != undefined && transaction.startMonth != undefined && transaction.startYear != undefined) {
          const startDateAsString = transaction.startYear + '/' + transaction.startMonth + '/' + transaction.startDay;
          const currentStartDate = new Date(startDateAsString);
          const currentEndDate = new Date(startDateAsString);
          this.startDate.push(currentStartDate);
          if (transaction.durationDays != undefined) {
            currentEndDate.setDate(currentEndDate.getDate() + Number(transaction.durationDays));
            this.durationDays = Number(transaction.durationDays)
            this.endDate.push(new Date(currentEndDate));
          }
          else {
            this.endDate.push(new Date(currentStartDate));
          }
        }
        else {
          this.startDate.push(new Date(0,0,0,0,0,0,0));
          this.endDate.push(new Date(0,0,0,0,0,0,0));
        }
        this.transactions.push(transaction);
      });
      this.timeConnectedList = timeConnected.split(" ");
      this.timeChargedList = timeCharged.split(" ");
      
      this.navigationBtns = [{text: this.chargeController.serialNumber!}, {text: this.translate.instant("charge-station.details.user-data")}]

    } catch (error) {
      console.log("ERROR fetch chargeController on user data page")
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

  navBtnClicked(valueEmitted: NavBtnType) {
    if (valueEmitted.text === this.chargeController.serialNumber && this.id !== -1) {
        this.router.navigate(['/portal/charge-station/details'], {queryParams: {id: this.id}});
    } else if (valueEmitted.text === this.translate.instant("charge-station.details.user-data")) {
      window.location.reload();
    }
  }

  showTransaction(transaction: BMSWebApiClientModule.Transaction) {
    this.transactionDialog.open(TransactionDialogComponent, {data: transaction});
  }

  async downloadTransactionsJson() {

    let jsonData = this.userData.jsonData != undefined ? this.userData.jsonData.toString() : "{}";
    const hiddenElement = document.createElement('a');

    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(jsonData);
    hiddenElement.target = '_blank';
    hiddenElement.download = `ChargeController-${this.chargeController.serialNumber}_Transactions_JSON.txt`;
    hiddenElement.click();
  }

  async exportTransactions() {
    //define the heading for each row of the data
    let csv = 'Id, Charge Point Id, Charge Point Name, Rfid Tag, Rfid Name, Start Day, Start Month, Start Year, Start Day Of Week, StartTime,  End Time, Duration Days, Connected Time Sec, Charge Time Sec, Average Power, Charged Energy, Charged Distance\n';
    //merge the data with CSV
    const that = this;
    const chargeController = await this.chargeStationService.getChargeControllerWithTransaction(this.id, true);
    const transactions = chargeController.transactions || []
    transactions.forEach(function(transactionToCSV) {
      let {transactionId, chargePointId, chargePointName, rfidTag, rfidName, startDay, startMonth, startYear, startDayOfWeek, startTime, endTime, durationDays, connectedTimeSec, chargeTimeSec, averagePower, chargedEnergy, chargedDistance } = transactionToCSV
      csv += `${transactionId},${chargePointId},${chargePointName},${rfidTag},${rfidName},${startDay},${startMonth},${startYear},"${startDayOfWeek}",${startTime},"${endTime}","${durationDays}",${connectedTimeSec},"${chargeTimeSec}","${averagePower}",${chargedEnergy},"${chargedDistance}"`
      csv += "\n";
    });

    const hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';

    //provide the name for the CSV file to be downloaded
    hiddenElement.download = `ChargeController-${this.chargeController.serialNumber}_Transactions.csv`;
    hiddenElement.click();
  }


  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return "-"
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

}
