param location string
param appServicePlanName string
param appServicePlanSku string
param appServiceName string

resource appServicePlan 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: appServicePlanSku
  }
}

resource appService 'Microsoft.Web/sites@2020-12-01' = {
  name: appServiceName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: { }
  }
}
