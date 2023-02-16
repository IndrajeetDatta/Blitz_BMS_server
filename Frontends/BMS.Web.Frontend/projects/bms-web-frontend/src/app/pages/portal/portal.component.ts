import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';
import { AuthenticationService } from '../../services/api/authentication.service';
import { ApplicationUserService } from '../../services/application-user.service';
import { StorageService } from '../../services/storage.service';
import { AccountSettingsDialogComponent } from './account-settings-dialog/account-settings-dialog.component';

@Component({
  selector: 'app-portal',
  templateUrl: './portal.component.html',
  styleUrls: ['./portal.component.scss'],
})
export class PortalComponent implements OnInit {
  masterUidControl: FormControl = new FormControl('');
  isAdmin: boolean = true;
  nickname?: string;
  imgUser?: string;

  constructor(
    private router: Router,
    private accountSettingsDialog: MatDialog,
    private snackBar: MatSnackBar,
    public translate: TranslateService,
    public authenticationService: AuthenticationService,
    public applicationUserService: ApplicationUserService,
    private storageService: StorageService,
    @Inject(DOCUMENT) private doc: Document
  ) {}

  async ngOnInit(): Promise<void> {
    this.isAdmin = await this.authenticationService.isAdmin();
    const user = await this.authenticationService.user();
    this.nickname = user.nickname;
    this.imgUser = user.picture;
    console.log(this.isAdmin);
  }

  async addChargeController() {
    try {
      if (!this.masterUidControl.value) throw 'Bad masterUid';

      const userUpdateRequest =
        new BMSWebApiClientModule.ApplicationUserUpdateRequest({
          newMasterUid: this.masterUidControl.value,
        });
      await this.applicationUserService.update(userUpdateRequest);
      this.snackBar.open(
        this.translate.instant('general.command-submitted'),
        this.translate.instant('general.ok'),
        { duration: 3000 }
      );

      setTimeout(() => {
        if (this.router.url.includes('overview')) window.location.reload();
      }, 2000);
    } catch (e) {
      this.snackBar.open(
        this.translate.instant('general.wrong'),
        this.translate.instant('general.ok'),
        { duration: 3000 }
      );
    }
  }

  logout() {
    this.authenticationService.logout();
  }

  openAccountSettings() {
    this.accountSettingsDialog.open(AccountSettingsDialogComponent);
  }
}
