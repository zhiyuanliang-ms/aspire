// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Azure.Identity;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace Aspire.Azure.Configuration.AppConfiguration;

/// <summary>
/// POC
/// </summary>
public static class AspireAppConfigurationExtensions
{
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

    /// <summary>
    /// POC
    /// </summary>
    public static IConfigurationBuilder AddAzureAppConfiguration(
        this IConfigurationManager builder,
        string connectionName,
        Action<AzureAppConfigurationOptions>? action = null)
    {
        string? connectionString = builder.GetConnectionString(connectionName);
        if (connectionString is null)
        {
            throw new InvalidOperationException($"Connection string is not found.");
        }

        Console.WriteLine($"!!!!!!!!!!!!!Connection string is: {connectionString}");

        bool isRealEndpoint = connectionString.EndsWith("azconfig.io", StringComparison.OrdinalIgnoreCase);

        if (!isRealEndpoint)
        {
            return builder.AddAzureAppConfiguration(options =>
            {
                options.Connect($"Endpoint={connectionString};Id=xxxx;Secret=xxxx");
                action?.Invoke(options);
            });
        }

        return builder.AddAzureAppConfiguration(options =>
        {
            options.Connect(new Uri(connectionString), new AzureCliCredential());
            action?.Invoke(options);
        });
    }
}
