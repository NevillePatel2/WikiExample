using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.LoginAndSignup
{
    public class FacultyLoginModel : PageModel
    {
        [BindProperty]
        public String Username { get; set; }

        [BindProperty]
        public String Password { get; set; }

        public String LoginMessage { get; set; }
        public void OnGet()
        {
            
            
        }
        public IActionResult OnPost()
        {
            if (DBClass.HashedFacParameterLogin(Username, Password))
            {
                HttpContext.Session.SetString("username", Username);
                HttpContext.Session.SetString("role", "Faculty");
                ViewData["LoginMessage"] = "Login Successful!";
                DBClass.AuthConnection.Close();
                return RedirectToPage("/Index");
               

            }
            else
            {
                ViewData["LoginMessage"] = "Username and/or Password Incorrect";

            }

            DBClass.AuthConnection.Close();

            return Page();
        }
    }
}
