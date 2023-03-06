using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class ViewQueueModel : PageModel
    {
        public List<SelectedOfficeHours> SelectedOfficeHoursList;
        public int selectedOfficeHoursID { get; set; }
        public int queueCount { get; set; }
        public int currentStudentID { get; set; }
        public int facultyID { get; set; }
        public Notification NewNotification { get; set; }
        public int queuePosition { get; set; }

        public ViewQueueModel()
        {
            SelectedOfficeHoursList = new List<SelectedOfficeHours>();
            NewNotification = new Notification();

        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Student")
            {
                return RedirectToPage("Index");
            }
            selectedOfficeHoursID = Int32.Parse(Request.Query["officehoursid"]);
            TempData["selectedOfficeHoursID"] = selectedOfficeHoursID;

            SqlDataReader currentStudentIDReader = DBClass.CurrentStudentID(HttpContext.Session.GetString("username"));
            while (currentStudentIDReader.Read())
            {
                currentStudentID = Int32.Parse(currentStudentIDReader["StudentID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            
            DBClass.Lab1DBConnection.Close();
            SqlDataReader SelectedOfficeHoursReader = DBClass.SelectedOfficeHoursReader(selectedOfficeHoursID);

            while (SelectedOfficeHoursReader.Read())
            {
                SelectedOfficeHoursList.Add(new SelectedOfficeHours
                {
                    officeHoursID = Int32.Parse(SelectedOfficeHoursReader["OfficeHoursID"].ToString()),
                    facultyID = Int32.Parse(SelectedOfficeHoursReader["FacultyID"].ToString()),
                    date = SelectedOfficeHoursReader["Date"].ToString(),
                    startTime = SelectedOfficeHoursReader["StartTime"].ToString(),
                    endTime = SelectedOfficeHoursReader["EndTime"].ToString(),
                    purpose = SelectedOfficeHoursReader["Purpose"].ToString(),
                    facFirstName = SelectedOfficeHoursReader["FacFirstName"].ToString(),
                    facLastName = SelectedOfficeHoursReader["FacLastName"].ToString(),
                    roomNumber = SelectedOfficeHoursReader["roomNumber"].ToString()
                    


            });
                facultyID = Int32.Parse(SelectedOfficeHoursReader["FacultyID"].ToString());
                HttpContext.Session.SetInt32("facultyID", facultyID);

            }
            // Close DB Connection Remotely
            DBClass.Lab1DBConnection.Close();
            queueCount = DBClass.QueueCount(selectedOfficeHoursID);



            DBClass.Lab1DBConnection.Close();

            queuePosition = DBClass.QueuePosition(selectedOfficeHoursID, currentStudentID) + 1;
            DBClass.Lab1DBConnection.Close();


            return Page();
        }

        public IActionResult OnPostLeaveQueue()
        {
            SqlDataReader currentStudentIDReader = DBClass.CurrentStudentID(HttpContext.Session.GetString("username"));
            while (currentStudentIDReader.Read())
            {
                currentStudentID = Int32.Parse(currentStudentIDReader["StudentID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();
            
            if (TempData.ContainsKey("selectedOfficeHoursID"))
            {
                selectedOfficeHoursID = (int)TempData["selectedOfficeHoursID"];
            }
            DBClass.LeaveQueue(selectedOfficeHoursID, currentStudentID);
            DBClass.Lab1DBConnection.Close();

            

            NewNotification.Message = "I have left the queue!";
            NewNotification.Timestamp = DateTime.Now.ToString();
            NewNotification.Sender = "Student";
            NewNotification.StudentID = currentStudentID;
            NewNotification.FacultyID = (int)HttpContext.Session.GetInt32("facultyID");
            DBClass.CreateNotification(NewNotification);
            return RedirectToPage("UpcomingAppointments");
        }
    }
}
