data "azurerm_container_app_environment" "shared" {
    resource_group_name        = var.container_app_environment_resource_group_name
    name                       = var.container_app_environment_name
    
    provider = azurerm.shared_resources
}