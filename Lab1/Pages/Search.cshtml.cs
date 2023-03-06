using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty] public String facultySearch { get; set; }
        public List<FacultySearch> FacultySearchOfficeHoursList { get; set; }
        
        public bool IsPostExecuted;
        public String warningMessage = "Sorry! No Available Office Hours / No Professor Found";
        public bool IsValidSearch = false;
        public String validSearchMessage;

        public SearchModel()
        {
            FacultySearchOfficeHoursList = new List<FacultySearch>();
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Student")
            {
                return RedirectToPage("Index");
            }
            IsValidSearch = true;
            return Page();

        }

        public IActionResult OnPost()
        {
            
            String[] parts = facultySearch.Split(' ');
            if (parts.Length != 2)
            {
                IsValidSearch = false;
                validSearchMessage = "Please Enter A Professor's First AND Last Name, Separated By A Space";
                
                return Page();
            }
            else
            {
                IsValidSearch = true;
            }
            String facFirstName = parts[0];
            String facLastName = parts[1];
            IsPostExecuted = true;
            
            SqlDataReader FacultySearchReader = DBClass.FacultySearchReader(facFirstName, facLastName);

            while (FacultySearchReader.Read())
            {
                FacultySearchOfficeHoursList.Add(new FacultySearch
                {
                    
                    date = FacultySearchReader["Date"].ToString(),
                    startTime = FacultySearchReader["StartTime"].ToString(),
                    endTime = FacultySearchReader["EndTime"].ToString(),
                    purpose = FacultySearchReader["Purpose"].ToString(),
                    facFirstName = FacultySearchReader["FacFirstName"].ToString(),
                    facLastName = FacultySearchReader["FacLastName"].ToString(),
                    roomNumber = FacultySearchReader["roomNumber"].ToString(),
                    officeHoursID = Int32.Parse(FacultySearchReader["officeHoursID"].ToString()),


                });
            }

            // Close DB Connection Remotely
            DBClass.Lab1DBConnection.Close();
            return Page();

        }
    }
}
