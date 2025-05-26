resource "azurerm_cognitive_deployment" "o3_mini_model" {
  cognitive_account_id = azurerm_ai_services.default.id
  name                 = "o3_mini"
  
  model {
    format = "OpenAI"
    name   = "o3-mini"
  }
  
  sku {
    name = "GlobalStandard"
    capacity = 200
  }
}