resource "azurerm_cognitive_deployment" "speech_model" {
  cognitive_account_id = azurerm_ai_services.default.id
  name                 = "speech-model"
  
  model {
    format  = "OpenAI"
    name    = "gpt-4o-mini-tts"
  }
  
  sku {
    name = "GlobalStandard"
    capacity = 10
  }
}
