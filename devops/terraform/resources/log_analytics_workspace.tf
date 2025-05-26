module "log_analytics_namingConvention" {
  source      = "../modules/naming_convention"
  environment = var.environment_name
  name        = local.service_name
  type        = "log"
  instance    = var.environment_instance
  delimiter   = "-"
}

resource "azurerm_log_analytics_workspace" "default" {
  name                = module.log_analytics_namingConvention.name
  location            = var.environment_location
  resource_group_name = azurerm_resource_group.default.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
  daily_quota_gb      = 0.05

  tags = local.tags
}