import { formatDate } from '@angular/common';
import { Component, Input, OnInit, EventEmitter, Output, Type } from '@angular/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { CommandService } from '../../../services/command.service';

export type NavBtnType = { text: string }

@Component({
  selector: 'app-header-navigation',
  templateUrl: './header-navigation.component.html',
  styleUrls: ['./header-navigation.component.scss']
})
export class HeaderNavigationComponent implements OnInit {

  constructor(private commandService: CommandService) { }

  @Input() chargeController: BMSWebApiClientModule.ChargeController;
  @Input() navigationBtns: NavBtnType[];
  @Output() navBtnClicked: EventEmitter<NavBtnType> = new EventEmitter<NavBtnType>();

  commands: BMSWebApiClientModule.CommandHistory[] = []
  updateCommandsInterval: any

  async ngOnInit(): Promise<void> {
    this.commands = await this.commandService.getPendingOrProcessedCommands(this.chargeController.serialNumber);
    this.updateCommandsInterval = setInterval(async() => {
      this.commands = await this.commandService.getPendingOrProcessedCommands(this.chargeController.serialNumber);
    }, 5000)
  }

  clickNavBtn(navBtnType: NavBtnType) {
    this.navBtnClicked.emit(navBtnType);
  }

  ngOnDestroy() {
    if (this.updateCommandsInterval)
      clearInterval(this.updateCommandsInterval);
  }

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return "-"
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }
}
