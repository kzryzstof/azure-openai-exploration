module "resource_group_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = local.service_name
  type                = "rg"
  instance            = var.environment_instance
  delimiter           = "-"
}

resource "azurerm_resource_group" "default" {
  name = module.resource_group_naming_convention.name
  location = var.environment_location

  tags = local.tags
}