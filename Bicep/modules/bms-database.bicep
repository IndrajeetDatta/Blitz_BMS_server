@allowed([
  'dev'
  'prod'
])
param environmentType string
param location string
param administratorLogin string
@secure()
param administratorLoginPassword string

resource database 'Microsoft.DBforPostgreSQL/flexibleServers@2021-06-01' = {
  name: 'psqldb-bms-${environmentType}'
  location: location
  sku: {
    name: 'Standard_B1ms'
    tier: 'Burstable'
  }
  properties: {
    version: '13'
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
    network: {
      delegatedSubnetResourceId: json('null')
      privateDnsZoneArmResourceId: json('null')
    }
    highAvailability: {
      mode: 'Disabled'
    }
    storage: {
      storageSizeGB: 32
    }
    backup: {
      backupRetentionDays: 30
      geoRedundantBackup: 'Disabled'
    }
    availabilityZone: ''
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

output connectionString string = 'Server=${database.properties.fullyQualifiedDomainName};Database=bms;Port=5432;User Id=${administratorLogin};Password=${administratorLoginPassword};Ssl Mode=VerifyFull;'

