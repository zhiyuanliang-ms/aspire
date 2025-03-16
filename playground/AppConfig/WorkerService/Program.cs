using Aspire.Azure.Configuration.AppConfiguration;
using Microsoft.FeatureManagement;
using WorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Configuration.AddAzureAppConfiguration("aspire-appconfig", options =>
{
    options.UseFeatureFlags();
    options.ConfigureRefresh(refresh =>
    {
        refresh.RegisterAll();
    });
    builder.Services.AddSingleton(options.GetRefresher());

});
builder.Services.AddFeatureManagement();

var host = builder.Build();
host.Run();
