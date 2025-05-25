module "user_identity_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  instance            = var.environment_instance
  name                = local.service_name
  type                = "id"
}

resource "azurerm_user_assigned_identity" "default" {
  name                = module.user_identity_naming_convention.name
  location            = var.environment_location
  resource_group_name = azurerm_resource_group.default.name

  tags                = local.tags
}