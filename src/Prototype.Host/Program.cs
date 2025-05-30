using Azure.Identity;
using DriftingBytesLabs.Prototype.Host.Components;
using DriftingBytesLabs.Prototype.Host.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Radzen;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var tokenCredential = new DefaultAzureCredential();

//	------------------------------------------------------------------------------------------
//  Sets up the Data Protection keys
//	------------------------------------------------------------------------------------------
builder.Services
    .AddDataProtection()
    .PersistKeysToAzureBlobStorage
    (
        new Uri(builder.Configuration["DataProtectionKey:BlobUri"] ?? "https://storage/data/protection/not/configured"),
        tokenCredential
    )
    .ProtectKeysWithAzureKeyVault
    (
        new Uri(builder.Configuration["DataProtectionKey:KeyUri"] ?? "https://keyvault/data/protection/not/configured"),
        tokenCredential
    )
    ;

//	------------------------------------------------------------------------------------------
//  Injects the key vault as a source for the configuration
//	------------------------------------------------------------------------------------------
builder
    .Configuration
    .AddAzureKeyVault
    (
        new Uri(builder.Configuration["KeyVaultConfiguration:Endpoint"] ?? string.Empty),
        tokenCredential
    );

//	------------------------------------------------------------------------------------------
//  Add services to the container & Blazor
//	------------------------------------------------------------------------------------------
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

//	------------------------------------------------------------------------------------------
//  Injects our services
//	------------------------------------------------------------------------------------------
builder.Services
    .AddServices(builder.Configuration);

//	------------------------------------------------------------------------------------------
//  Builds the Web Application
//	------------------------------------------------------------------------------------------
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();