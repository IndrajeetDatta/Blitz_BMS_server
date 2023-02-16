import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthenticationService } from './services/api/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'bms-web-frontend';

  constructor(
    private router: Router,
    public authenticationService: AuthenticationService
  ) {
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd) {
        this.authenticationService.isAuth.subscribe((isAuth) => {
          if (isAuth && (event.url.includes('auth/login') || event.url === '/'))
            this.router.navigate(['/portal/charge-station/overview']);
          else if (
            !isAuth &&
            !(event.url.includes('auth/login') || event.url === '/')
          )
            this.router.navigate(['auth/login']);
        });
      }
    });

    this.authenticationService.sendUserToApi();
  }

  ngOnInit(): void {}
}
