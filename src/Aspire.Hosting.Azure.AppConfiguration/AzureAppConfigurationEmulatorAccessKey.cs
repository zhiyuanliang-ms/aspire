// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Aspire.Hosting.Azure.AppConfiguration;

internal sealed class AzureAppConfigurationEmulatorAccessKey
{
    public required string Id { get; set; }

    public required string Secret { get; set; }
}
