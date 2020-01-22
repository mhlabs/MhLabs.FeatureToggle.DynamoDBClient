using MhLabs.FeatureToggle.DynamoDBClient.Client;
using MhLabs.FeatureToggle.DynamoDBClient.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace MhLabs.FeatureToggle.DynamoDBClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureToggleClient(this IServiceCollection services, FeatureToggleConfiguration configuration = null)
        {
            services.AddSingleton<IFeatureToggleConfiguration>(configuration ?? new FeatureToggleConfiguration());
            services.AddSingleton<IFeatureToggleClient>();
            return services;
        }       
    }
}