using System.Threading;
using System.Threading.Tasks;
using MhLabs.FeatureToggle.DynamoDBClient.Services.Responses;

namespace MhLabs.FeatureToggle.DynamoDBClient.Services
{
    public interface IFeatureToggleService
    {
        Task<FeatureToggleResponse> Get(string flagName, string userKey, bool defaultValue = default(bool), CancellationToken cancellationToken = default(CancellationToken));
    }
}