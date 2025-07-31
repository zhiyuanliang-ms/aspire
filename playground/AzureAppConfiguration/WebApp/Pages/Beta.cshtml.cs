using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement.Mvc;

[FeatureGate("Beta")]
public class BetaModel : PageModel
{
    public void OnGet()
    {
    }
}
