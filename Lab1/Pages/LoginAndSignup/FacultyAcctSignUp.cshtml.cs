using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class UserSignUpModel : PageModel
    {
        [BindProperty]
        public Faculty NewFacultyAcct { get; set; }

        [BindProperty]
        public Location NewLocation { get; set; }
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
        public String OfficeLocation { get; set; }
        public int newLocationID { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(String button)
        {
            if (button == "clear")
            {
                ModelState.Clear();
                FirstName = "";
                LastName = "";
                Username = "";
                Password = "";
                Email = "";
                OfficeLocation = "";
            }
            else if (button == "populate")
            {
                ModelState.Clear();
                FirstName = "Carey";
                LastName = "Cole";
                Username = "CCole";
                Password = "Pass123";
                Email = "CareyCole@dukes.jmu";
                OfficeLocation = "Hartman 5023";
            }
            else if (button == "submit")
            {
                NewLocation.roomNumber = OfficeLocation;
                NewLocation.capacity = 32;
                DBClass.CreateLocation(NewLocation);
                SqlDataReader LocationIDReader = DBClass.LocationIDReader(OfficeLocation);
                while (LocationIDReader.Read())
                {
                    newLocationID = Int32.Parse(LocationIDReader["LocationID"].ToString());
                }
                DBClass.Lab1DBConnection.Close();
                NewFacultyAcct.facFirstName = FirstName;
                NewFacultyAcct.facLastName = LastName;
                NewFacultyAcct.facEmail = Email;
                NewFacultyAcct.facUsername = Username;
                
                NewFacultyAcct.locationID = newLocationID;
                DBClass.CreateFacultyAccount(NewFacultyAcct);
                DBClass.CreateHashedFaculty(Username, Password);

                return RedirectToPage("FacultyLogin");
            }
            return Page();
        }
    }
}
