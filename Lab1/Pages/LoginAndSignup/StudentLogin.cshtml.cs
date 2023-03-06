using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Lab1.Pages.LoginAndSignup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Lab1.Pages
{
    public class UserLoginModel : PageModel
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
            if (DBClass.HashedStuParameterLogin(Username, Password))
            {
                HttpContext.Session.SetString("username", Username);
                HttpContext.Session.SetString("role", "Student");
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
