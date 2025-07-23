// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

var builder = DistributedApplication.CreateBuilder(args);

// API from package "Aspire.Hosting.Azure.AppConfiguration"
var appConfig = builder.AddAzureAppConfiguration("config-dev")
    //.RunAsExisting("Aspire-Demo-LZY", "Dev");
    .RunAsEmulator(emulator =>
    {
        emulator.WithDataBindMount();
    });

var weatherApi = builder.AddProject<Projects.WeatherApi>("weatherapi");
var _ = builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(appConfig)
    .WithReference(weatherApi); // Reference different components

builder.Build().Run();
