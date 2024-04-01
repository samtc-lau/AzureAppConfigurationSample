namespace AzureAppConfigurationSample;

public record MyApplicationConfiguration
{
    public required string ApplicationName { get; init; }
    public required string ApplicationSecret { get; init; }
}

