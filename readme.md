*Introduction*

The repository contains a prototype application that explores Azure AI Foundry and its SDK. 

The repository contains a CI/CD pipeline that runs in Azure DevOps which:

- Builds a containerized Blazor application and publishes it in an ACR;
- Uses Terraform to plan the resources to create in an Azure subscription;
- Uses Terraform to deploy the application in an Azure Container Application;

Regarding Azure AI Foundry, the repository creates:

- An Azure AI account;
- An Azure AI Foundry;
- An Azure AI Foundry Project;
- A chat model GPT-4.1-nano (carefully regarding its location as there are severe quotas) 

*Setup*

 The requirements are:

- An Azure Container Registry where to store the container images
- A resource group where to store the Terraform file

- Services connections:
  - containerRegistry_ServiceConnectionName: a Docker connection to the ACR repository
  - acrResourceGroup_ServiceConnectionName: an ARM connection to the ACR repository, used to clean-up outdated images
  - prototypesSubscription_ServiceConnectionName: an ARM connection to the Azure subscription where the service is deployed

