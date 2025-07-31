using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// API from package "Aspire.Microsoft.Extensions.Configuration.AzureAppConfiguration"
builder.AddAzureAppConfiguration("config-dev", // Use the service discovery alias to add Azure App Configuration
    configureOptions: options =>
    {
        options.UseFeatureFlags();
        options.ConfigureRefresh(refreshOptions =>
        {
            refreshOptions.RegisterAll();
            refreshOptions.SetRefreshInterval(TimeSpan.FromSeconds(5));
        });
    });

builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement();

builder.Services.AddRazorPages();
builder.Services.AddHttpClient<WeatherApiClient>(client =>
{
    client.BaseAddress = new Uri("https+http://weatherapi"); // Use the service discovery alias for WeatherApi
});

var app = builder.Build();

app.UseAzureAppConfiguration();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
