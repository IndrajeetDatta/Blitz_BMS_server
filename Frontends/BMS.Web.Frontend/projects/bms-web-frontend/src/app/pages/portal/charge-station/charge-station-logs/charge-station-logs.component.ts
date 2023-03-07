import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import { CommandService } from 'projects/bms-web-frontend/src/app/services/command.service';

@Component({
  selector: 'app-charge-station-logs',
  templateUrl: './charge-station-logs.component.html',
  styleUrls: ['./charge-station-logs.component.scss']
})
export class ChargeStationLogsComponent implements OnInit {

  constructor(
    private chargeStationService: ChargeStationService,
    private commandService: CommandService,
    private router: Router,
    private snackBar: MatSnackBar,
    public translate: TranslateService,
    private route: ActivatedRoute) {
    this.route.queryParams
      .subscribe(params => {
        this.id = parseInt(params['id'])
      }
    );
   }

  id: number = -1;
  dtOptions: DataTables.Settings = {};
  navigationBtns: NavBtnType[];

  logFilesData: {name: string, createOn: string, downloadLink: string}[] = [];
  chargeController: BMSWebApiClientModule.ChargeController;

  async ngOnInit(): Promise<void> {
    this.dtOptions = {
      order:[[0, 'desc']] // '0' is the timestamp column(1st column) and 'desc' is the sorting order
    }

    try {
      const response = await this.chargeStationService.getLogFiles(this.id);
      this.chargeController = response.chargeController!;
      this.navigationBtns = [{text: this.chargeController.serialNumber || '-'}, {text: 'Logs'}]
      this.logFilesData = JSON.parse(response.json!);
    } catch (error) {
      console.log("ERROR fetch chargeController on log page")
    }
  }
  // FUNCTION FOR DOWNLOAD THE FILE FROM ANY SITE
  downloadFile(url: string) {
    fetch(url)
      .then((res) => res.blob())
      .then((file) => {
        let tempUrl = URL.createObjectURL(file)
        const aTag = document.createElement("a")
        aTag.href = tempUrl
        const splitURL = url.split("/")
        const name = splitURL[splitURL.length - 1]
        aTag.download = name
        document.body.appendChild(aTag)
        aTag.click()
        URL.revokeObjectURL(tempUrl)
        aTag.remove()
      })
  }

  async getLogFilesCommand(): Promise<void> {
    try {
      await this.commandService.postCommand(this.chargeController.id, undefined, undefined, BMSWebApiClientModule.CommandType.GetLogFiles)
      this.snackBar.open(this.translate.instant("general.command-submitted"), this.translate.instant("general.ok"), {duration: 3000});
    } catch (e) {
      this.snackBar.open(this.translate.instant("general.wrong"), this.translate.instant("general.ok"), {duration: 3000});
    }
  }

  getDate(date?: string, shortFormat?: Boolean) {
    if (!date) return "-"
    const dateDate = new Date(date)
    return formatDate(dateDate, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (valueEmitted.text === this.chargeController.serialNumber && this.id !== -1) {
        this.router.navigate(['/portal/charge-station/details'], {queryParams: {id: this.id}});
    } else if (valueEmitted.text === 'Logs') {
      window.location.reload();
    }
  }
}
