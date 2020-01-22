namespace MhLabs.FeatureToggle.DynamoDBClient.Configuration
{
    public interface IFeatureToggleConfiguration
    {
        double CacheDurationInSeconds { get; }
        string TableName { get; }
    }
}
