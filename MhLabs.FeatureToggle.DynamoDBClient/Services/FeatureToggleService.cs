using System;
using System.Threading;
using System.Threading.Tasks;
using LaunchDarkly.Client;
using LaunchDarkly.Client.DynamoDB;
using MhLabs.FeatureToggle.DynamoDBClient.Configuration;
using MhLabs.FeatureToggle.DynamoDBClient.Services.Responses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace MhLabs.FeatureToggle.DynamoDBClient.Services
{
    public class FeatureToggleService : IFeatureToggleService
    {
        private readonly ILdClient _ldClient;
        private readonly ILogger<FeatureToggleService> _logger;

        public FeatureToggleService(IFeatureToggleConfiguration configuration, ILogger<FeatureToggleService> logger)
        {
            var store = DynamoDBComponents.DynamoDBFeatureStore(configuration.TableName)
            .WithCaching(FeatureStoreCacheConfig.Enabled.WithTtlSeconds(configuration.CacheDurationInSeconds));

            var ldConfig = new LaunchDarkly.Client.Configuration()
                .WithFeatureStoreFactory(store).WithOffline(true);

            _ldClient = new LdClient(ldConfig);
            _logger = logger;
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

        public async Task<FeatureToggleResponse<T>> GetJSON<T>(string flagName, string userKey)
        {
            try
            {
                await Task.CompletedTask;
                var flag = _ldClient.JsonVariation(flagName, new User(userKey), default);

                return new FeatureToggleResponse<T>
                {
                    Toggle = flag.ToObject<T>(),
                    Error = null
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Flag: {FlagName}. UserKey: {UserKey}", flagName, userKey);

                return new FeatureToggleResponse<T>
                {
                    Toggle = default,
                    Error = exception.Message
                };
            }
        }
    }
}
