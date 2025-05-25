# ----------------------------------------------- 
# Gives the App access to the key vault.
# -----------------------------------------------
resource "azurerm_role_assignment" "keyvault_secrets_officer" {
  scope               = azurerm_key_vault.default.id
  role_definition_name = local.built_in_roles.keyvault_secrets_officer_role_name
  principal_id        = azurerm_user_assigned_identity.default.principal_id
}

resource "azurerm_role_assignment" "keyvault_certificate_officer" {
  scope               = azurerm_key_vault.default.id
  role_definition_name = local.built_in_roles.keyvault_certificate_officer_role_name
  principal_id        = azurerm_user_assigned_identity.default.principal_id
}

resource "azurerm_role_assignment" "keyvault_crypto_officer" {
  scope               = azurerm_key_vault.default.id
  role_definition_name = local.built_in_roles.keyvault_crypto_officer_role_name
  principal_id        = azurerm_user_assigned_identity.default.principal_id
}

# ----------------------------------------------------------  
# Gives the App access to the Data storage account 
# ----------------------------------------------------------
resource "azurerm_role_assignment" "app_data_storage_account_access" {
  scope               = azurerm_storage_account.data.id
  role_definition_name = local.built_in_roles.storage_table_data_contributor_role_name
  principal_id        = azurerm_user_assigned_identity.default.principal_id
}

resource "azurerm_role_assignment" "app_data_storage_account_blob_access" {
  scope               = azurerm_storage_account.data.id
  role_definition_name = local.built_in_roles.storage_blob_data_contributor_role_name
  principal_id        = azurerm_user_assigned_identity.default.principal_id
}

# Permission required by the health check which calls GetPropertiesAsync.
resource "azurerm_role_assignment" "app_storage_account_management" {
  scope               = azurerm_storage_account.data.id
  role_definition_name = local.built_in_roles.storage_account_contributor_role_name
  principal_id        = azurerm_user_assigned_identity.default.principal_id
}

# ----------------------------------------------------------  
# Gives the App access to the System storage account 
# ----------------------------------------------------------
resource "azurerm_role_assignment" "system_storage_account_blob_access" {
  scope               = azurerm_storage_account.system.id
  role_definition_name = local.built_in_roles.storage_blob_data_contributor_role_name
  principal_id        = azurerm_user_assigned_identity.default.principal_id
}

# -------------------------------------------------  
# Gives the App access to the Service Bus
# -------------------------------------------------
resource "azurerm_role_assignment" "service_bus_namespace_owner" {
  scope                 = azurerm_servicebus_namespace.default.id
  role_definition_name  = local.built_in_roles.service_bus_data_owner_role_name
  principal_id          = azurerm_user_assigned_identity.default.principal_id
}

# -------------------------------------------------  
# Gives the App access to the Azure Communication Services
# -------------------------------------------------
resource "azurerm_role_assignment" "communication_services_contributor" {
  scope               = data.azurerm_communication_service.default.id
  role_definition_name = local.built_in_roles.contributor
  principal_id        = azurerm_user_assigned_identity.default.principal_id
}