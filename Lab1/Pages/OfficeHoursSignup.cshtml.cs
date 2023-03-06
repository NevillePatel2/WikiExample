using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class OfficeHoursSignupModel : PageModel
    {
        public List<Faculty> FacultyList { get; set; }
        
        // Properties for Single Select Dropdown
        [BindProperty] public int SelectedNumber { get; set; }
        public String SelectMessage { get; set; }
        public List<OfficeHours> OfficeHoursList;
        public List<SpecificOfficeHours> SpecificOfficeHoursList;
        public bool IsPostExecuted;
        public String warningMessage = "Sorry, There Are No Available Office Hours For the Selected Professor";

        public OfficeHoursSignupModel()
        {
            FacultyList = new List<Faculty>();
            OfficeHoursList = new List<OfficeHours>();
            SpecificOfficeHoursList = new List<SpecificOfficeHours>();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Student")
            {
                return RedirectToPage("Index");
            }
            IsPostExecuted = false;
            SqlDataReader FacultyReader = DBClass.FacultyReader();

            while (FacultyReader.Read())
            {
                FacultyList.Add(new Faculty
                {
                    facultyID = Int32.Parse(FacultyReader["FacultyID"].ToString()),
                    facFirstName = FacultyReader["FacFirstName"].ToString(),
                    facLastName = FacultyReader["FacLastName"].ToString(),                     
                });
            }
            DBClass.Lab1DBConnection.Close();
            return Page();


        }

        public IActionResult OnPostSingleSelect()
        {
            IsPostExecuted = true;
                SqlDataReader SpecificOfficeHoursReader = DBClass.SpecificOfficeHoursReader(SelectedNumber);

                while (SpecificOfficeHoursReader.Read())
                {
                SpecificOfficeHoursList.Add(new SpecificOfficeHours
                {
                    officeHoursID = Int32.Parse(SpecificOfficeHoursReader["OfficeHoursID"].ToString()),
                    date = SpecificOfficeHoursReader["Date"].ToString(),
                    startTime = SpecificOfficeHoursReader["StartTime"].ToString(),
                    endTime = SpecificOfficeHoursReader["EndTime"].ToString(),
                    purpose = SpecificOfficeHoursReader["Purpose"].ToString(),
                    facFirstName = SpecificOfficeHoursReader["FacFirstName"].ToString(),
                    facLastName = SpecificOfficeHoursReader["FacLastName"].ToString(),
                    roomNumber = SpecificOfficeHoursReader["roomNumber"].ToString()


                }) ;
                }

                // Close DB Connection Remotely
                DBClass.Lab1DBConnection.Close();

                SqlDataReader FacultyReader = DBClass.FacultyReader();

                while (FacultyReader.Read())
                {
                    FacultyList.Add(new Faculty
                    {
                        facultyID = Int32.Parse(FacultyReader["FacultyID"].ToString()),
                        facFirstName = FacultyReader["FacFirstName"].ToString(),
                        facLastName = FacultyReader["FacLastName"].ToString(),

                    });
                }
                DBClass.Lab1DBConnection.Close();

                return Page();
            
            
        }
    }
}
