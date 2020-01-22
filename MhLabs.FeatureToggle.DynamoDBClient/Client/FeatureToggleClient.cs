using System;
using System.Threading.Tasks;
using MhLabs.FeatureToggle.DynamoDBClient.Configuration;
using MhLabs.FeatureToggle.DynamoDBClient.Services;
using MhLabs.FeatureToggle.DynamoDBClient.Services.Responses;
using Microsoft.Extensions.Logging;

namespace MhLabs.FeatureToggle.DynamoDBClient.Client
{
    public class FeatureToggleClient : IFeatureToggleClient
    {
        private readonly ILogger<FeatureToggleClient> _logger;
        private readonly IFeatureToggleService _service;

        public FeatureToggleClient(IFeatureToggleService service, IFeatureToggleConfiguration configuration, ILogger<FeatureToggleClient> logger)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<bool> Enabled(string flagName, string userKey, bool defaultValue = false)
        {
            return (await Get(flagName, userKey, defaultValue))?.Enabled == true;
        }

        public async Task<FeatureToggleResponse> Get(string flagName, string userKey, bool defaultValue = default)
        {
            try
            {
                return await _service.Get(flagName, userKey, defaultValue);
            }
            catch (Exception ex)
            {
                return HandleException(ex, flagName, userKey, defaultValue);
            }
        }

        private FeatureToggleResponse HandleException(Exception ex, string flagName, string userKey, bool defaultValue)
        {
            if (ex is UnauthorizedAccessException)
            {
                _logger.LogError(ex, "Request UnauthorizedAccessException - Flag: {Flag}. User: {UserKey}", flagName, userKey);
            }
            else if (ex is TaskCanceledException)
            {
                _logger.LogError(ex, "Request TaskCanceledException - Flag: {Flag}. User: {UserKey}", flagName, userKey);
            }
            else
            {
                _logger.LogError(ex, "Request Exception - Flag: {Flag}. User: {UserKey}", flagName, userKey);
            }

            return new FeatureToggleResponse() 
            {
                Enabled = defaultValue,
                Error = ex.GetType().Name
            };
        }
    }
}