using System;

namespace MhLabs.FeatureToggle.DynamoDBClient.Services.Responses
{
    public class FeatureToggleResponse
    {
        public bool Enabled { get; set; }
        public string Error { get; set; }
        public long TimeStamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}