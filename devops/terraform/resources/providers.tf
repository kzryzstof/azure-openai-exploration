terraform {
  backend "azurerm" {}

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
    }
    azuread = {
      source  = "hashicorp/azuread"
    }
    azapi = {
      source  = "Azure/azapi"
    }
  }
}

provider "azurerm" {
  subscription_id = var.subscription_id
  resource_provider_registrations = "all"

  features {
    key_vault {
      purge_soft_delete_on_destroy = true
    }
  }
}

provider "azurerm" {
  subscription_id = var.shared_resources_subscription_id
  resource_provider_registrations = "all"

  alias = "shared_resources"

  features {}
}