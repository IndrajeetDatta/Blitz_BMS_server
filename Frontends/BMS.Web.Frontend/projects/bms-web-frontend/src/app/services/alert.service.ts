import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class AlertService {
  constructor(private translate: TranslateService) { }

  alert(message: string, snackBar: MatSnackBar) {
    snackBar.open(message, this.translate.instant('general.ok'), { duration: 5000 });
  }
  alertErrorGeneric(snackBar: MatSnackBar) {
    this.alert(this.translate.instant('errors.generic'), snackBar);
  }
  alertInvalidCredentials(snackBar: MatSnackBar) {
    this.alert(this.translate.instant('errors.invalid-credentials'), snackBar);
  }
}
