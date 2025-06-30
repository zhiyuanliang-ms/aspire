// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Aspire.Hosting.Azure.AppConfiguration;

internal sealed class AzureAppConfigurationEmulatorOptions
{
    public bool AnonymousAccessEnabled { get; set; } = true;

    public string AnonymousUserRole { get; set; } = "Owner";

    public bool HmacAuthenticationEnabled { get; set; }

    public List<AzureAppConfigurationEmulatorAccessKey> AccessKeys { get; set; } = new List<AzureAppConfigurationEmulatorAccessKey>();
}
