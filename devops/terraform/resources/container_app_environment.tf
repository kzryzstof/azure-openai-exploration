data "azurerm_container_app_environment" "shared" {
    resource_group_name        = local.container_app_environment.resource_group_name
    name                       = local.container_app_environment.name
    
    provider = azurerm.shared_resources
}