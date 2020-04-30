using System.Threading.Tasks;
using System.Threading;
using MhLabs.FeatureToggle.DynamoDBClient.Services;
using MhLabs.FeatureToggle.DynamoDBClient.Services.Responses;

namespace MhLabs.FeatureToggle.DynamoDBClient.Tests
{

    public class DelayedDummyFeatureToggleService : IFeatureToggleService
    {
        public async Task<FeatureToggleResponse> Get(string flagName, string userKey, bool defaultValue = false, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000, cancellationToken);
            return new FeatureToggleResponse();
        }

        public async Task<FeatureToggleResponse<T>> GetJSON<T>(string flagName, string userKey)
        {
            await Task.Delay(1000);
            return new FeatureToggleResponse<T>();
        }
    }
}
