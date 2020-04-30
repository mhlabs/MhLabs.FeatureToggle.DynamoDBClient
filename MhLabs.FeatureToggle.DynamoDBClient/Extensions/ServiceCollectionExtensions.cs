using MhLabs.FeatureToggle.DynamoDBClient.Client;
using MhLabs.FeatureToggle.DynamoDBClient.Configuration;
using MhLabs.FeatureToggle.DynamoDBClient.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MhLabs.FeatureToggle.DynamoDBClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureToggleClient(this IServiceCollection services, FeatureToggleConfiguration configuration = null)
        {
            services.AddSingleton<IFeatureToggleConfiguration>(configuration ?? new FeatureToggleConfiguration());
            services.AddSingleton<IFeatureToggleClient, FeatureToggleClient>();
            services.AddSingleton<IFeatureToggleService, FeatureToggleService>();
            return services;
        }
    }
}
