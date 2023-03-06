using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages
{
    public class DataDashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Faculty")
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
