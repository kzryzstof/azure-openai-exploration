data "azurerm_container_registry" "default" {
  name                = var.container_registry_name
  resource_group_name = var.container_registry_resource_group_name
  
  provider = azurerm.shared_resources
}