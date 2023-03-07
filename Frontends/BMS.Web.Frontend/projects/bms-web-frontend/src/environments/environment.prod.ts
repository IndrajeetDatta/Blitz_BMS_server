import auth0_config from "../../auth0_config.json";

export const environment = {
  production: true,
  appVersion: '1',
  USERDATA_KEY: 'udk',
  bmsWebApiBaseUrl: "https://azapp-bms-web-api-prod.azurewebsites.net/api",
  auth0: {
    domain: auth0_config.domain,
    clientId: auth0_config.clientId,
    redirectUri: window.location.origin,
    useRefreshTokens: true
  }
};