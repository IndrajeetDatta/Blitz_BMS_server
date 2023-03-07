@allowed([
  'dev'
  'prod'
])
param environmentType string
param location string = resourceGroup().location
param bmsDatabaseAdministratorLogin string
@secure() 
param bmsDatabaseAdministratorLoginPassword string

module bmsDatabase 'modules/bms-database.bicep' = {
  name: 'bms-database-${environmentType}'
  params: {
    administratorLogin: bmsDatabaseAdministratorLogin
    administratorLoginPassword: bmsDatabaseAdministratorLoginPassword
    environmentType: environmentType
    location: location
  }
}

module bmsStorage 'modules/bms-storage.bicep' = {
  name: 'bms-storage-${environmentType}'
  params: {
    environmentType: environmentType
    location: location
  }
}

module bmsDataApi 'modules/bms-data-api.bicep' = {
  name: 'bms-data-api-${environmentType}'
  params: {
    bmsDatabaseConnectionString: bmsDatabase.outputs.connectionString
    bmsStorageConnectionString: bmsStorage.outputs.connectionString
    environmentType: environmentType
    location: location
  }
}

module bmsWebApi 'modules/bms-web-api.bicep' = {
  name: 'bms-web-api-${environmentType}'
  params: {
    bmsDatabaseConnectionString: bmsDatabase.outputs.connectionString
    bmsStorageConnectionString: bmsStorage.outputs.connectionString
    environmentType: environmentType
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
