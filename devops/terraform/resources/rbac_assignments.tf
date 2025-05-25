# ----------------------------------------------- 
# Gives the App access to the key vault.
# -----------------------------------------------
resource "azurerm_role_assignment" "keyvault_secrets_officer" {
  scope                = azurerm_key_vault.default.id
  role_definition_name = local.built_in_roles.keyvault_secrets_officer_role_name
  principal_id         = azurerm_user_assigned_identity.default.principal_id
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
# Gives the App access to the System storage account 
# ----------------------------------------------------------
resource "azurerm_role_assignment" "system_storage_account_blob_access" {
  scope               = azurerm_storage_account.system.id
  role_definition_name = local.built_in_roles.storage_blob_data_contributor_role_name
  principal_id        = azurerm_user_assigned_identity.default.principal_id
}