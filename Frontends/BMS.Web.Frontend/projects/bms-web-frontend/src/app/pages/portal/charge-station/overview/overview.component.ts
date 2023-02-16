import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import {
  directionSort,
  sortArray,
} from 'projects/bms-web-frontend/src/app/sort-tables';
import { formatDate } from '@angular/common';
import { ddReleaseChargingMode } from '../charging-point-edit/drop-downs';
@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss'],
})
export class OverviewComponent implements OnInit {
  stationsPointData: BMSWebApiClientModule.ChargePoint[] = [];
  stationsPointDataMockup: BMSWebApiClientModule.ChargePoint[] = [];
  page = 1;
  pageSize = 50;
  collectionSize = 0;
  pageSizes = [50, 100, 150];
  filteredValue = '';
  chargePoints: BMSWebApiClientModule.ChargePoint[] = [];
  directionSorting: {
    [key in
      | keyof BMSWebApiClientModule.ChargePoint
      | 'heartbeat']?: directionSort;
  } = {
    serialNumber: '',
    location: '',
    releaseChargingMode: '',
    chargeControllerUid: '',
    heartbeat: 'asc',
  };

  constructor(
    private activatedRoute: ActivatedRoute,
    private chargeStationService: ChargeStationService
  ) {}

  async ngOnInit(): Promise<void> {
    try {
      this.stationsPointData =
        await this.chargeStationService.getAllChargingStationOverview();
      this.stationsPointData.forEach((station) => {
        const stationPosition = this.chargePoints.findIndex(
          (chargePoint) =>
            station.serialNumber === chargePoint.serialNumber &&
            station.chargeController?.serialNumber ===
              chargePoint.chargeController?.serialNumber
        );
        if (stationPosition === -1) {
          station.states = [];
          station.releaseChargingModes = [];
          station.names = [];
          station.states.push(
            station.state != null || station.state != undefined
              ? station.state
              : ''
          );
          station.releaseChargingModes.push(
            this.friendlyDisplayValue(station.releaseChargingMode)
          );
          station.names.push(
            station.name != null || station.name != undefined
              ? station.name
              : ''
          );
          this.chargePoints.push(station);
        } else {
          this.chargePoints[stationPosition].states?.push(
            station.state != null || station.state != undefined
              ? station.state
              : ''
          );
          this.chargePoints[stationPosition].releaseChargingModes?.push(
            this.friendlyDisplayValue(station.releaseChargingMode)
          );
          this.chargePoints[stationPosition].names?.push(
            station.name != null || station.name != undefined
              ? station.name
              : ''
          );
        }
      });
      this.stationsPointDataMockup = this.chargePoints;
      this.collectionSize = this.chargePoints.length;
      this.filteredValue = '';
      this.sortTable('heartbeat');
    } catch (error) {
      console.log('ERROR fetch chargePoints list', error);
    }
  }

  FilterTableData(inputValue: string) {
    this.refreshStationData(true);
  }

  refreshStationData(resetToPage1?: boolean) {
    if (this.filteredValue.length) {
      this.chargePoints = this.stationsPointDataMockup.filter((station) => {
        return station.chargeController?.serialNumber
            ?.toLocaleLowerCase()
            .includes(this.filteredValue) ||
          station.serialNumber
            ?.toLocaleLowerCase()
            .includes(this.filteredValue.toLocaleLowerCase()) ||
          station
            .chargeController!.client!.name?.toLocaleLowerCase()
            .includes(this.filteredValue.toLocaleLowerCase()) ||
          station.chargeController?.client?.location
            ?.toLocaleLowerCase()
            .includes(this.filteredValue.toLocaleLowerCase()) ||
          station
            .chargeController!.installer!.firstName?.toLocaleLowerCase()
            .includes(this.filteredValue.toLocaleLowerCase());

      });
    } else {
      this.chargePoints = this.stationsPointDataMockup;
    }

    this.collectionSize = this.chargePoints.length;
    if (resetToPage1) this.page = 1;

    this.chargePoints = this.chargePoints.slice(
      (this.page - 1) * this.pageSize,
      (this.page - 1) * this.pageSize + this.pageSize
    );
  }

  sortTable(
    columnSorting: keyof BMSWebApiClientModule.ChargePoint | 'heartbeat'
  ) {
    if (this.directionSorting[columnSorting] === '')
      this.directionSorting[columnSorting] = 'asc';
    else if (this.directionSorting[columnSorting] === 'asc')
      this.directionSorting[columnSorting] = 'desc';
    else this.directionSorting[columnSorting] = '';

    if (
      columnSorting === 'heartbeat' &&
      this.directionSorting[columnSorting] !== ''
    ) {
      this.stationsPointDataMockup.sort((a, b) => {
        const res =
          a.chargeController!.heartbeat!.getTime() >
          b.chargeController!.heartbeat!.getTime()
            ? 1
            : a.chargeController!.heartbeat!.getTime() <
              b.chargeController!.heartbeat!.getTime()
            ? -1
            : 0;
        return this.directionSorting[columnSorting] === 'asc' ? res : -res;
      });
    } else
      this.stationsPointDataMockup = sortArray(
        this.stationsPointDataMockup,
        this.directionSorting[columnSorting]!,
        columnSorting
      );
    this.refreshStationData();
  }

  friendlyDisplayValue(value?: string): string {
    try {
      return (
        ddReleaseChargingMode.find((x) => x.fetchValue == value)
          ?.displayedValue || '-'
      );
    } catch (ex) {
      return '-';
    }
  }

  getDate(date?: Date, shortFormat?: Boolean) {
    if (!date) return '-';
    return formatDate(date, 'YYYY-MM-dd HH:mm:ss', 'en-US');
  }

  getStatusIconClass(status: string) {
    if (status === 'A1') return 'bi bi-lightning-fill green-icon';
    else if (status === 'C2') return 'bi bi-lightning-fill blue-icon';
    else if (status === 'E') return 'bi bi-exclamation-triangle-fill red-icon';
    else if (status === 'B2') return 'bi bi-lightning-fill yellow-icon';

    return '';
  }

  getFirstLetterOfReleaseMode(name: string | undefined) {
    if (name != undefined) {
      let i = 0;
      while (name[i] < 'A' || name[i] > 'Z' || name[i] === 'B') {
        i++;
      }
      return name[i];
    }
    return '-';
  }

  getTitleForStatus(
    station: BMSWebApiClientModule.ChargePoint,
    position: number
  ) {
    if (
      station.names != undefined &&
      station.states != undefined &&
      station.names[position] != undefined &&
      station.states[position] != undefined
    ) {
      return (
        'Name: ' +
        station.names[position] +
        '\n' +
        'Status: ' +
        station.states[position]
      );
    }
    if (station.names != undefined && station.names[position] != undefined) {
      return 'Name: ' + station.names[position] + '\n' + 'Status: ' + '-';
    }
    if (station.states != undefined && station.states[position] != undefined) {
      return 'Name: ' + '-' + '\n' + 'Status: ' + station.states[position];
    }
    return 'Name: ' + '-' + '\n' + 'Status: ' + '-';
  }

  getTitleForReleaseMode(
    station: BMSWebApiClientModule.ChargePoint,
    position: number
  ) {
    if (
      station.names != undefined &&
      station.releaseChargingModes != undefined &&
      station.names[position] != undefined &&
      station.releaseChargingModes[position] != undefined
    ) {
      return (
        'Name: ' +
        station.names[position] +
        '\n' +
        'Release Mode: ' +
        station.releaseChargingModes[position]
      );
    }
    if (station.names != undefined && station.names[position] != undefined) {
      return 'Name: ' + station.names[position] + '\n' + 'Release Mode: ' + '-';
    }
    if (
      station.releaseChargingModes != undefined &&
      station.releaseChargingModes[position] != undefined
    ) {
      return (
        'Name: ' +
        '-' +
        '\n' +
        'Release Mode: ' +
        station.releaseChargingModes[position]
      );
    }
    return 'Name: ' + '-' + '\n' + 'Release Mode: ' + '-';
  }
}
