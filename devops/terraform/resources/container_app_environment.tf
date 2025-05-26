module "container_app_environment_namingConvention" {
    source      = "../modules/naming_convention"
    environment = var.environment.name
    name        = local.service_name
    type        = "cae"
    instance    = var.environment.instance
    delimiter   = "-"
}

resource "azurerm_container_app_environment" "default" {
    name                       = module.container_app_environment_namingConvention.name
    resource_group_name        = azurerm_resource_group.default.name
    location                   = var.environment.location
    log_analytics_workspace_id = azurerm_log_analytics_workspace.default.id

    tags = local.tags
}