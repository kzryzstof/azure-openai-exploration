variable "environment_name" {
  type  = string
}

variable "environment_instance" {
  type  = string
}

variable "environment_location" {
  type  = string
  description = "AI models are not available everywhere"
}

variable "image_tag" {
  type  = string
}

variable "build_number" {
  type  = string
}

# The following values are set in the pipeline.
variable "tenant_id" {
  type  = string
}

variable "subscription_id" {
  type  = string
}

variable "shared_resources_subscription_id" {
  type  = string
}

variable "container_registry_resource_group_name" {
  type  = string
}

variable "container_registry_name" {
  type  = string
}

variable "container_registry_url" {
  type  = string
}

variable "budget_email_address" {
  type  = string
}

variable "eleven_labs_api_key" {
  type  = string
}

variable "eleven_labs_voice_id" {
  type  = string
}