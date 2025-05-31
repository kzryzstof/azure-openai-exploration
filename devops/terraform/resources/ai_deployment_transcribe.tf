resource "azurerm_cognitive_deployment" "transcribe_model" {
  cognitive_account_id = azurerm_ai_services.default.id
  name                 = "transcribe-model"
  
  model {
    format  = "OpenAI"
    name    = "gpt-4o-mini-transcribe"
    version = "2025-03-20"
  }
  
  sku {
    name = "GlobalStandard"
    capacity = 500
  }
}
