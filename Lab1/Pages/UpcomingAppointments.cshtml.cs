using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class UpcomingAppointmentsModel : PageModel
    {
        public List<SpecificMeetings> SpecificMeetingsList;
        public List<SpecificQueues> SpecificQueuesList;
        public int currentStudentID { get; set; }

        public UpcomingAppointmentsModel()
        {
            SpecificMeetingsList = new List<SpecificMeetings>();
            SpecificQueuesList = new List<SpecificQueues>();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Student")
            {
                return RedirectToPage("Index");
            }
            SqlDataReader currentStudentIDReader = DBClass.CurrentStudentID(HttpContext.Session.GetString("username"));
            while (currentStudentIDReader.Read())
            {
                currentStudentID = Int32.Parse(currentStudentIDReader["StudentID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            SqlDataReader SpecificMeetingReader = DBClass.SpecificMeetingReader(currentStudentID);

            while (SpecificMeetingReader.Read())
            {
                SpecificMeetingsList.Add(new SpecificMeetings
                {
                    meetingID = Int32.Parse(SpecificMeetingReader["MeetingID"].ToString()),
                    date = SpecificMeetingReader["date"].ToString(),
                    startTime = SpecificMeetingReader["StartTime"].ToString(),
                    purpose = SpecificMeetingReader["Purpose"].ToString(),
                    facFirstName = SpecificMeetingReader["FacFirstName"].ToString(),
                    facLastName = SpecificMeetingReader["FacLastName"].ToString(),
                    roomNumber = SpecificMeetingReader["roomNumber"].ToString()


                });
                
            }

            // Close DB Connection Remotely
            DBClass.Lab1DBConnection.Close();

            SqlDataReader SpecificQueueReader = DBClass.SpecificQueueReader(currentStudentID);

            while (SpecificQueueReader.Read())
            {
                SpecificQueuesList.Add(new SpecificQueues
                {
                    officeHoursID = Int32.Parse(SpecificQueueReader["officeHoursID"].ToString()),
                    queueID = Int32.Parse(SpecificQueueReader["QueueID"].ToString()),
                    date = SpecificQueueReader["date"].ToString(),
                    startTime = SpecificQueueReader["StartTime"].ToString(),
                    endTime = SpecificQueueReader["EndTime"].ToString(),
                    purpose = SpecificQueueReader["Purpose"].ToString(),
                    facFirstName = SpecificQueueReader["FacFirstName"].ToString(),
                    facLastName = SpecificQueueReader["FacLastName"].ToString(),
                    roomNumber = SpecificQueueReader["roomNumber"].ToString()
                });
            }

            // Close DB Connection Remotely
            DBClass.Lab1DBConnection.Close();
            return Page();
        }
    }
}
