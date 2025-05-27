resource "azurerm_cognitive_deployment" "default" {
  cognitive_account_id = azurerm_ai_services.default.id
  name                 = "gpt-4.1-mini"
  
  model {
    format = "OpenAI"
    name   = "gpt-4.1-mini"
  }
  
  sku {
    name = "GlobalStandard"
    capacity = 200
  }
}