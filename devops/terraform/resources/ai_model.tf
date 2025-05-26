resource "azurerm_cognitive_deployment" "gpt4o_model" {
  cognitive_account_id = azurerm_ai_services.default.id
  name                 = "gpt4o"
  
  model {
    format = "OpenAI"
    name   = "gpt-4o"
    version= "2024-05-13"
  }
  
  sku {
    name = "GlobalStandard"
    capacity = 200
  }
}