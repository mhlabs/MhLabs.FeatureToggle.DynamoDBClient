using System;
using System.Threading;
using System.Threading.Tasks;
using LaunchDarkly.Client;
using LaunchDarkly.Client.DynamoDB;
using MhLabs.FeatureToggle.DynamoDBClient.Configuration;
using MhLabs.FeatureToggle.DynamoDBClient.Services.Responses;

namespace MhLabs.FeatureToggle.DynamoDBClient.Services
{
    public class FeatureToggleService : IFeatureToggleService
    {
        private readonly ILdClient _ldClient;
        public FeatureToggleService(FeatureToggleConfiguration configuration)
        {

            var store = DynamoDBComponents.DynamoDBFeatureStore(configuration.TableName)
            .WithCaching(FeatureStoreCacheConfig.Enabled.WithTtlSeconds(configuration.CacheDurationInSeconds));

            var ldConfig = new LaunchDarkly.Client.Configuration()
                .WithFeatureStoreFactory(store).WithOffline(true);

            _ldClient = new LdClient(ldConfig);
        }
        public async Task<FeatureToggleResponse> Get(string flagName, string userKey, bool defaultValue = false, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;

            var flag = _ldClient.BoolVariation(flagName, new User(userKey));
            return new FeatureToggleResponse()
            {
                Enabled = flag,
                    Error = null
            };
        }
    }
}