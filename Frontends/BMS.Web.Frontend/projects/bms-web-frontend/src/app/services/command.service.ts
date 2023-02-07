import { Injectable } from '@angular/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { ChargeStationService } from './charge-station.service';

@Injectable({
  providedIn: 'root'
})
export class CommandService {
  private commandType = BMSWebApiClientModule.CommandType;

  constructor(private chargeStationService: ChargeStationService,) { }

  private readonly RestartAppList: BMSWebApiClientModule.CommandType[] = [
    this.commandType.RestartApp,
    this.commandType.RestartMB,
    this.commandType.RestartLM,
    this.commandType.RestartOCPP,
    this.commandType.RestartCA,
    this.commandType.RestartWEB,
    this.commandType.RestartJC,
    this.commandType.RestartMS,
    this.commandType.RestartSM
  ];
  private readonly RestartAppListToString : string[] = this.RestartAppList.map(commandType => commandType.toString())
  
  disableCommandButtons: boolean = false
  reasonOfDisableCommandButtons: string = ''
  disableTypeCommandButtonsList: {commandType?: string, specificIdentify?: any}[] = []

  private addDisableTypeCommandButton(command: BMSWebApiClientModule.Command | BMSWebApiClientModule.CommandHistory, additionalValue?: any) {
    let commandType, specificIdentify
    if (command instanceof BMSWebApiClientModule.Command) {
      commandType = command.type ? command.type.toString() : undefined;
      specificIdentify =  command.type ?
                                  [this.commandType.EnableDisableChargePoint, this.commandType.SaveChargingPoint].includes(command.type) ? 
                                    additionalValue : 
                                    [this.commandType.EditRFID, this.commandType.DeleteRFID].includes(command.type) ? 
                                      additionalValue : undefined :
                                undefined;
    } else {
      commandType = command.commandType;
      specificIdentify =  command.commandType ?
                                  [this.commandType.EnableDisableChargePoint.toString() , this.commandType.SaveChargingPoint.toString()].includes(command.commandType) ? 
                                    command.chargePointUid : 
                                    [this.commandType.EditRFID.toString(), this.commandType.DeleteRFID.toString()].includes(command.commandType) ? 
                                      command.rfidSerialNumber : undefined :
                                undefined;
    }
    this.disableTypeCommandButtonsList.push({commandType, specificIdentify});
  }
  private setDisableCommandButtons(disableCommandButtons: boolean, reasonOfDisableCommandButtons: string = "") {
    this.disableCommandButtons = disableCommandButtons
    this.reasonOfDisableCommandButtons = reasonOfDisableCommandButtons;
  }

  async postCommand(chargeControllerId?: number, chargePointId?: number, payload?: string, type?: BMSWebApiClientModule.CommandType, additionalValue?: any) {
    const command: BMSWebApiClientModule.Command = new BMSWebApiClientModule.Command({ type, payload, chargeControllerId, chargePointId, additionalValue})

    try {
      const noOfPendingCommands = await this.chargeStationService.postCommand(command);

      if (noOfPendingCommands >= 5)
        this.setDisableCommandButtons(true, "There are more than 5 commands in pending for this charge controller.")
      if (command.type && this.RestartAppList.includes(command.type))
        this.setDisableCommandButtons(true, "There are REBOOT commands in pending for this charge controller.")
      
      this.addDisableTypeCommandButton(command, additionalValue);
    } catch(e) {
      const response = e as Response
      if (response.status === 413) {
        this.setDisableCommandButtons(true, "There are more than 5 commands in pending for this charge controller.")
      } else if (response.status === 412) {
        this.setDisableCommandButtons(true, "There are REBOOT commands in pending for this charge controller.");
      } else if (response.status === 410) {
        this.addDisableTypeCommandButton(command, additionalValue);
      }
      throw e;
    }
  }

  async getPendingOrProcessedCommands(masterId?: string) : Promise<BMSWebApiClientModule.CommandHistory[]> {
    try {
      if (!masterId) throw "No master id"

      const commands = await this.chargeStationService.getCommands(masterId, true);

      if (commands.length >= 5)
        this.setDisableCommandButtons(true, "There are more than 5 commands in pending / processing for this charge controller.");
      else if (commands.find(command => command.commandType && this.RestartAppListToString.includes(command.commandType))) {
        this.setDisableCommandButtons(true, "There are REBOOT commands in pending / processing for this charge controller.");
      }
      else
        this.setDisableCommandButtons(false);
      
      this.disableTypeCommandButtonsList = []
      commands.forEach(command => { this.addDisableTypeCommandButton(command); })

      return commands.sort((a, b) =>  a.createdDate ? b.createdDate ? a.createdDate.getTime() - b.createdDate.getTime() : -1 : 1)
    } catch (ex) {
      //maybe here you should set to true with message "The server is down/ Something wrong happend" (the periodic call from header helps you to update when server is up again)
      this.setDisableCommandButtons(false);
    }

    return []
  }

  isDisabledCommand(commandType?: string, specificIdentify? : any) {
    if (this.disableTypeCommandButtonsList.find(x => x.commandType === commandType && x.specificIdentify === specificIdentify))
      return true;
    return false
  }

  tooltipMessage(commandType?: string, specificIdentify? : any) {
    return this.disableCommandButtons ? 
            this.reasonOfDisableCommandButtons : 
            commandType && this.isDisabledCommand(commandType, specificIdentify) ? "This type of command already exists in pending / processing commands." : '';
  }

}
