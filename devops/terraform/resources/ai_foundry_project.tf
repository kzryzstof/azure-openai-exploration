module "ai_foundry_project_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = local.service_name
  type                = "aifp"
  instance            = var.environment_instance
  delimiter           = "-"
}

resource "azurerm_ai_foundry_project" "default" {
  name               = module.ai_foundry_project_naming_convention.name
  location           = azurerm_ai_foundry.default.location
  ai_services_hub_id = azurerm_ai_foundry.default.id
}