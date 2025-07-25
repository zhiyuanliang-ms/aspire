// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Aspire.Dashboard.Telemetry;

public static class TelemetryPropertyKeys
{
    private const string AspireDashboardPropertyPrefix = "Aspire.Dashboard.";

    // Default properties
    public const string DashboardVersion = AspireDashboardPropertyPrefix + "Version";
    public const string DashboardBuildId = AspireDashboardPropertyPrefix + "BuildId";

    // IComponentWithTelemetry properties
    public const string DashboardComponentId = AspireDashboardPropertyPrefix + "ComponentId";
    public const string DashboardComponentType = AspireDashboardPropertyPrefix + "ComponentType";
    public const string UserAgent = AspireDashboardPropertyPrefix + "UserAgent";

    // ConsoleLogs properties
    public const string ConsoleLogsShowTimestamp = AspireDashboardPropertyPrefix + "ConsoleLogs.ShowTimestamp";
    public const string ConsoleLogsApplicationName = AspireDashboardPropertyPrefix + "ConsoleLogs.ApplicationName";

    // Metrics properties
    public const string MetricsApplicationIsReplica = AspireDashboardPropertyPrefix + "Metrics.ApplicationIsReplica";
    public const string MetricsInstrumentsCount = AspireDashboardPropertyPrefix + "Metrics.InstrumentsCount";
    public const string MetricsSelectedDuration = AspireDashboardPropertyPrefix + "Metrics.SelectedDuration";
    public const string MetricsSelectedView = AspireDashboardPropertyPrefix + "Metrics.SelectedView";

    // Exception properties
    public const string ExceptionType = AspireDashboardPropertyPrefix + "Exception.Type";
    public const string ExceptionMessage = AspireDashboardPropertyPrefix + "Exception.Message";
    public const string ExceptionStackTrace = AspireDashboardPropertyPrefix + "Exception.StackTrace";
    public const string ExceptionRuntimeVersion = AspireDashboardPropertyPrefix + "Exception.RuntimeVersion";

    // Resources properties
    public const string ResourceTypes = AspireDashboardPropertyPrefix + "Resource.Types";
    public const string ResourceType = AspireDashboardPropertyPrefix + "Resource.Type";
    public const string ResourceView = AspireDashboardPropertyPrefix + "Resource.View";

    // Error properties
    public const string ErrorRequestId = AspireDashboardPropertyPrefix + "RequestId";

    // Structured logs properties
    public const string StructuredLogsSelectedLogLevel = AspireDashboardPropertyPrefix + "StructuredLogs.SelectedLogLevel";
    public const string StructuredLogsFilterCount = AspireDashboardPropertyPrefix + "StructuredLogs.FilterCount";

    // Command properties
    public const string CommandName = AspireDashboardPropertyPrefix + "Command.Name";
}
