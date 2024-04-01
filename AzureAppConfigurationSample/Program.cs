using AzureAppConfigurationSample;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Azure.Identity;

var builder = Host.CreateApplicationBuilder(args);

var endpoint = builder.Configuration.GetValue<string>("AzureAppConfiguration:Endpoint");
var label = builder.Configuration.GetValue<string>("AzureAppConfiguration:Label");
var sentinelKey = builder.Configuration.GetValue<string>("AzureAppConfiguration:SentinelKey");

if (!string.IsNullOrWhiteSpace(endpoint))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(new Uri(endpoint), new DefaultAzureCredential())
        .Select(KeyFilter.Any, label)
        .ConfigureRefresh(refreshOptions =>
        {
            refreshOptions.Register(sentinelKey, label, refreshAll: true);
        })
        .ConfigureKeyVault(keyVaultOptions => keyVaultOptions.SetCredential(new DefaultAzureCredential()));
    });
}

builder.Services.Configure<MyApplicationConfiguration>(builder.Configuration.GetSection("MyApplicationConfiguration"));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
