using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class StudentSignUpModel : PageModel
    {
        public Queue NewQueue { get; set; }
        public int currentStudentID { get; set; }
        public List<SelectedOfficeHours> SelectedOfficeHoursList;

        public int selectedOfficeHoursID { get; set; }
        public bool isValid;
        public String errorMessage;
        public int queueCount { get; set; }
        public StudentSignUpModel()
        {
           SelectedOfficeHoursList = new List<SelectedOfficeHours>();

        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Student")
            {
                return RedirectToPage("Index");
            }
            isValid = true;
            selectedOfficeHoursID = Int32.Parse(Request.Query["officehoursid"]);

            SqlDataReader SelectedOfficeHoursReader = DBClass.SelectedOfficeHoursReader(selectedOfficeHoursID);

            while (SelectedOfficeHoursReader.Read())
            {
                SelectedOfficeHoursList.Add(new SelectedOfficeHours
                {
                    officeHoursID = Int32.Parse(SelectedOfficeHoursReader["OfficeHoursID"].ToString()),
                    date = SelectedOfficeHoursReader["Date"].ToString(),
                    startTime = SelectedOfficeHoursReader["StartTime"].ToString(),
                    endTime = SelectedOfficeHoursReader["EndTime"].ToString(),
                    purpose = SelectedOfficeHoursReader["Purpose"].ToString(),
                    facFirstName = SelectedOfficeHoursReader["FacFirstName"].ToString(),
                    facLastName = SelectedOfficeHoursReader["FacLastName"].ToString(),
                    roomNumber = SelectedOfficeHoursReader["roomNumber"].ToString()


                });
            }

            // Close DB Connection Remotely
            DBClass.Lab1DBConnection.Close();

            queueCount = DBClass.QueueCount(selectedOfficeHoursID);
            DBClass.Lab1DBConnection.Close();
            return Page();

        }
        public IActionResult OnPost()
        {
            
            selectedOfficeHoursID = Int32.Parse(Request.Query["officehoursid"]);
            SqlDataReader currentStudentIDReader = DBClass.CurrentStudentID(HttpContext.Session.GetString("username"));
            while (currentStudentIDReader.Read())
            {
                currentStudentID = Int32.Parse(currentStudentIDReader["studentID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();
            if (DBClass.QueueValidation(currentStudentID, selectedOfficeHoursID) == 0)
            {
                isValid = true;
                DBClass.Lab1DBConnection.Close();
                DBClass.CreateQueue(selectedOfficeHoursID, currentStudentID);
                DBClass.Lab1DBConnection.Close();
                
                return RedirectToPage("UpcomingAppointments");
            }
            else
            {
                DBClass.Lab1DBConnection.Close();
                isValid = false;
                errorMessage = "You Cannot Signup For The Same Office Hours Slot More Than Once!";
                selectedOfficeHoursID = Int32.Parse(Request.Query["officehoursid"]);

                SqlDataReader SelectedOfficeHoursReader = DBClass.SelectedOfficeHoursReader(selectedOfficeHoursID);

                while (SelectedOfficeHoursReader.Read())
                {
                    SelectedOfficeHoursList.Add(new SelectedOfficeHours
                    {
                        officeHoursID = Int32.Parse(SelectedOfficeHoursReader["OfficeHoursID"].ToString()),
                        date = SelectedOfficeHoursReader["Date"].ToString(),
                        startTime = SelectedOfficeHoursReader["StartTime"].ToString(),
                        endTime = SelectedOfficeHoursReader["EndTime"].ToString(),
                        purpose = SelectedOfficeHoursReader["Purpose"].ToString(),
                        facFirstName = SelectedOfficeHoursReader["FacFirstName"].ToString(),
                        facLastName = SelectedOfficeHoursReader["FacLastName"].ToString(),
                        roomNumber = SelectedOfficeHoursReader["roomNumber"].ToString()


                    });
                }

                // Close DB Connection Remotely
                DBClass.Lab1DBConnection.Close();

                queueCount = DBClass.QueueCount(selectedOfficeHoursID);
                DBClass.Lab1DBConnection.Close();
                return Page();
            }
            

            
        }
    }
}
