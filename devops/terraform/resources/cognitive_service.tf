module "cognitive_account_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = local.service_name
  type                = "cog
  "
  instance            = var.environment_instance
  delimiter           = "-"
}

resource "azurerm_cognitive_account" "default" {
  name                = module.cognitive_account_naming_convention.name
  location            = var.environment_location
  resource_group_name = azurerm_resource_group.default.name
  kind                = "OpenAI"
  
  sku_name = "S0"
  
  tags = local.tags
}