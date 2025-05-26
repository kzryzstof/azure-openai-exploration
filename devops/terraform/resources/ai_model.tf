resource "azurerm_cognitive_deployment" "o4_mini_model" {
  cognitive_account_id = azurerm_ai_services.default.id
  name                 = "o4_mini"
  
  model {
    format = "OpenAI"
    name   = "o4-mini"
  }
  
  sku {
    name = "GlobalBatch"
    capacity = 200
  }
}