locals {
  service_name = "azure-open-ai"
  
  # Azure Built-in Roles
  built_in_roles = {
    keyvault_secrets_officer_role_name       = "Key Vault Secrets Officer"
    keyvault_crypto_officer_role_name        = "Key Vault Crypto Officer"
    keyvault_certificate_officer_role_name   = "Key Vault Certificates Officer"
    storage_table_data_contributor_role_name = "Storage Table Data Contributor"
    storage_blob_data_contributor_role_name  = "Storage Blob Data Contributor"
    storage_account_contributor_role_name    = "Storage Account Contributor"
    contributor                              = "Contributor"
    acr_pull                                 = "AcrPull"
    cognitive_services_openai_user           = "Cognitive Services OpenAI User"
  }

  # Constants
  data_protection_key_name = "data-protection-key"

  # Shared resources
  budget = {
    monthly_amount_allocated = 1
    alert_threshold          = 60
  }

  azure_ai_foundry_short_name = replace(azurerm_ai_foundry.default.name, "-", "")
  
  tags = {
    "service": local.service_name
    "environment": var.environment_name,
    "product": "Prototype: Azure OpenAI"
  }
}