@allowed([
  'dev'
  'prod'
])
param environmentType string
param location string
param bmsDatabaseConnectionString string
param bmsStorageConnectionString string

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: 'ai-bms-data-api-${environmentType}'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: 'apppln-bms-data-api-${environmentType}'
  location: location
  sku: {
    name: 'F1'
  }
}

resource appService 'Microsoft.Web/sites@2020-12-01' = {
  name: 'azapp-bms-data-api-${environmentType}'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: 'InstrumentationKey=${appInsights.properties.InstrumentationKey}'
        }
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: environmentType == 'dev' ? 'dev' : 'prod'
        }
        {
          name: 'BlobConnectionString'
          value: bmsStorageConnectionString
        }
        {
          name: 'BlobContainerPrefix'
          value: 'log-files-'
        }
        {
          name: 'deleteOCPPMessagesInHours'
          value: '60'
        }
        {
          name: 'expireNetworkConnectionInSeconds'
          value: '360'
        }
        {
          name: 'intervalToCheckNetworkConnectionInSeconds'
          value: '10'
        }
        {
          name: 'intervalToCheckOCPPMessagesInHours'
          value: '10'
        }
        {
          name: 'intervalToCheckPendingOrProcessingCommandsInMinutes'
          value: '10'
        }
        {
          name: 'logAllHeartbeats'
          value: 'false'
        }
        {
          name: 'messageError'
          value: 'true'
        }
        {
          name: 'timeoutPendingCommandsInMinutes'
          value: '15'
        }
        {
          name: 'timeoutProcessingCommandsInMinutes'
          value: '20'
        }
      ]
      connectionStrings: [
        {
          name: 'BMSDatabaseConnectionString'
          connectionString: bmsDatabaseConnectionString
          type: 'SQLServer'
        }
      ]
    }
  }
}
