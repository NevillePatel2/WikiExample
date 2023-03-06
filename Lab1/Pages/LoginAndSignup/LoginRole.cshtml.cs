using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.LoginAndSignup
{
    public class LoginRoleModel : PageModel
    {
        [BindProperty]
        public String SelectedRole { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()

        {
            if (SelectedRole.Equals("Faculty"))
            {
                return RedirectToPage("FacultyLogin");
            }
            else
            {
                return RedirectToPage("StudentLogin");
            }


        }
    }
}
