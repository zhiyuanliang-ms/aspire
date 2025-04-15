// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//using System.Text;
using Aspire.Components.ConformanceTests;
using Azure.Core;
using Azure.Identity;
using Azure.Data.AppConfiguration;
using Microsoft.DotNet.RemoteExecutor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Aspire.Azure.AppConfiguration.Tests;

public class ConformanceTests : ConformanceTests<IConfigurationRefresherProvider, AzureAppConfigurationSettings>
{
    public const string Endpoint = "https://aspiretests.azconfig.io/";

    protected override ServiceLifetime ServiceLifetime => ServiceLifetime.Singleton;

    protected override string ActivitySourceName => "Microsoft.Extensions.Configuration.AzureAppConfiguration";

    protected override string[] RequiredLogCategories => new string[] { "Microsoft.Extensions.Configuration.AzureAppConfiguration.Refresh" };

    protected override bool SupportsKeyedRegistrations => false;

    protected override bool IsBuiltBeforeHost => true;

    protected override string ValidJsonConfig => """
        {
          "Aspire": {
            "Azure": {
              "Data": {
                "AppConfiguration": {
                  "Endpoint": "http://YOUR_URI",
                  "DisableHealthChecks": true,
                  "DisableTracing": false
                }
              }
            }
          }
        }
        """;

    protected override void PopulateConfiguration(ConfigurationManager configuration, string? key = null)
        => configuration.AddInMemoryCollection(new KeyValuePair<string, string?>[]
        {
            new(CreateConfigKey("Aspire:Azure:AppConfiguration", null, "Endpoint"), Endpoint)
        });

    protected override void RegisterComponent(HostApplicationBuilder builder, Action<AzureAppConfigurationSettings>? configure = null, string? key = null)
    {
        builder.AddAzureAppConfiguration(
            "appconfig",
            settings =>
            {
                configure?.Invoke(settings);
                settings.Credential = new EmptyTokenCredential();
            },
            options =>
            {
                options.MinBackoffDuration = TimeSpan.FromSeconds(1);
                options.ConfigureRefresh(refreshOptions =>
                {
                    refreshOptions.Register("sentinel")
                        .SetRefreshInterval(TimeSpan.FromSeconds(1));
                });
                options.ConfigureStartupOptions(startupOptions =>
                {
                    startupOptions.Timeout = TimeSpan.FromSeconds(1);
                });
                options.ConfigureClientOptions(clientOptions => clientOptions.Retry.MaxRetries = 0);
            },
            optional: true);

        var op = new ConfigurationClientOptions();
        op.Retry.MaxRetries = 0;
        builder.Services.AddSingleton(new ConfigurationClient(new Uri(Endpoint), new DefaultAzureCredential(), op));
    }

    protected override (string json, string error)[] InvalidJsonToErrorMessage => new[]
        {
            ("""{"Aspire": { "Azure": { "AppConfiguration": { "Endpoint": "YOUR_URI"}}}}""", "Value does not match format \"uri\""),
            ("""{"Aspire": { "Azure": { "AppConfiguration": { "Endpoint": "http://YOUR_URI", "DisableHealthChecks": "true"}}}}""", "Value is \"string\" but should be \"boolean\""),
        };

    protected override void SetHealthCheck(AzureAppConfigurationSettings options, bool enabled)
        => options.DisableHealthChecks = !enabled;

    protected override void SetMetrics(AzureAppConfigurationSettings options, bool enabled)
        => throw new NotImplementedException();

    protected override void SetTracing(AzureAppConfigurationSettings options, bool enabled)
        => options.DisableTracing = !enabled;

    protected override void TriggerActivity(IConfigurationRefresherProvider service)
    {
        Thread.Sleep(1000);
        service.Refreshers.First().RefreshAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    [Fact]
    public void TracingEnablesTheRightActivitySource()
        => RemoteExecutor.Invoke(() => ActivitySourceTest(key: null)).Dispose();

    internal sealed class EmptyTokenCredential : TokenCredential
    {
        public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return new AccessToken(string.Empty, DateTimeOffset.MaxValue);
        }

        public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return new ValueTask<AccessToken>(new AccessToken(string.Empty, DateTimeOffset.MaxValue));
        }
    }
}
