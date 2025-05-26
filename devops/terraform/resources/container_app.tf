module "container_webapp_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = local.service_name
  type                = "ca"
  instance            = var.environment_instance
  delimiter           = "-"
}

resource "azurerm_role_assignment" "acr_pull" {
  scope                 = data.azurerm_container_registry.default.id
  role_definition_name  = local.built_in_roles.acr_pull
  principal_id          = azurerm_user_assigned_identity.default.principal_id
}

resource "azurerm_container_app" "service" {
  name                         = module.container_webapp_naming_convention.name
  container_app_environment_id = azurerm_container_app_environment.default.id
  resource_group_name          = azurerm_resource_group.default.name
  revision_mode                = "Single"

  lifecycle {
    ignore_changes = [
      ingress[0].custom_domain
    ]
  }

  identity {
    type = "UserAssigned"
    identity_ids = [
      azurerm_user_assigned_identity.default.id
    ]
  }

  registry {
    server = var.container_registry_url
    identity = azurerm_user_assigned_identity.default.id
  }

  ingress {
    allow_insecure_connections  = false
    external_enabled            = true
    target_port                 = 8080
    transport                   = "auto"
    
    traffic_weight {
      percentage      = 100
      latest_revision = true
    }
  }
  
  template {
    
    container {
      name   = "prototype"
      image  = var.image_tag
      cpu    = 0.25
      memory = "0.5Gi"

      env {
        name  = "APPLICATIONINSIGHTS_CONNECTION_STRING"
        value = azurerm_application_insights.default.connection_string
      }

      env {
        name  = "AZURE_CLIENT_ID"
        value = azurerm_user_assigned_identity.default.client_id
      }

      env {
        name  = "ServiceConfiguration_BuildNumber"
        value = var.build_number
      }

      env {
        name  = "KeyVaultConfiguration__Endpoint"
        value = azurerm_key_vault.default.vault_uri
      }

      env {
        name  = "DataProtectionKey__BlobUri"
        value = azurerm_storage_blob.keys.url
      }

      env {
        name  = "DataProtectionKey__KeyUri"
        value = azurerm_key_vault_key.data_protection_key.id
      }

      env {
        name  = "AzureOpenAiConfiguration__Endpoint"
        value = azurerm_ai_foundry_project.default.endpoint
      }

      env {
        name  = "AzureOpenAiConfiguration__DeploymentName"
        value = azurerm_cognitive_deployment.gpt4o_model.name
      }
    }

    min_replicas = 0
    max_replicas = 1
  }

  tags = local.tags
}