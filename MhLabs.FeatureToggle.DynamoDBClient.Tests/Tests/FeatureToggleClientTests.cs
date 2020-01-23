using System;
using Xunit;
using Moq;
using AutoFixture;
using System.Threading.Tasks;
using Shouldly;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using MhLabs.FeatureToggle.DynamoDBClient.Services;
using MhLabs.FeatureToggle.DynamoDBClient.Client;
using Microsoft.Extensions.Logging.Abstractions;
using MhLabs.FeatureToggle.DynamoDBClient.Extensions;
using MhLabs.FeatureToggle.DynamoDBClient.Configuration;

namespace MhLabs.FeatureToggle.DynamoDBClient.Tests
{
    public class FeatureToggleClientTests
    {
        private readonly Mock<IFeatureToggleService> _service = new Mock<IFeatureToggleService>();
        private readonly Fixture _fixture = new Fixture();
        
        public IFeatureToggleClient CreateClient(TestConfig config = null, IFeatureToggleService service = null)
        {
            var testConfig = config ?? new TestConfig();

            return new FeatureToggleClient(service ?? _service.Object, testConfig.Configuration, NullLogger<FeatureToggleClient>.Instance);
        }

        [Fact]
        public void Can_Resolve_Feature_Toggle_Client()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddFeatureToggleClient();
            serviceCollection.AddLogging();


            var provider = serviceCollection.BuildServiceProvider();

            // Act
            var instance = provider.GetService<IFeatureToggleClient>();

            // Assert
            instance.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Return_Response_When_SuccesfulAsync()
        {
            // Arrange
            var config = new TestConfig();
            config.Response.Enabled = true;
            config.Response.Error = null;
            
            _service.Setup(x => x.Get(config.FlagName, config.UserKey, config.DefaultValue, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(config.Response);

            // Act
            var client = CreateClient(config);
            var response = await client.Get(config.FlagName, config.UserKey, config.DefaultValue);
            
            // Assert
            response.Enabled.ShouldBeTrue();
            response.Error.ShouldBeNull();
        }

        [Fact]
        public async Task Should_Return_Response_When_Exception()
        {
            // Arrange
            var config = new TestConfig();
            config.Response.Enabled = true;
            config.Response.Error = null;


            _service.Setup(x => x.Get(config.FlagName, config.UserKey, config.DefaultValue, It.IsAny<CancellationToken>()))
                                .ThrowsAsync(new UnauthorizedAccessException());

            // Act
            var client = CreateClient(config);
            var response = await client.Get(config.FlagName, config.UserKey, config.DefaultValue);
            
            // Assert
            response.Enabled.ShouldBe(config.DefaultValue);
            response.Error.ShouldBe(typeof(UnauthorizedAccessException).Name);
        }
    }
}
