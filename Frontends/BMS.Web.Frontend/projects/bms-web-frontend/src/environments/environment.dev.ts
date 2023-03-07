import auth0_config from "../../auth0_config.json";

export const environment = {
  appVersion: '1',
  USERDATA_KEY: 'udk',
  
  production: false,
  bmsWebApiBaseUrl: "https://azapp-bms-web-api-dev.azurewebsites.net/api",
  auth0: {
    domain: auth0_config.domain,
    clientId: auth0_config.clientId,
    redirectUri: window.location.origin,
    useRefreshTokens: true
  }
};