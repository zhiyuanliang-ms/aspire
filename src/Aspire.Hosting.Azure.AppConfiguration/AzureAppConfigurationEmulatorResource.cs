// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Azure.AppConfiguration;

namespace Aspire.Hosting.Azure;

/// <summary>
/// Wraps an <see cref="AzureAppConfigurationResource" /> in a type that exposes container extension methods.
/// </summary>
/// <param name="innerResource">The inner resource used to store annotations.</param>
public class AzureAppConfigurationEmulatorResource(AzureAppConfigurationResource innerResource) : ContainerResource(innerResource.Name), IResource
{
    private readonly AzureAppConfigurationResource _innerResource = innerResource ?? throw new ArgumentNullException(nameof(innerResource));

    /// <summary>
    /// Enables anonymous authentication for the Azure App Configuration emulator resource.
    /// </summary>
    internal void EnableAnonymousAuthentication(string role = "Owner")
    {
        _innerResource.EmulatorOptions.AnonymousAccessEnabled = true;
        _innerResource.EmulatorOptions.AnonymousUserRole = role;
    }

    /// <summary>
    /// Enables HMAC-SHA256 authentication for the Azure App Configuration emulator resource with the specified access key.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="secret"></param>
    internal int AddAccessKey(string id, string secret)
    {
        _innerResource.EmulatorOptions.HmacAuthenticationEnabled = true;
        _innerResource.EmulatorOptions.AccessKeys.Add(new AzureAppConfigurationEmulatorAccessKey
        {
            Id = id,
            Secret = secret
        });
        return _innerResource.EmulatorOptions.AccessKeys.Count - 1;
    }

    /// <inheritdoc/>
    public override ResourceAnnotationCollection Annotations => _innerResource.Annotations;
}
