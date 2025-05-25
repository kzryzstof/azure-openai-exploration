resource "azurerm_consumption_budget_resource_group" "budget" {
  name              = "budget-${local.service_name}-${var.environment_name}-${var.environment_instance}"
  resource_group_id = azurerm_resource_group.default.id

  amount            = local.budget.monthly_amount_allocated
  time_grain        = "Monthly"

  time_period {
    start_date = "2025-05-01T00:00:00Z"
  }

  notification {
    enabled   = true
    threshold = local.budget.alert_threshold
    operator  = "GreaterThan"

    contact_emails = [
      var.budget_email_address
    ]
  }
}