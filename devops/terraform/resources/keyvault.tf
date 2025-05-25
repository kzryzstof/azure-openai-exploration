module "keyvault_signing_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = replace(local.service_name, "-", "")
  type                = "kv"
  instance            = var.environment_instance
  delimiter           = "-"
}

resource "azurerm_key_vault" "default" {
  name                = module.keyvault_signing_naming_convention.short_name
  resource_group_name = azurerm_resource_group.default.name
  location            = var.environment_location

  sku_name            = "standard"
  tenant_id           = local.tenant_id
  
  purge_protection_enabled = true
  soft_delete_retention_days = 7
  
  enable_rbac_authorization = true

  tags = local.tags
}

resource "azurerm_key_vault_key" "data_protection_key" {
  key_vault_id = azurerm_key_vault.default.id
  name         = local.data_protection_key_name
  key_type     = "RSA"
  key_size     = 2048
  key_opts     = ["unwrapKey", "wrapKey"]
}
