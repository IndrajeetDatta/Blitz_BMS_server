import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AlertService } from '../../../services/alert.service';
import { StorageService } from '../../../services/storage.service';
import { AuthenticationService } from '../../../services/api/authentication.service';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup | any;
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
  passwordFormControl = new FormControl('', [Validators.required]);
  
  constructor(
    public translate: TranslateService,
    private router: Router,
    private storageService: StorageService,
    private snackBar: MatSnackBar,
    private alertService: AlertService,
    private authenticationService: AuthenticationService) {
  }

  ngOnInit(): void {
  }

  authenticate() {
    this.authenticationService.login()
  }
}
