// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
import auth0_config from '../../auth0_config.json';

export const environment = {
  appVersion: '1',
  USERDATA_KEY: 'udk',

  production: false,
  // bmsWebApiBaseUrl: 'https://localhost:7180/api',
  bmsWebApiBaseUrl: 'https://azapp-bms-web-api-dev.azurewebsites.net/api',
  auth0: {
    domain: auth0_config.domain,
    clientId: auth0_config.clientId,
    redirectUri: window.location.origin,
    useRefreshTokens: true,
  },
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
