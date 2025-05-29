## Introduction

The repository contains a prototype application that explores `Azure AI Foundry` and its SDK. 

It has an Azure DevOps CI/CD pipeline that:

- Builds a containerized Blazor application published in an `Azure Container Registry`;
- Uses `Terraform` to plan the resources to create in an Azure subscription;
- Uses `Terraform` to deploy the containerized application in an `Azure Container Application`;

Regarding `Azure AI Foundry`, the repository creates also the following resources:

- An `Azure AI Service`;
- An `Azure AI Foundry`;
- An `Azure AI Foundry Project`;
- A chat model `GPT-4.1-nano` (careful regarding its location as there are severe quotas) 

## Setup

 The requirements are:

- An `Azure Container Registry` resource where to store the container images. It is not part of the terraformed resources;
- A `Resource Group` and a `Storage Account` where to store the `Terraform` state file. It is not part of the terraformed resources;

Finally, the Azure DevOps pipelines requires the following service connections set up:

- `containerRegistry_ServiceConnectionName`: a Docker connection to the `ACR` repository;
- `acrResourceGroup_ServiceConnectionName`: an ARM connection to the `ACR` repository, used to clean-up outdated images;
- `prototypesSubscription_ServiceConnectionName`: an ARM connection to the `Azure subscription` where the service is deployed

