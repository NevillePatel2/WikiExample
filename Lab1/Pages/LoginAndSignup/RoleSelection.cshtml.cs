using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1.Pages.LoginAndSignup
{
    public class RoleSelectionModel : PageModel
    {
        [BindProperty]
        public string SelectedRole { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()

        {
            if (SelectedRole.Equals("Faculty"))
            {
                return RedirectToPage("FacultyAcctSignUp");
            }
            else
            {
                return RedirectToPage("StudentAcctSignUp");
            }


        }
    }
}
