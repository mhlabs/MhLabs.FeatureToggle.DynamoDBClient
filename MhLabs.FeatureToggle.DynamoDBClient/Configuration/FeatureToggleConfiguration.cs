using System;

namespace MhLabs.FeatureToggle.DynamoDBClient.Configuration
{

    public class FeatureToggleConfiguration : IFeatureToggleConfiguration
    {
        private const double DefaultCacheDurationSeconds = 60;
        public double CacheDurationInSeconds { get; set; } = long.TryParse(Environment.GetEnvironmentVariable("CacheDurationInSeconds"), out var duration) ?
            duration :
            DefaultCacheDurationSeconds;

        public string TableName => Environment.GetEnvironmentVariable("FeatureFlagsTable");
    }
}