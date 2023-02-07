# Pre-requisites

- [Install Bicep tools](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/install)

# Docs

- [Quickstart: Create Bicep files with Visual Studio Code](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/quickstart-create-bicep-use-visual-studio-code)
- [Build your first Bicep template](https://docs.microsoft.com/en-us/learn/modules/build-first-bicep-template/)

# Reference

- [Azure / azure-quickstart-templates](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts)

# Deployment

## Azure CLI

``` ps
# Connect to Azure
az login

# Set the Subscription
az account set --subscription "{Subscription Name}"

# Set the Resource Group
az configure --defaults group={Resource Group Name}

# Deploy the template
az deployment group create --template-file main.bicep
```

## Azure PowerShell module

``` ps
# Connect to Azure
Connect-AzAccount

# Set the Subscription
$context = Get-AzSubscription -SubscriptionName '{Subscription Name}'
Set-AzContext $context

# Set the Resource Group
Set-AzDefault -ResourceGroupName {Resource Group Name}

# Deploy the template
New-AzResourceGroupDeployment -TemplateFile ./main.bicep
```
