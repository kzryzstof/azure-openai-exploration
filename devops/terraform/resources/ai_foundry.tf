
module "ai_services_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = local.service_name
  type                = "ais"
  instance            = var.environment_instance
  delimiter           = "-"
}

resource "azurerm_ai_services" "default" {
  name                = module.ai_services_naming_convention.name
  location            = var.environment_location
  resource_group_name = azurerm_resource_group.default.name
  sku_name            = "S0"
}

module "ai_foundry_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = local.service_name
  type                = "aif"
  instance            = var.environment_instance
  delimiter           = "-"
}

resource "azurerm_ai_foundry" "default" {
  name                = module.ai_foundry_naming_convention.name
  location            = azurerm_ai_services.default.location
  resource_group_name = azurerm_resource_group.default.name
  storage_account_id  = azurerm_storage_account.system.id
  key_vault_id        = azurerm_key_vault.default.id
}