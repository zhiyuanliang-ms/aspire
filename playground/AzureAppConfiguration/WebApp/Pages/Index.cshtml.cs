using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly WeatherApiClient _weatherApiClient;
    private readonly IConfiguration _configuration;
    
    public string Message { get; private set; } = string.Empty;
    
    public IndexModel(WeatherApiClient weatherApiClient, IConfiguration configuration)
    {
        _weatherApiClient = weatherApiClient;
        _configuration = configuration;
    }

    public void OnGet()
    {
        Message = _configuration["Message"] ?? "No message found. Please set the 'Message' key in your configuration.";
    }

    public async Task<IActionResult> OnGetWeatherAsync()
    {
        var response = await _weatherApiClient.GetWeatherAsync();
        return Content(response, "application/json");
    }
}