// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

var builder = DistributedApplication.CreateBuilder(args);

var appConfig = builder
    .AddAzureAppConfiguration("aspire-appconfig")
    .RunAsEmulator();

builder.AddProject<Projects.WorkerService>("workerservice")
    .WithReference(appConfig);

builder.Build().Run();
