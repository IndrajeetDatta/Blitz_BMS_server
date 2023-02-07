import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { AuthenticationService } from './services/api/authentication.service';
import { StorageService } from './services/storage.service';
import { WebApiService } from './services/web-api.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'bms-web-frontend';
  constructor(
    private router: Router, 
    private webApiService : WebApiService,
    public authenticationService: AuthenticationService,
    private storageService: StorageService
  ) {
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd) {
        this.authenticationService.isAuth.subscribe(isAuth => {
          if (isAuth && (event.url.includes('auth/login') || event.url === '/'))
            this.router.navigate(['/portal/charge-station/overview']);
          else if (!isAuth && !(event.url.includes('auth/login') || event.url === '/'))
            this.router.navigate(['auth/login']);
        });

      }
    })
    
    this.authenticationService.sendUserToApi();
  }

  ngOnInit(): void {}
}
