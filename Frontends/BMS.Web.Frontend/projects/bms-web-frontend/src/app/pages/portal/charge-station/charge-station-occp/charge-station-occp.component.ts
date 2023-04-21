import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import {
  directionSort,
  sortArray,
} from 'projects/bms-web-frontend/src/app/sort-tables';
import { TranslateService } from '@ngx-translate/core';
import { OcppMessageDialogComponent } from './ocpp-message-dialog/ocpp-message-dialog.component';
import { formatDate } from '@angular/common';
import { NavBtnType } from 'projects/bms-web-frontend/src/app/compoenents/header-navigation/header-navigation/header-navigation.component';
import { CommandService } from 'projects/bms-web-frontend/src/app/services/command.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-charge-station-occp',
  templateUrl: './charge-station-occp.component.html',
  styleUrls: ['./charge-station-occp.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class ChargeStationOccpComponent implements OnInit {
  commandType = BMSWebApiClientModule.CommandType;

  constructor(
    private http: HttpClient,
    public commandService: CommandService,
    private ocppMessageDialog: MatDialog,
    private route: ActivatedRoute,
    private chargeStationService: ChargeStationService,
    private router: Router,
    private snackBar: MatSnackBar,
    public translate: TranslateService
  ) {
    this.route.queryParams.subscribe((params) => {
      this.id = parseInt(params['id']);
    });
  }

  page = 1;
  pageSize = 50;
  collectionSize = 0;
  pageSizes = [50, 100, 150];
  directionSorting: {
    [key in keyof BMSWebApiClientModule.OcppStatus]?: directionSort;
  } = {
    id: '',
    status: '',
    occpStatusSentDate: '',
    occpStatus: '',
  };

  dtOptions: DataTables.Settings = {};

  id = -1;
  ocppStatusData: BMSWebApiClientModule.OcppStatus[] = [];
  ocppStatusDataMockup: BMSWebApiClientModule.OcppStatus[] = [];
  ocppMessageData: BMSWebApiClientModule.OcppMessage[] = [];
  ocppMessageDataMockup: BMSWebApiClientModule.OcppMessage[] = [];
  ocppConfigData: BMSWebApiClientModule.OcppConfig;
  chargeController: BMSWebApiClientModule.ChargeController;

  formOCPPVersion: FormControl;
  formNetworkInterface: FormControl;
  formBackendURL: FormControl;
  formServiceRFID: FormControl;
  formFreeModeRfid: FormControl;
  formModel: FormControl;
  formVendor: FormControl;
  formSerialNumber: FormControl;
  formChargeBoxId: FormControl;
  formChargeBoxSerialNumber: FormControl;
  formIccid: FormControl;
  formImsi: FormControl;
  formMeterType: FormControl;
  formMeterSerialNumber: FormControl;

  navigationBtns: NavBtnType[];

  async ngOnInit(): Promise<void> {
    //Fake data
    this.exampleData = {
      AllowOfflineTxForUnknownId: {
        agent_used: true,
        key: 'AllowOfflineTxForUnknownId',
        predefined: true,
        readonly: true,
        required: false,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      AllowTimeSyncDuringSession: {
        agent_used: true,
        key: 'AllowTimeSyncDuringSession',
        predefined: false,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: true,
      },
      AuthorizationCacheEnabled: {
        agent_used: true,
        key: 'AuthorizationCacheEnabled',
        predefined: true,
        readonly: true,
        required: false,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      AuthorizeRemoteTxRequests: {
        agent_used: true,
        key: 'AuthorizeRemoteTxRequests',
        predefined: true,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: true,
      },
      AvailabilityOnlyWhenTimeSynchronized: {
        agent_used: true,
        key: 'AvailabilityOnlyWhenTimeSynchronized',
        predefined: false,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: true,
      },
      BlinkRepeat: {
        agent_used: true,
        key: 'BlinkRepeat',
        predefined: true,
        readonly: false,
        required: false,
        type: 'integer',
        unit: 'times',
        value: 0,
      },
      ChargeProfileMaxStackLevel: {
        agent_used: true,
        key: 'ChargeProfileMaxStackLevel',
        predefined: true,
        readonly: true,
        required: true,
        type: 'integer',
        unit: '',
        value: 0,
      },
      ChargingScheduleAllowedChargingRateUnit: {
        agent_used: true,
        key: 'ChargingScheduleAllowedChargingRateUnit',
        predefined: true,
        readonly: true,
        required: true,
        type: 'CSL',
        unit: 'CSL',
        value: 'Current',
      },
      ChargingScheduleMaxPeriods: {
        agent_used: true,
        key: 'ChargingScheduleMaxPeriods',
        predefined: true,
        readonly: true,
        required: true,
        type: 'integer',
        unit: '',
        value: 96,
      },
      ClockAlignedDataInterval: {
        agent_used: true,
        key: 'ClockAlignedDataInterval',
        predefined: true,
        readonly: true,
        required: true,
        type: 'integer',
        unit: 's',
        value: 0,
      },
      ConnectionTimeOut: {
        agent_used: true,
        key: 'ConnectionTimeOut',
        predefined: true,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 30,
      },
      ConnectorPhaseRotation: {
        agent_used: true,
        key: 'ConnectorPhaseRotation',
        predefined: true,
        readonly: true,
        required: true,
        type: 'CSL',
        unit: 'CSL',
        value: ['0.RST'],
      },
      ConnectorPhaseRotationMaxLength: {
        agent_used: true,
        key: 'ConnectorPhaseRotationMaxLength',
        predefined: true,
        readonly: true,
        required: false,
        type: 'integer',
        unit: '',
        value: 1,
      },
      ConnectorSwitch3to1PhaseSupported: {
        agent_used: true,
        key: 'ConnectorSwitch3to1PhaseSupported',
        predefined: true,
        readonly: true,
        required: false,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      EVDiscardTimeOut: {
        agent_used: true,
        key: 'EVDiscardTimeOut',
        predefined: false,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 120,
      },
      EichrechtAdminList: {
        agent_used: true,
        key: 'EichrechtAdminList',
        predefined: false,
        readonly: false,
        required: true,
        type: 'CSL',
        unit: 'CSL',
        value: '',
      },
      ForceUpdate: {
        agent_used: true,
        key: 'ForceUpdate',
        predefined: false,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      GetConfigurationMaxKeys: {
        agent_used: true,
        key: 'GetConfigurationMaxKeys',
        predefined: true,
        readonly: true,
        required: true,
        type: 'integer',
        unit: '',
        value: 100,
      },
      GlobalMaxCurrent: {
        agent_used: true,
        key: 'GlobalMaxCurrent',
        predefined: false,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 'A',
        value: 16,
      },
      HeartbeatInterval: {
        agent_used: true,
        key: 'HeartbeatInterval',
        predefined: true,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 300,
      },
      LightIntensity: {
        agent_used: true,
        key: 'LightIntensity',
        predefined: true,
        readonly: false,
        required: false,
        type: 'integer',
        unit: '%',
        value: 100,
      },
      LocalAuthListEnabled: {
        agent_used: true,
        key: 'LocalAuthListEnabled',
        predefined: true,
        readonly: true,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      LocalAuthorizeOffline: {
        agent_used: true,
        key: 'LocalAuthorizeOffline',
        predefined: true,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      LocalPreAuthorize: {
        agent_used: true,
        key: 'LocalPreAuthorize',
        predefined: true,
        readonly: true,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      LogLevel: {
        agent_used: true,
        key: 'LogLevel',
        predefined: false,
        readonly: false,
        required: true,
        type: 'string',
        unit: '',
        value: 'DEBUG',
      },
      MaxChargingProfilesInstalled: {
        agent_used: true,
        key: 'MaxChargingProfilesInstalled',
        predefined: true,
        readonly: true,
        required: true,
        type: 'integer',
        unit: '',
        value: 1,
      },
      MaxCurrent: {
        agent_used: true,
        key: 'MaxCurrent',
        predefined: false,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 'A',
        value: 16,
      },
      MaxEnergyOnInvalidId: {
        agent_used: true,
        key: 'MaxEnergyOnInvalidId',
        predefined: true,
        readonly: true,
        required: false,
        type: 'integer',
        unit: 'Wh',
        value: false,
      },
      MaxTimeDiffWithoutSynchronize: {
        agent_used: true,
        key: 'MaxTimeDiffWithoutSynchronize',
        predefined: false,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 60,
      },
      MessageAtKeyTransfer: {
        agent_used: true,
        key: 'MessageAtKeyTransfer',
        predefined: false,
        readonly: false,
        required: true,
        type: 'string',
        unit: 'string',
        value: 'setMeterConfiguration',
      },
      MeterValueSampleInterval: {
        agent_used: true,
        key: 'MeterValueSampleInterval',
        predefined: true,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 900,
      },
      MeterValuesAlignedData: {
        agent_used: true,
        key: 'MeterValuesAlignedData',
        predefined: true,
        readonly: true,
        required: true,
        type: 'CSL',
        unit: 'CSL',
        value: '',
      },
      MeterValuesAlignedDataMaxLength: {
        agent_used: true,
        key: 'MeterValuesAlignedDataMaxLength',
        predefined: true,
        readonly: true,
        required: false,
        type: 'integer',
        unit: '',
        value: 10,
      },
      MeterValuesSampledData: {
        agent_used: true,
        key: 'MeterValuesSampledData',
        predefined: true,
        readonly: false,
        required: true,
        type: 'CSL',
        unit: 'CSL',
        value:
          'Voltage, Current.Import, Energy.Active.Import.Register, Power.Active.Import',
      },
      MeterValuesSampledDataMaxLength: {
        agent_used: true,
        key: 'MeterValuesSampledDataMaxLength',
        predefined: true,
        readonly: true,
        required: false,
        type: 'integer',
        unit: '',
        value: 10,
      },
      MinimumStatusDuration: {
        agent_used: true,
        key: 'MinimumStatusDuration',
        predefined: true,
        readonly: false,
        required: false,
        type: 'integer',
        unit: 's',
        value: 1,
      },
      NewBackendURL: {
        agent_used: true,
        key: 'NewBackendURL',
        predefined: false,
        readonly: false,
        required: true,
        type: 'string',
        unit: '',
        value: '',
      },
      NumberOfConnectors: {
        agent_used: true,
        key: 'NumberOfConnectors',
        predefined: true,
        readonly: true,
        required: true,
        type: 'integer',
        unit: '',
        value: 0,
      },
      PreUnavailabilityForUpdate: {
        agent_used: true,
        key: 'PreUnavailabilityForUpdate',
        predefined: false,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 30,
      },
      PresentingRFIDEndCharging: {
        agent_used: true,
        key: 'PresentingRFIDEndCharging',
        predefined: false,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: true,
      },
      RFIDByteOrder: {
        agent_used: true,
        key: 'RFIDByteOrder',
        predefined: false,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      RFIDCharacterOrder: {
        agent_used: true,
        key: 'RFIDCharacterOrder',
        predefined: false,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      ReserveConnectorZeroSupported: {
        agent_used: true,
        key: 'ReserveConnectorZeroSupported',
        predefined: true,
        readonly: true,
        required: false,
        type: 'boolean',
        unit: 'ON/OFF',
        value: false,
      },
      ResetRetries: {
        agent_used: true,
        key: 'ResetRetries',
        predefined: true,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 'times',
        value: 3,
      },
      SendLocalListMaxLength: {
        agent_used: true,
        key: 'SendLocalListMaxLength',
        predefined: true,
        readonly: true,
        required: true,
        type: 'integer',
        unit: '',
        value: 50000,
      },
      SignedDataFormat: {
        agent_used: true,
        key: 'SignedDataFormat',
        predefined: false,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 'format',
        value: 0,
      },
      StopTransactionOnEVSideDisconnect: {
        agent_used: true,
        key: 'StopTransactionOnEVSideDisconnect',
        predefined: true,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: true,
      },
      StopTransactionOnInvalidId: {
        agent_used: true,
        key: 'StopTransactionOnInvalidId',
        predefined: true,
        readonly: true,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: true,
      },
      StopTxnAlignedData: {
        agent_used: true,
        key: 'StopTxnAlignedData',
        predefined: true,
        readonly: true,
        required: true,
        type: 'CSL',
        unit: 'CSL',
        value: ' ',
      },
      StopTxnSampledData: {
        agent_used: true,
        key: 'StopTxnSampledData',
        predefined: true,
        readonly: true,
        required: true,
        type: 'CSL',
        unit: 'CSL',
        value: '',
      },
      SupportedFeatureProfiles: {
        agent_used: true,
        key: 'SupportedFeatureProfiles',
        predefined: true,
        readonly: true,
        required: true,
        type: 'CSL',
        unit: 'CSL',
        value:
          'Core, FirmwareManagement, LocalAuthListManagement, Reservation, SmartCharging, RemoteTrigger',
      },
      SupportedFeatureProfilesMaxLength: {
        agent_used: true,
        key: 'SupportedFeatureProfilesMaxLength',
        predefined: true,
        readonly: true,
        required: false,
        type: 'integer',
        unit: '',
        value: 6,
      },
      TimeInSyncAfterConnectionLoss: {
        agent_used: true,
        key: 'TimeInSyncAfterConnectionLoss',
        predefined: false,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 86400,
      },
      TimeoutSignedData: {
        agent_used: true,
        key: 'TimeoutSignedData',
        predefined: false,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 30,
      },
      TransactionMessageAttempts: {
        agent_used: true,
        key: 'TransactionMessageAttempts',
        predefined: true,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 'times',
        value: 3,
      },
      TransactionMessageRetryInterval: {
        agent_used: true,
        key: 'TransactionMessageRetryInterval',
        predefined: true,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 10,
      },
      UnlockConnectorOnEVSideDisconnect: {
        agent_used: true,
        key: 'UnlockConnectorOnEVSideDisconnect',
        predefined: true,
        readonly: false,
        required: true,
        type: 'boolean',
        unit: 'ON/OFF',
        value: true,
      },
      VendorAtKeyTransfer: {
        agent_used: true,
        key: 'VendorAtKeyTransfer',
        predefined: false,
        readonly: false,
        required: true,
        type: 'string',
        unit: 'string',
        value: 'generalConfiguration',
      },
      WebSocketPingInterval: {
        agent_used: true,
        key: 'WebSocketPingInterval',
        predefined: true,
        readonly: false,
        required: false,
        type: 'integer',
        unit: 's',
        value: 0,
      },
      WebSocketPingTimeout: {
        agent_used: true,
        key: 'WebSocketPingTimeout',
        predefined: false,
        readonly: false,
        required: true,
        type: 'integer',
        unit: 's',
        value: 30,
      },
    };

    this.dtOptions = {
      order: [[0, 'desc']], // '0' is the timestamp column(1st column) and 'desc' is the sorting order
    };

    try {
      const response: BMSWebApiClientModule.Anonymous =
        await this.chargeStationService.getChargeStationOcpp(this.id);
      this.ocppConfigData = response.ocppConfig!;
      this.ocppStatusData = response.ocppStatus!;
      this.ocppStatusDataMockup = response.ocppStatus!;
      this.ocppMessageData = response.ocppMessages!;
      this.ocppMessageDataMockup = response.ocppMessages!;
      this.chargeController = this.ocppConfigData.chargeController!;
      if (!this.chargeController) {
        this.router.navigate(['/portal/charge-station/details'], {
          queryParams: { id: this.id },
        });
        return;
      } else {
        this.navigationBtns = [
          { text: this.chargeController.serialNumber! },
          { text: 'OCCP' },
        ];
      }
      this.collectionSize = this.ocppStatusData.length;

      this.formOCPPVersion = new FormControl(
        this.ocppConfigData.ocppProtocolVersion
      );
      this.formNetworkInterface = new FormControl(
        this.ocppConfigData.networkInterface
      );
      this.formBackendURL = new FormControl(this.ocppConfigData.backendURL);
      this.formServiceRFID = new FormControl(this.ocppConfigData.serviceRFID);
      this.formFreeModeRfid = new FormControl(this.ocppConfigData.freeModeRFID);
      this.formModel = new FormControl(this.ocppConfigData.chargeStationModel);
      this.formVendor = new FormControl(
        this.ocppConfigData.chargeStationVendor
      );
      this.formSerialNumber = new FormControl(
        this.ocppConfigData.chargeStationSerialNumber
      );
      this.formChargeBoxId = new FormControl(this.ocppConfigData.chargeBoxID);
      this.formChargeBoxSerialNumber = new FormControl(
        this.ocppConfigData.chargeBoxSerialNumber
      );
      this.formIccid = new FormControl(this.ocppConfigData.iccid);
      this.formImsi = new FormControl(this.ocppConfigData.imsi);
      this.formMeterType = new FormControl(this.ocppConfigData.meterType);
      this.formMeterSerialNumber = new FormControl(
        this.ocppConfigData.meterSerialNumber
      );

      this.refreshStationData();
    } catch (error) {
      console.log('ERROR fetch chargeController on detials page');
    }
  }

  refreshStationData() {
    this.ocppStatusData = this.ocppStatusDataMockup.slice(
      (this.page - 1) * this.pageSize,
      (this.page - 1) * this.pageSize + this.pageSize
    );
  }

  sortTable(columnSorting: keyof BMSWebApiClientModule.OcppStatus) {
    if (this.directionSorting[columnSorting] === '')
      this.directionSorting[columnSorting] = 'asc';
    else if (this.directionSorting[columnSorting] === 'asc')
      this.directionSorting[columnSorting] = 'desc';
    else this.directionSorting[columnSorting] = '';

    this.ocppStatusDataMockup = sortArray(
      this.ocppStatusDataMockup,
      this.directionSorting[columnSorting]!,
      columnSorting
    );
    this.refreshStationData();
  }

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return '-';
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

  showMessage(message: BMSWebApiClientModule.OcppMessage) {
    this.ocppMessageDialog.open(OcppMessageDialogComponent, { data: message });
  }

  async saveConfiguration(): Promise<void> {
    try {
      this.ocppConfigData.ocppProtocolVersion = this.formOCPPVersion.value;
      this.ocppConfigData.networkInterface = this.formNetworkInterface.value;
      this.ocppConfigData.backendURL = this.formBackendURL.value;
      this.ocppConfigData.serviceRFID = this.formServiceRFID.value;
      this.ocppConfigData.freeModeRFID = this.formFreeModeRfid.value;
      this.ocppConfigData.chargeStationModel = this.formModel.value;
      this.ocppConfigData.chargeStationVendor = this.formVendor.value;
      this.ocppConfigData.chargeStationSerialNumber =
        this.formSerialNumber.value;
      this.ocppConfigData.chargeBoxID = this.formChargeBoxId.value;
      this.ocppConfigData.chargeBoxSerialNumber =
        this.formChargeBoxSerialNumber.value;
      this.ocppConfigData.iccid = this.formIccid.value;
      this.ocppConfigData.imsi = this.formImsi.value;
      this.ocppConfigData.meterType = this.formMeterType.value;
      this.ocppConfigData.meterSerialNumber = this.formMeterSerialNumber.value;

      await this.commandService.postCommand(
        this.chargeController.id,
        undefined,
        JSON.stringify(this.ocppConfigData),
        BMSWebApiClientModule.CommandType.OcppConfig
      );
      this.snackBar.open(
        this.translate.instant('general.command-submitted'),
        this.translate.instant('general.ok'),
        { duration: 3000 }
      );
    } catch (e) {
      this.snackBar.open(
        this.translate.instant('general.wrong'),
        this.translate.instant('general.ok'),
        { duration: 3000 }
      );
    }
  }

  async exportOcppMessages() {
    //define the heading for each row of the data
    let csv = 'Timestamp,Type,Action,Message Data\n';
    //merge the data with CSV
    const that = this;
    const allOcppMessage =
      await this.chargeStationService.getChargeStationAllOcppMessages(
        this.chargeController.id!
      );
    allOcppMessage.forEach(function (ocppMessage) {
      let { timestamp, action, type, messageData } = ocppMessage;

      messageData = messageData?.replace(/(\r\n|\n|\r|\s+|\t|&nbsp;)/gm, ' ');
      messageData = messageData?.replace(/"/g, '""');
      messageData = messageData?.replace(/ +(?= )/g, '');

      csv += `${that.getDate(timestamp)},${type},${action},"${messageData}"`;
      csv += '\n';
    });

    const hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';

    //provide the name for the CSV file to be downloaded
    hiddenElement.download = `ChargeController-${this.chargeController.serialNumber}_OCPPMessages.csv`;
    hiddenElement.click();
  }

  navBtnClicked(valueEmitted: NavBtnType) {
    if (
      valueEmitted.text === this.chargeController.serialNumber &&
      this.id !== -1
    ) {
      this.router.navigate(['/portal/charge-station/details'], {
        queryParams: { id: this.id },
      });
    } else if (valueEmitted.text === 'OCCP') {
      window.location.reload();
    }
  }

  getOCCP(id?: number) {
    this.router.navigate(['/portal/charge-station/ocpp-server-variable'], {
      queryParams: { id },
    });
  }

  exampleData: any = [];

  // exampleData = {
  //   AllowOfflineTxForUnknownId: {
  //     agent_used: true,
  //     key: 'AllowOfflineTxForUnknownId',
  //     predefined: true,
  //     readonly: true,
  //     required: false,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   AllowTimeSyncDuringSession: {
  //     agent_used: true,
  //     key: 'AllowTimeSyncDuringSession',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: true,
  //   },
  //   AuthorizationCacheEnabled: {
  //     agent_used: true,
  //     key: 'AuthorizationCacheEnabled',
  //     predefined: true,
  //     readonly: true,
  //     required: false,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   AuthorizeRemoteTxRequests: {
  //     agent_used: true,
  //     key: 'AuthorizeRemoteTxRequests',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: true,
  //   },
  //   AvailabilityOnlyWhenTimeSynchronized: {
  //     agent_used: true,
  //     key: 'AvailabilityOnlyWhenTimeSynchronized',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: true,
  //   },
  //   BlinkRepeat: {
  //     agent_used: true,
  //     key: 'BlinkRepeat',
  //     predefined: true,
  //     readonly: false,
  //     required: false,
  //     type: 'integer',
  //     unit: 'times',
  //     value: 0,
  //   },
  //   ChargeProfileMaxStackLevel: {
  //     agent_used: true,
  //     key: 'ChargeProfileMaxStackLevel',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'integer',
  //     unit: '',
  //     value: 0,
  //   },
  //   ChargingScheduleAllowedChargingRateUnit: {
  //     agent_used: true,
  //     key: 'ChargingScheduleAllowedChargingRateUnit',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'CSL',
  //     unit: 'CSL',
  //     value: 'Current',
  //   },
  //   ChargingScheduleMaxPeriods: {
  //     agent_used: true,
  //     key: 'ChargingScheduleMaxPeriods',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'integer',
  //     unit: '',
  //     value: 96,
  //   },
  //   ClockAlignedDataInterval: {
  //     agent_used: true,
  //     key: 'ClockAlignedDataInterval',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 0,
  //   },
  //   ConnectionTimeOut: {
  //     agent_used: true,
  //     key: 'ConnectionTimeOut',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 30,
  //   },
  //   ConnectorPhaseRotation: {
  //     agent_used: true,
  //     key: 'ConnectorPhaseRotation',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'CSL',
  //     unit: 'CSL',
  //     value: ['0.RST'],
  //   },
  //   ConnectorPhaseRotationMaxLength: {
  //     agent_used: true,
  //     key: 'ConnectorPhaseRotationMaxLength',
  //     predefined: true,
  //     readonly: true,
  //     required: false,
  //     type: 'integer',
  //     unit: '',
  //     value: 1,
  //   },
  //   ConnectorSwitch3to1PhaseSupported: {
  //     agent_used: true,
  //     key: 'ConnectorSwitch3to1PhaseSupported',
  //     predefined: true,
  //     readonly: true,
  //     required: false,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   EVDiscardTimeOut: {
  //     agent_used: true,
  //     key: 'EVDiscardTimeOut',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 120,
  //   },
  //   EichrechtAdminList: {
  //     agent_used: true,
  //     key: 'EichrechtAdminList',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'CSL',
  //     unit: 'CSL',
  //     value: '',
  //   },
  //   ForceUpdate: {
  //     agent_used: true,
  //     key: 'ForceUpdate',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   GetConfigurationMaxKeys: {
  //     agent_used: true,
  //     key: 'GetConfigurationMaxKeys',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'integer',
  //     unit: '',
  //     value: 100,
  //   },
  //   GlobalMaxCurrent: {
  //     agent_used: true,
  //     key: 'GlobalMaxCurrent',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 'A',
  //     value: 16,
  //   },
  //   HeartbeatInterval: {
  //     agent_used: true,
  //     key: 'HeartbeatInterval',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 300,
  //   },
  //   LightIntensity: {
  //     agent_used: true,
  //     key: 'LightIntensity',
  //     predefined: true,
  //     readonly: false,
  //     required: false,
  //     type: 'integer',
  //     unit: '%',
  //     value: 100,
  //   },
  //   LocalAuthListEnabled: {
  //     agent_used: true,
  //     key: 'LocalAuthListEnabled',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   LocalAuthorizeOffline: {
  //     agent_used: true,
  //     key: 'LocalAuthorizeOffline',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   LocalPreAuthorize: {
  //     agent_used: true,
  //     key: 'LocalPreAuthorize',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   LogLevel: {
  //     agent_used: true,
  //     key: 'LogLevel',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'string',
  //     unit: '',
  //     value: 'DEBUG',
  //   },
  //   MaxChargingProfilesInstalled: {
  //     agent_used: true,
  //     key: 'MaxChargingProfilesInstalled',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'integer',
  //     unit: '',
  //     value: 1,
  //   },
  //   MaxCurrent: {
  //     agent_used: true,
  //     key: 'MaxCurrent',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 'A',
  //     value: 16,
  //   },
  //   MaxEnergyOnInvalidId: {
  //     agent_used: true,
  //     key: 'MaxEnergyOnInvalidId',
  //     predefined: true,
  //     readonly: true,
  //     required: false,
  //     type: 'integer',
  //     unit: 'Wh',
  //     value: false,
  //   },
  //   MaxTimeDiffWithoutSynchronize: {
  //     agent_used: true,
  //     key: 'MaxTimeDiffWithoutSynchronize',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 60,
  //   },
  //   MessageAtKeyTransfer: {
  //     agent_used: true,
  //     key: 'MessageAtKeyTransfer',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'string',
  //     unit: 'string',
  //     value: 'setMeterConfiguration',
  //   },
  //   MeterValueSampleInterval: {
  //     agent_used: true,
  //     key: 'MeterValueSampleInterval',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 900,
  //   },
  //   MeterValuesAlignedData: {
  //     agent_used: true,
  //     key: 'MeterValuesAlignedData',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'CSL',
  //     unit: 'CSL',
  //     value: '',
  //   },
  //   MeterValuesAlignedDataMaxLength: {
  //     agent_used: true,
  //     key: 'MeterValuesAlignedDataMaxLength',
  //     predefined: true,
  //     readonly: true,
  //     required: false,
  //     type: 'integer',
  //     unit: '',
  //     value: 10,
  //   },
  //   MeterValuesSampledData: {
  //     agent_used: true,
  //     key: 'MeterValuesSampledData',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'CSL',
  //     unit: 'CSL',
  //     value:
  //       'Voltage, Current.Import, Energy.Active.Import.Register, Power.Active.Import',
  //   },
  //   MeterValuesSampledDataMaxLength: {
  //     agent_used: true,
  //     key: 'MeterValuesSampledDataMaxLength',
  //     predefined: true,
  //     readonly: true,
  //     required: false,
  //     type: 'integer',
  //     unit: '',
  //     value: 10,
  //   },
  //   MinimumStatusDuration: {
  //     agent_used: true,
  //     key: 'MinimumStatusDuration',
  //     predefined: true,
  //     readonly: false,
  //     required: false,
  //     type: 'integer',
  //     unit: 's',
  //     value: 1,
  //   },
  //   NewBackendURL: {
  //     agent_used: true,
  //     key: 'NewBackendURL',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'string',
  //     unit: '',
  //     value: '',
  //   },
  //   NumberOfConnectors: {
  //     agent_used: true,
  //     key: 'NumberOfConnectors',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'integer',
  //     unit: '',
  //     value: 0,
  //   },
  //   PreUnavailabilityForUpdate: {
  //     agent_used: true,
  //     key: 'PreUnavailabilityForUpdate',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 30,
  //   },
  //   PresentingRFIDEndCharging: {
  //     agent_used: true,
  //     key: 'PresentingRFIDEndCharging',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: true,
  //   },
  //   RFIDByteOrder: {
  //     agent_used: true,
  //     key: 'RFIDByteOrder',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   RFIDCharacterOrder: {
  //     agent_used: true,
  //     key: 'RFIDCharacterOrder',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   ReserveConnectorZeroSupported: {
  //     agent_used: true,
  //     key: 'ReserveConnectorZeroSupported',
  //     predefined: true,
  //     readonly: true,
  //     required: false,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: false,
  //   },
  //   ResetRetries: {
  //     agent_used: true,
  //     key: 'ResetRetries',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 'times',
  //     value: 3,
  //   },
  //   SendLocalListMaxLength: {
  //     agent_used: true,
  //     key: 'SendLocalListMaxLength',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'integer',
  //     unit: '',
  //     value: 50000,
  //   },
  //   SignedDataFormat: {
  //     agent_used: true,
  //     key: 'SignedDataFormat',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 'format',
  //     value: 0,
  //   },
  //   StopTransactionOnEVSideDisconnect: {
  //     agent_used: true,
  //     key: 'StopTransactionOnEVSideDisconnect',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: true,
  //   },
  //   StopTransactionOnInvalidId: {
  //     agent_used: true,
  //     key: 'StopTransactionOnInvalidId',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: true,
  //   },
  //   StopTxnAlignedData: {
  //     agent_used: true,
  //     key: 'StopTxnAlignedData',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'CSL',
  //     unit: 'CSL',
  //     value: ' ',
  //   },
  //   StopTxnSampledData: {
  //     agent_used: true,
  //     key: 'StopTxnSampledData',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'CSL',
  //     unit: 'CSL',
  //     value: '',
  //   },
  //   SupportedFeatureProfiles: {
  //     agent_used: true,
  //     key: 'SupportedFeatureProfiles',
  //     predefined: true,
  //     readonly: true,
  //     required: true,
  //     type: 'CSL',
  //     unit: 'CSL',
  //     value:
  //       'Core, FirmwareManagement, LocalAuthListManagement, Reservation, SmartCharging, RemoteTrigger',
  //   },
  //   SupportedFeatureProfilesMaxLength: {
  //     agent_used: true,
  //     key: 'SupportedFeatureProfilesMaxLength',
  //     predefined: true,
  //     readonly: true,
  //     required: false,
  //     type: 'integer',
  //     unit: '',
  //     value: 6,
  //   },
  //   TimeInSyncAfterConnectionLoss: {
  //     agent_used: true,
  //     key: 'TimeInSyncAfterConnectionLoss',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 86400,
  //   },
  //   TimeoutSignedData: {
  //     agent_used: true,
  //     key: 'TimeoutSignedData',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 30,
  //   },
  //   TransactionMessageAttempts: {
  //     agent_used: true,
  //     key: 'TransactionMessageAttempts',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 'times',
  //     value: 3,
  //   },
  //   TransactionMessageRetryInterval: {
  //     agent_used: true,
  //     key: 'TransactionMessageRetryInterval',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 10,
  //   },
  //   UnlockConnectorOnEVSideDisconnect: {
  //     agent_used: true,
  //     key: 'UnlockConnectorOnEVSideDisconnect',
  //     predefined: true,
  //     readonly: false,
  //     required: true,
  //     type: 'boolean',
  //     unit: 'ON/OFF',
  //     value: true,
  //   },
  //   VendorAtKeyTransfer: {
  //     agent_used: true,
  //     key: 'VendorAtKeyTransfer',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'string',
  //     unit: 'string',
  //     value: 'generalConfiguration',
  //   },
  //   WebSocketPingInterval: {
  //     agent_used: true,
  //     key: 'WebSocketPingInterval',
  //     predefined: true,
  //     readonly: false,
  //     required: false,
  //     type: 'integer',
  //     unit: 's',
  //     value: 0,
  //   },
  //   WebSocketPingTimeout: {
  //     agent_used: true,
  //     key: 'WebSocketPingTimeout',
  //     predefined: false,
  //     readonly: false,
  //     required: true,
  //     type: 'integer',
  //     unit: 's',
  //     value: 30,
  //   },
  // };
}
