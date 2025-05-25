module "dashboard_naming_convention" {
  source              = "../modules/naming_convention"
  environment         = var.environment_name
  name                = local.service_name
  type                = "ds"
  instance            = var.environment_instance
  delimiter           = "-"
}

resource "azurerm_portal_dashboard" "default" {
  name                = module.dashboard_naming_convention.name
  resource_group_name = azurerm_resource_group.default.name
  location            = var.environment_location

  tags = local.tags

  dashboard_properties = <<DASH
{
  "lenses": {
    "0": {
      "order": 0,
      "parts": {
        "0": {
          "position": {
            "x": 0,
            "y": 0,
            "colSpan": 2,
            "rowSpan": 2
          },
          "metadata": {
            "inputs": [],
            "type": "Extension/HubsExtension/PartType/ClockPart",
            "settings": {
              "content": {
                "settings": {
                  "timeFormat": "hh:mma",
                  "timezoneId": "Eastern Standard Time",
                  "version": 1
                }
              }
            }
          }
        },
        "1": {
          "position": {
            "x": 2,
            "y": 0,
            "colSpan": 4,
            "rowSpan": 3
          },
          "metadata": {
            "inputs": [],
            "type": "Extension/HubsExtension/PartType/MonitorChartPart",
            "settings": {
              "content": {
                "options": {
                  "chart": {
                    "metrics": [
                      {
                        "resourceMetadata": {
                          "id": "${azurerm_container_app.service.id}"
                        },
                        "name": "UsageNanoCores",
                        "aggregationType": 1,
                        "namespace": "microsoft.app/containerapps",
                        "metricVisualization": {
                          "displayName": "CPU usage",
                          "resourceDisplayName": "${azurerm_container_app.service.name}"
                        }
                      }
                    ],
                    "title": "CPU",
                    "titleKind": 2,
                    "visualization": {
                      "chartType": 2,
                      "legendVisualization": {
                        "isVisible": true,
                        "position": 2,
                        "hideSubtitle": false
                      },
                      "axisVisualization": {
                        "x": {
                          "isVisible": true,
                          "axisType": 2
                        },
                        "y": {
                          "isVisible": true,
                          "axisType": 1
                        }
                      },
                      "disablePinning": true
                    }
                  }
                }
              }
            }
          }
        },
        "2": {
          "position": {
            "x": 6,
            "y": 0,
            "colSpan": 6,
            "rowSpan": 3
          },
          "metadata": {
            "inputs": [],
            "type": "Extension/HubsExtension/PartType/MonitorChartPart",
            "settings": {
              "content": {
                "options": {
                  "chart": {
                    "metrics": [
                      {
                        "resourceMetadata": {
                          "id": "${azurerm_container_app.service.id}"
                        },
                        "name": "WorkingSetBytes",
                        "aggregationType": 4,
                        "namespace": "microsoft.app/containerapps",
                        "metricVisualization": {
                          "displayName": "Memory working set",
                          "resourceDisplayName": "${azurerm_container_app.service.name}"
                        }
                      }
                    ],
                    "title": "Memory working set",
                    "titleKind": 2,
                    "visualization": {
                      "chartType": 2,
                      "legendVisualization": {
                        "isVisible": true,
                        "position": 2,
                        "hideSubtitle": false
                      },
                      "axisVisualization": {
                        "x": {
                          "isVisible": true,
                          "axisType": 2
                        },
                        "y": {
                          "isVisible": true,
                          "axisType": 1
                        }
                      },
                      "disablePinning": true
                    }
                  }
                }
              }
            }
          }
        },
        "4": {
          "position": {
            "x": 0,
            "y": 3,
            "colSpan": 6,
            "rowSpan": 4
          },
          "metadata": {
            "inputs": [
              {
                "name": "options",
                "isOptional": true
              },
              {
                "name": "sharedTimeRange",
                "isOptional": true
              }
            ],
            "type": "Extension/HubsExtension/PartType/MonitorChartPart",
            "settings": {
              "content": {
                "options": {
                  "chart": {
                    "metrics": [
                      {
                        "resourceMetadata": {
                          "id": "${azurerm_application_insights.default.id}"
                        },
                        "name": "accountsmgr.account.create.succeeded.count",
                        "aggregationType": 7,
                        "namespace": "azure.applicationinsights",
                        "metricVisualization": {
                          "displayName": "Succeeded",
                          "resourceDisplayName": "${azurerm_application_insights.default.name}"
                        }
                      },
                      {
                        "resourceMetadata": {
                          "id": "${azurerm_application_insights.default.id}"
                        },
                        "name": "accountsmgr.accounts.create.failed.count",
                        "aggregationType": 7,
                        "namespace": "azure.applicationinsights",
                        "metricVisualization": {
                          "displayName": "Failed",
                          "color": "#d40000",
                          "resourceDisplayName": "${azurerm_application_insights.default.name}"
                        }
                      }
                    ],
                    "title": "Accounts created",
                    "titleKind": 1,
                    "visualization": {
                      "chartType": 1,
                      "legendVisualization": {
                        "isVisible": true,
                        "position": 2,
                        "hideSubtitle": false
                      },
                      "axisVisualization": {
                        "x": {
                          "isVisible": true,
                          "axisType": 2
                        },
                        "y": {
                          "isVisible": true,
                          "axisType": 1
                        }
                      },
                      "disablePinning": true
                    }
                  }
                }
              }
            }
          }
        },
        "5": {
          "position": {
            "x": 6,
            "y": 3,
            "colSpan": 6,
            "rowSpan": 4
          },
          "metadata": {
            "inputs": [
              {
                "name": "options",
                "isOptional": true
              },
              {
                "name": "sharedTimeRange",
                "isOptional": true
              }
            ],
            "type": "Extension/HubsExtension/PartType/MonitorChartPart",
            "settings": {
              "content": {
                "options": {
                  "chart": {
                    "metrics": [
                      {
                        "resourceMetadata": {
                          "id": "${azurerm_application_insights.default.id}"
                        },
                        "name": "accountsmgr.account.delete.succeeded.count",
                        "aggregationType": 7,
                        "namespace": "azure.applicationinsights",
                        "metricVisualization": {
                          "displayName": "Succeeded",
                          "resourceDisplayName": "${azurerm_application_insights.default.name}"
                        }
                      },
                      {
                        "resourceMetadata": {
                          "id": "${azurerm_application_insights.default.id}"
                        },
                        "name": "accountsmgr.accounts.delete.failed.count",
                        "aggregationType": 7,
                        "namespace": "azure.applicationinsights",
                        "metricVisualization": {
                          "displayName": "Failed",
                          "color": "#d40000",
                          "resourceDisplayName": "${azurerm_application_insights.default.name}"
                        }
                      }
                    ],
                    "title": "Accounts deleted",
                    "titleKind": 1,
                    "visualization": {
                      "chartType": 1,
                      "legendVisualization": {
                        "isVisible": true,
                        "position": 2,
                        "hideSubtitle": false
                      },
                      "axisVisualization": {
                        "x": {
                          "isVisible": true,
                          "axisType": 2
                        },
                        "y": {
                          "isVisible": true,
                          "axisType": 1
                        }
                      },
                      "disablePinning": true
                    }
                  }
                }
              }
            }
          }
        },
        "6": {
          "position": {
            "x": 12,
            "y": 3,
            "colSpan": 6,
            "rowSpan": 4
          },
          "metadata": {
            "inputs": [
              {
                "name": "options",
                "isOptional": true
              },
              {
                "name": "sharedTimeRange",
                "isOptional": true
              }
            ],
            "type": "Extension/HubsExtension/PartType/MonitorChartPart",
            "settings": {
              "content": {
                "options": {
                  "chart": {
                    "metrics": [
                      {
                        "resourceMetadata": {
                          "id": "${azurerm_application_insights.default.id}"
                        },
                        "name": "accountsmgr.accounts.requests.count",
                        "aggregationType": 7,
                        "namespace": "azure.applicationinsights",
                        "metricVisualization": {
                          "displayName": "Requests",
                          "resourceDisplayName": "${azurerm_application_insights.default.name}"
                        }
                      }
                    ],
                    "title": "Accounts requested",
                    "titleKind": 1,
                    "visualization": {
                      "chartType": 1,
                      "legendVisualization": {
                        "isVisible": true,
                        "position": 2,
                        "hideSubtitle": false
                      },
                      "axisVisualization": {
                        "x": {
                          "isVisible": true,
                          "axisType": 2
                        },
                        "y": {
                          "isVisible": true,
                          "axisType": 1
                        }
                      },
                      "disablePinning": true
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  },
  "metadata": {
    "model": {}
  }
}
  DASH
}