using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicManagementSystem.Pages
{
    public class LogOutModel : PageModel
    {
        public IActionResult OnGet()
        {
            Response.Cookies.Append("Authorized", "No");
            return Redirect("/Index");
        }
    }
}
