resource "azurerm_cognitive_deployment" "gpt_4_1_nano_model" {
  cognitive_account_id = azurerm_ai_services.default.id
  name                 = "gpt-4.1-nano"
  
  model {
    format = "OpenAI"
    name   = "gpt-4.1-nano"
  }
  
  sku {
    name = "Standard"
    capacity = 200
  }
}