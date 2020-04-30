using System.Threading.Tasks;
using MhLabs.FeatureToggle.DynamoDBClient.Services.Responses;

namespace MhLabs.FeatureToggle.DynamoDBClient.Client
{
    public interface IFeatureToggleClient
    {
        Task<FeatureToggleResponse> Get(string flagName, string userKey, bool defaultValue = default(bool));
        Task<bool> Enabled(string flagName, string userKey, bool defaultValue = default(bool));
        Task<FeatureToggleResponse<T>> Get<T>(string flagName, string userKey);
    }
}
