@allowed([
  'dev'
  'prod'
])
param environmentType string
param bmsDatabaseAdministratorLogin string
param bmsDatabaseAdministratorLoginPassword string

var location = resourceGroup().location

module bmsDatabase 'modules/bms-database.bicep' = {
  name: 'bms-database-${environmentType}'
  params: {
    administratorLogin: bmsDatabaseAdministratorLogin
    administratorLoginPassword: bmsDatabaseAdministratorLoginPassword
    serverName: 'psqldb-bms-${environmentType}'
    location: 'northeurope' // west europe not supported by dev subscription
  }
}

module bmsDataApi 'modules/bms-api.bicep' = {
  name: 'bms-data-api-${environmentType}'
  params: {
    appInsightsName: 'ai-bms-data-api-${environmentType}'
    appServiceName: 'azapp-bms-data-api-${environmentType}'
    appServicePlanName: 'apppln-bms-data-api-${environmentType}'
    appServicePlanSku: 'F1'
    databaseConnectionStringName: 'BMSDatabaseConnectionString'
    databaseConnectionStringValue: bmsDatabase.outputs.databaseConnectionString
    location: location
  }
}

module bmsWebApi 'modules/bms-api.bicep' = {
  name: 'bms-web-api-${environmentType}'
  params: {
    appInsightsName: 'ai-bms-web-api-${environmentType}'
    appServiceName: 'azapp-bms-web-api-${environmentType}'
    appServicePlanName: 'apppln-bms-web-api-${environmentType}'
    appServicePlanSku: 'F1'
    databaseConnectionStringName: 'BMSDatabaseConnectionString'
    databaseConnectionStringValue: bmsDatabase.outputs.databaseConnectionString
    location: location
  }
}

module bmsWebFrontend 'modules/bms-web-frontend.bicep' = {
  name: 'bms-web-frontend-${environmentType}'
  params: {
    appServiceName: 'azapp-bms-web-frontend-${environmentType}'
    appServicePlanName: 'apppln-bms-web-frontend-${environmentType}'
    appServicePlanSku: 'F1'
    location: location
  }
}
