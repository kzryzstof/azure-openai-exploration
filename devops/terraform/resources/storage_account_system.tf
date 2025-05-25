module "system_storage_account_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = "s${replace(local.service_name, "-", "")}"
  type                = "st"
  instance            = var.environment_instance
  delimiter           = ""
}

resource "azurerm_storage_account" "system" {
  account_replication_type  = "LRS"
  account_kind              = "StorageV2"
  account_tier              = "Standard"
  access_tier               = "Hot"
  location                  = var.environment_location
  name                      = module.system_storage_account_naming_convention.short_name
  resource_group_name       = azurerm_resource_group.default.name
  https_traffic_only_enabled = true
  
  min_tls_version = "TLS1_2"
  allow_nested_items_to_be_public = false
  
  blob_properties {
    delete_retention_policy {
      days = 7
    }
  }
  
  tags = local.tags
}

resource "azurerm_storage_container" "data" {
  name                  = "data"
  storage_account_id    = azurerm_storage_account.system.id
  
  container_access_type = "private"
}

resource "azurerm_storage_blob" "keys" {
  name                  = "keys"
  storage_account_name  = azurerm_storage_account.system.name
  storage_container_name = azurerm_storage_container.data.name
  type = "Block"

  lifecycle {
    ignore_changes = [
      content_md5,
      content_type
    ]
  }
}