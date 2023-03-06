using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class ViewMeetingsModel : PageModel
    {
        public List<SpecificOfficeHours> SpecificOfficeHoursList;
        
        public int currentFacultyID { get; set; }

        public ViewMeetingsModel() 
        {
            SpecificOfficeHoursList = new List<SpecificOfficeHours>();
            
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Faculty")
            {
                return RedirectToPage("Index");
            }
            SqlDataReader currentFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
            while (currentFacultyIDReader.Read())
            {
                currentFacultyID = Int32.Parse(currentFacultyIDReader["facultyID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            SqlDataReader SpecificOfficeHoursReader = DBClass.SpecificOfficeHoursReader(currentFacultyID);

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


                });
            }

            // Close DB Connection Remotely
            DBClass.Lab1DBConnection.Close();
            return Page();

        }
    }
}
