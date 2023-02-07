param location string
param administratorLogin string
param administratorLoginPassword string
param serverName string
param serverEdition string = 'Burstable'
param skuSizeGB int = 32
param dbInstanceType string = 'Standard_B1ms'

param haMode string = 'Disabled'
param availabilityZone string = ''

param virtualNetworkExternalId string = ''
param subnetName string = ''
param privateDnsZoneArmResourceId string = ''

resource database 'Microsoft.DBforPostgreSQL/flexibleServers@2021-06-01' = {
  name: serverName
  location: location
  sku: {
    name: dbInstanceType
    tier: serverEdition
  }
  properties: {
    version: '13'
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
    network: {
      delegatedSubnetResourceId: (empty(virtualNetworkExternalId) ? json('null') : json('${virtualNetworkExternalId}/subnets/${subnetName}'))
      privateDnsZoneArmResourceId: (empty(virtualNetworkExternalId) ? json('null') : privateDnsZoneArmResourceId)
    }
    highAvailability: {
      mode: haMode
    }
    storage: {
      storageSizeGB: skuSizeGB
    }
    backup: {
      backupRetentionDays: 7
      geoRedundantBackup: 'Disabled'
    }
    availabilityZone: availabilityZone
  }
}

resource allowAllWindowsAzureIps 'Microsoft.DBforPostgreSQL/flexibleServers/firewallRules@2022-01-20-preview' = {
  parent: database
  name: 'AllowAllWindowsAzureIps'
  properties: {
    endIpAddress: '0.0.0.0'
    startIpAddress: '0.0.0.0'
  }
}

output databaseConnectionString string = 'Server=${database.properties.fullyQualifiedDomainName};Database=bms;Port=5432;User Id=${administratorLogin};Password=${administratorLoginPassword};Ssl Mode=VerifyFull;'
