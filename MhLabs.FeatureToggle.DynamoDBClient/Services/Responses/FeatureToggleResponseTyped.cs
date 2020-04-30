using System;

namespace MhLabs.FeatureToggle.DynamoDBClient.Services.Responses
{
    public class FeatureToggleResponse<T>
    {
        public T Toggle { get; set; }
        public string Error { get; set; }
        public long TimeStamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
