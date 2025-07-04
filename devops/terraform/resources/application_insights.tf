module "application_insights_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = local.service_name
  type                = "appi"
  instance            = var.environment_instance
  delimiter           = "-"
}

resource "azurerm_application_insights" "default" {
  name                = module.application_insights_naming_convention.name
  location            = var.environment_location
  application_type    = "web"
  resource_group_name = azurerm_resource_group.default.name
  
  sampling_percentage = 25
  daily_data_cap_in_gb = 0.5
  
  workspace_id        = azurerm_log_analytics_workspace.default.id

  tags = local.tags
}