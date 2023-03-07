import { Target } from '@angular/compiler';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { AuthenticationService } from 'projects/bms-web-frontend/src/app/services/api/authentication.service';
import { ChargeStationService } from 'projects/bms-web-frontend/src/app/services/charge-station.service';
import { CommandService } from 'projects/bms-web-frontend/src/app/services/command.service';

@Component({
  selector: 'app-user-access-dialog',
  templateUrl: './user-access-dialog.component.html',
  styleUrls: ['./user-access-dialog.component.scss']
})
export class UserAccessDialogComponent implements OnInit {
  constructor(
    public commandService: CommandService,
    private chargeStationService: ChargeStationService,
    public authenticationService: AuthenticationService,
    private snackBar: MatSnackBar,
    public translate: TranslateService,
    private router: Router,
    private route: ActivatedRoute,
    @Inject(MAT_DIALOG_DATA) data : BMSWebApiClientModule.ApplicationUser[],
    private installersDialog: MatDialog) {
      this.route.queryParams
        .subscribe(params => {
          this.id = parseInt(params['id'])
        }
      );
      this.installersWithAccess = data;
  }

  installersWithAccess: BMSWebApiClientModule.ApplicationUser[];
  hadInstallerAccess: boolean[] = [];
  installersAccess: boolean[] = [];
  installers: BMSWebApiClientModule.ApplicationUser[];
  filteredInstallers: BMSWebApiClientModule.ApplicationUser[];
  removeInstallerAccessList: BMSWebApiClientModule.ApplicationUser[] = [];
  addInstallerAccessList: BMSWebApiClientModule.ApplicationUser[] = [];
  filteredValue = '';
  collectionSize: number;
  page = 1;
  pageSize = 10;
  id: number;

  async ngOnInit(): Promise<void> {
    this.installers = await this.chargeStationService.getAllInstallers();
    this.installers.forEach(installer => {
      if (this.installersWithAccess.filter(x => x.id === installer.id).length > 0) {
        this.hadInstallerAccess.push(true);
        this.installersAccess.push(true);
      }
      else {
        this.hadInstallerAccess.push(false);
        this.installersAccess.push(false);
      }
    });
    this.filteredInstallers = this.installers;

    this.collectionSize = this.filteredInstallers.length;
    this.filteredInstallers = this.filteredInstallers.slice(
      (this.page - 1) * this.pageSize,
      (this.page - 1) * this.pageSize + this.pageSize
    );
  }

  changeAccessForInstaller(event:any, installerPosition: number) {
    this.installersAccess[installerPosition] = event.checked;
  }

  filterData() {
    if (this.filteredValue != '') {
      this.filteredInstallers = this.installers.filter((installer) => {
        if (installer.firstName?.toLocaleLowerCase().includes(this.filteredValue) || installer.lastName?.toLocaleLowerCase().includes(this.filteredValue) || installer.email?.toLocaleLowerCase().includes(this.filteredValue)) {
          return true;
        }
        return false;
      })
    }
    else {
      this.filteredInstallers = this.installers;
    }

    this.collectionSize = this.filteredInstallers.length
    this.filteredInstallers = this.filteredInstallers.slice(
      (this.page - 1) * this.pageSize,
      (this.page - 1) * this.pageSize + this.pageSize
    );
  }

  async saveAccessModifications() {
    this.hadInstallerAccess.forEach((hadAccess, index) => {
      if (hadAccess != this.installersAccess[index]) {
        if (this.installersAccess[index]) {
          this.addInstallerAccessList.push(this.installers[index]);
        }
        else {
          this.removeInstallerAccessList.push(this.installers[index]);
        }
      }
    });
    await this.chargeStationService.addAndRemoveAccessForInstallers(this.id, this.removeInstallerAccessList, this.addInstallerAccessList);
    window.location.reload();
  }
}
