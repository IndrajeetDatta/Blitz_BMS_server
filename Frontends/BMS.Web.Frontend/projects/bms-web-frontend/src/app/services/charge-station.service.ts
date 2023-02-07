import { Injectable } from '@angular/core';
import { BMSWebApiClientModule } from "../../../../bms-web-api-client/src/lib/bms-web-api-client.module";
import { AuthenticationService } from './api/authentication.service';
import { StorageService } from './storage.service';
import { WebApiService } from './web-api.service';

@Injectable({
  providedIn: 'root'
})
export class ChargeStationService {

  userEmail: string
  externalId: string

  constructor(private webApiService : WebApiService, public storageService: StorageService, public authenticationService: AuthenticationService) {
    this.getUser();
  }

  async getUser() {
    let user = await this.authenticationService.user();
    this.userEmail = user.email != undefined ? user.email : '';
    this.externalId = user.sub != undefined ? user.sub : '';
  }

  async getChargeController(id: number) : Promise<BMSWebApiClientModule.ChargeController> {
    return this.webApiService.getClient().getChargeStationConfiguration(this.userEmail, this.externalId, id);
  }

  async getChargeControllerWithWhitelist(id: number) : Promise<BMSWebApiClientModule.ChargeController> {
    return this.webApiService.getClient().getChargeStationWhitelist(this.userEmail, this.externalId, id);
  }

  async getChargeControllerWithTransaction(id: number, getAllTransactions: boolean) : Promise<BMSWebApiClientModule.ChargeController> {
    return this.webApiService.getClient().getChargeStationTransaction(this.userEmail, getAllTransactions, this.externalId, id);
  }

  async getChargePoint(id: number) : Promise<BMSWebApiClientModule.ChargePoint> {
    return this.webApiService.getClient().getChargeStationChargePoint(this.userEmail, this.externalId, id);
  }

  async updateConfiguration(configuration: BMSWebApiClientModule.ChargeController, id: number) : Promise<BMSWebApiClientModule.ChargeController> {
    return this.webApiService.getClient().updateChargeStationConfigurationId(configuration, this.userEmail, this.externalId, id);
  }

  async updateChargePoint(chargePoint: BMSWebApiClientModule.ChargePoint, id: number) : Promise<BMSWebApiClientModule.ChargePoint> {
    return this.webApiService.getClient().updateChargeStationChargePointId(chargePoint, id);
  }

  async getAllChargingStationOverview() : Promise<BMSWebApiClientModule.ChargePoint[]> {
    return this.webApiService.getClient().getChargeStationsOverview(this.userEmail, this.externalId);
  }

  async getChargeStationOcpp(id: number) : Promise<BMSWebApiClientModule.Anonymous> {
    return this.webApiService.getClient().getChargeStationOcpp(this.userEmail, this.externalId, id);
  }

  async getChargeStationAllOcppMessages(chargeControllerId: number) : Promise<BMSWebApiClientModule.OcppMessage[]> {
    return this.webApiService.getClient().getOcppMessages(this.userEmail, this.externalId, chargeControllerId);
  }

  async updateOcppConfig(ocppConfig: BMSWebApiClientModule.OcppConfig, chargeControllerid: number) : Promise<BMSWebApiClientModule.OcppConfig> {
    return this.webApiService.getClient().updateChargeStationOcppConfig(ocppConfig, chargeControllerid);
  }

  async getCommandHistory(masterId: string, chargeControllerId: number) : Promise<BMSWebApiClientModule.Anonymous2> {
    return this.webApiService.getClient().getChargeStationCommandHistory(this.userEmail, this.externalId, masterId, chargeControllerId);
  }

  async getCommands(masterId: string, inPendingorProcessedCommands: boolean) : Promise<BMSWebApiClientModule.CommandHistory[]> {
    return this.webApiService.getClient().getCommands(inPendingorProcessedCommands, masterId);
  }

  async postCommand(command: BMSWebApiClientModule.Command) : Promise<number> {
    return this.webApiService.getClient().postCommand(command, this.userEmail, this.externalId);
  }

  async getEmailsList(chargeControllerId: number) : Promise<BMSWebApiClientModule.Email[]> {
    return this.webApiService.getClient().getChargeStationEmails(this.userEmail, this.externalId, chargeControllerId);
  }
  
  async getInstallersForChargeStation(chargeControllerId: number) : Promise<BMSWebApiClientModule.ApplicationUser[]> {
    return this.webApiService.getClient().getInstallersForChargeStation(this.userEmail, this.externalId, chargeControllerId);
  }
}
