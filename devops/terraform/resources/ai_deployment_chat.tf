resource "azurerm_cognitive_deployment" "chat_model" {
  cognitive_account_id = azurerm_ai_services.default.id
  name                 = "chat-model"
  
  model {
    format = "OpenAI"
    name   = "gpt-4.1-mini"
  }
  
  sku {
    name = "Standard"
    capacity = 200
  }
}