# MhLabs.FeatureToggle.DynamoDBClient

A thin client around [LaunchDarkly.ServerSdk.DynamoDB](https://github.com/launchdarkly/dotnet-server-sdk-dynamodb) that reads flags in an offline mode from a DynamoDB table. This table is populated via a separate worker and the idea is that the code using this client shouldn't need any internet access.

## Usage
To register the client using IoC:
```
serviceCollection.AddFeatureToggleClient();
```

To check if a flag is enabled:
```
private readonly IFeatureToggleClient _client;
[...]
var enabled = _client.Enabled("flagKey", "userKey");

```

or 

```
var enabled = _client.Get("flagKey", "userKey").Enabled;
```

## Configuration
```
var configuration = new FeatureTogleConfiguration {
    TableName = "DynamoDbTableName", // Defaults to Environment.GetEnvironmentVariable("FeatureFlagsTable")
    CacheDurationInSeconds = <cache duration> // Defaults to 60
} 
serviceCollection.AddFeatureToggleClient();
```