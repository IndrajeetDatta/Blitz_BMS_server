import { MatDialog } from '@angular/material/dialog';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApplicationUserService } from '../../../services/application-user.service';
import { BMSWebApiClientModule } from 'projects/bms-web-api-client/src/public-api';

@Component({
  selector: 'app-account-settings-dialog',
  templateUrl: './account-settings-dialog.component.html',
  styleUrls: ['./account-settings-dialog.component.scss']
})
export class AccountSettingsDialogComponent implements OnInit {

  error: {active: boolean, message: string} = {active: false, message: "-"};
  isEdited: boolean = false;
  firstNameControl = new FormControl('testfirstName');
  lastNameControl = new FormControl('testSecondName');
  currentPasswordControl = new FormControl('');
  newPasswordControl = new FormControl('');
  confirmPasswordControl = new FormControl('');

  constructor(
    public translate: TranslateService,
    private accountSettingsDialog: MatDialog,
    private snackBar: MatSnackBar,
    private applicationUserService: ApplicationUserService
  ) { }

  changeText = () => {
    if (!this.isEdited) this.isEdited = true;
    if (this.error.active) this.error.active = false;
  }

  ngOnInit(): void {
    this.firstNameControl.valueChanges.subscribe(this.changeText)
    this.lastNameControl.valueChanges.subscribe(this.changeText)
    this.currentPasswordControl.valueChanges.subscribe(this.changeText)
    this.newPasswordControl.valueChanges.subscribe(this.changeText)
    this.confirmPasswordControl.valueChanges.subscribe(this.changeText)
  }

  async saveChanges() {
    if (this.firstNameControl.value === "") {
      this.error = {active: true, message: this.translate.instant("account-settings.error.first-name")};
      return;
    }
    else if (this.lastNameControl.value === ""){
      this.error = {active: true, message: this.translate.instant("account-settings.error.last-name")};
      return;
    }
    else if (this.currentPasswordControl.value !== "" || this.newPasswordControl.value !== "" || this.confirmPasswordControl.value !== "") {
      if (!this.newPasswordControl.value || this.newPasswordControl.value.length < 8) {
        this.error = {active: true, message: this.translate.instant("account-settings.error.short-password")};
        return;
      }
      else if (!/\d/.test(this.newPasswordControl.value)) {
        this.error = {active: true, message: this.translate.instant("account-settings.error.digit-password")};
        return;
      }
      else if (!/[A-Z]/.test(this.newPasswordControl.value)) {
        this.error = {active: true, message: this.translate.instant("account-settings.error.upper-letter-password")};
        return;
      }
      else if (!/[`!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/.test(this.newPasswordControl.value)) {
        this.error = {active: true, message: this.translate.instant("account-settings.error.special-character-password")};
        return;
      }
      else if (this.newPasswordControl.value !== this.confirmPasswordControl.value) {
        this.error = {active: true, message: this.translate.instant("account-settings.error.not-equal-password")};
        return;
      }
    }

    //send to backend
    const request = new BMSWebApiClientModule.ApplicationUserUpdateRequest({
      id: 1,
      firstName: this.firstNameControl.value!,
      lastName: this.lastNameControl.value!,
      newPassword: this.newPasswordControl.value!,
      currentPassword: this.currentPasswordControl.value!
    });
    try {
      const updatedApplicationUser = await this.applicationUserService.update(request);
      //this.accountSettingsDialog.closeAll();
      if (updatedApplicationUser) {
        this.snackBar.open(this.translate.instant("account-settings.success.save"), this.translate.instant("general.ok"), {duration: 3000});
      }
    } catch (error) {
      console.log(error);
    }

  }

  toggleErrorLabel() {
    if (this.error.active) return { opacity: 1 }
    return {opacity: 0}
  }

}
