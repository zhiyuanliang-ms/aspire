var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var app = builder.Build();

string[] summaries = [ "Sunny", "Cloudy", "Rainy"];
var random = new Random();

app.MapGet("/weather", () =>
{
    var summary = summaries[random.Next(summaries.Length)];
    var temperature = random.Next(25, 36); // Random temp 25-35°C
    return Results.Json(new { summary, temperature });
});

app.Run();