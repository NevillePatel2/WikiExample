using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class ManageMeetingModel : PageModel
    {
        public List<SpecificMeetings> SpecificMeetingsList { get; set; }
        public int selectedMeetingID { get; set; }
        public int currentStudentID { get; set; }

        public ManageMeetingModel()
        {
            SpecificMeetingsList = new List<SpecificMeetings>();
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Student")
            {
                return RedirectToPage("Index");
            }
            selectedMeetingID = Int32.Parse(Request.Query["meetingid"]);
            TempData["meetingID"] = selectedMeetingID;

            SqlDataReader SingleMeetingReader = DBClass.SingleMeetingReader(selectedMeetingID);

            while (SingleMeetingReader.Read())
            {
                SpecificMeetingsList.Add(new SpecificMeetings
                {
                    meetingID = Int32.Parse(SingleMeetingReader["MeetingID"].ToString()),
                    date = SingleMeetingReader["Date"].ToString(),
                    startTime = SingleMeetingReader["StartTime"].ToString(),
                    purpose = SingleMeetingReader["Purpose"].ToString(),
                    facFirstName = SingleMeetingReader["FacFirstName"].ToString(),
                    facLastName = SingleMeetingReader["FacLastName"].ToString(),
                    roomNumber = SingleMeetingReader["roomNumber"].ToString()
                });

            }
            DBClass.Lab1DBConnection.Close();
            return Page();
        }

        public IActionResult OnPostCancelMeeting()
        {
            SqlDataReader currentStudentIDReader = DBClass.CurrentStudentID(HttpContext.Session.GetString("username"));
            while (currentStudentIDReader.Read())
            {
                currentStudentID = Int32.Parse(currentStudentIDReader["StudentID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            if (TempData.ContainsKey("meetingID"))
            {
                selectedMeetingID = (int)TempData["meetingID"];
            }
            DBClass.CancelMeeting(selectedMeetingID, currentStudentID);
            DBClass.Lab1DBConnection.Close();
            return RedirectToPage("UpcomingAppointments");
        }
    }
}
