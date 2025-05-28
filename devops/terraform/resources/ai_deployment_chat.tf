resource "azurerm_cognitive_deployment" "chat_model" {
  cognitive_account_id = azurerm_ai_services.default.id
  name                 = "chat-model"
  
  model {
    format  = "OpenAI"
    name    = "gpt-4.1-nano"
    version = "2025-04-14"
  }
  
  sku {
    name = "GlobalStandard"
    capacity = 10
  }
}