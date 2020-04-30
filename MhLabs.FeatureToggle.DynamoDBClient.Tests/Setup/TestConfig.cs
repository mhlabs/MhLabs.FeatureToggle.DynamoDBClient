using Moq;
using AutoFixture;
using MhLabs.FeatureToggle.DynamoDBClient.Configuration;
using MhLabs.FeatureToggle.DynamoDBClient.Services.Responses;

namespace MhLabs.FeatureToggle.DynamoDBClient.Tests
{

    public class TestConfig 
    {
        private static readonly Fixture _fixture = new Fixture();

        public IFeatureToggleConfiguration Configuration;
        public FeatureToggleResponse Response = new FeatureToggleResponse();
        public string FlagName = _fixture.Create<string>();
        public string UserKey = _fixture.Create<string>();
        public bool DefaultValue = _fixture.Create<bool>();
        

        public TestConfig(int? apiRequestTimeoutMilliseconds = null)
        {
            var config = new Mock<IFeatureToggleConfiguration>();
            config.Setup(x => x.CacheDurationInSeconds).Returns(60);
            config.Setup(x => x.TableName).Returns("TestTable");
            Configuration = config.Object;
        }
    }
}
