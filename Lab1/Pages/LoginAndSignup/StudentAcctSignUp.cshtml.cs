using Lab1.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.DB;
namespace Lab1.Pages.LoginAndSignup
{
    public class StudentAcctSignUpModel : PageModel
    {
        [BindProperty]
        public Student NewStudentAcct { get; set; }

        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]

        public string PhoneNumber { get; set; }
        [BindProperty]
        public String? Major { get; set; }

       
        public void OnGet()
        {
        }

        public IActionResult OnPost(string button)
        {
            if (button == "clear")
            {
                ModelState.Clear();
                FirstName = "";
                LastName = "";
                Username = "";
                Password = "";
                Email = "";
                PhoneNumber = "";
                Major = "";
            }
            else if (button == "populate")
            {
                ModelState.Clear();
                FirstName = "Bob";
                LastName = "Smith";
                Username = "SmithB";
                Password = "1234";
                Email = "BobSmith@gmail.com";
                PhoneNumber = "1234567890";
                Major = "CIS";
            }
            else if (button == "submit")
            {
                NewStudentAcct.stuFirstName = FirstName;
                NewStudentAcct.stuLastName = LastName;
                NewStudentAcct.stuUsername = Username;
                
                NewStudentAcct.stuEmail = Email;
                NewStudentAcct.stuPhone = PhoneNumber;
                NewStudentAcct.stuMajor = Major;

                DBClass.CreateStudentAccount(NewStudentAcct);
                DBClass.CreateHashedStudent(Username, Password);
                return RedirectToPage("StudentLogin");
            }
            return Page();

        }


    }
}
