using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class CreateMeetingModel : PageModel
    {
        [BindProperty] public Meeting NewMeeting { get; set; }
        [BindProperty] public MeetingRequest NewMeetingRequest { get; set; }
        public Notification NewNotification { get; set; } = new Notification();


        // Properties for Handling Dates and Times
        [BindProperty] public String Date { get; set; }
        [BindProperty] public String Purpose { get; set; }
        [BindProperty] public String StartTime { get; set; }

        // Properties for Single Select Dropdown
        [BindProperty] public int SelectedLocation { get; set; }
        [BindProperty] public int SelectedFaculty { get; set; }
        public int currentStudentID { get; set; }

        public List<Location> LocationList;
        public List<Faculty> FacultyList;

        public CreateMeetingModel()
        {
            LocationList = new List<Location>();
            FacultyList = new List<Faculty>();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Student")
            {
                return RedirectToPage("Index");
            }
            //OnGet method loads in data for class and location drop down menus
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
            // Close DB Connection Remotely
            DBClass.Lab1DBConnection.Close();

            SqlDataReader locationReader = DBClass.LocationReader();

            while (locationReader.Read())
            {
                LocationList.Add(new Location
                {
                    locationID = Int32.Parse(locationReader["LocationID"].ToString()),
                    roomNumber = locationReader["roomNumber"].ToString(),
                    capacity = Int32.Parse(locationReader["Capacity"].ToString()),
                });
            }

            // Close DB Connection Remotely
            DBClass.Lab1DBConnection.Close();
            return Page();

        }

        public IActionResult OnPostSingleSelect()
        {
            SqlDataReader currentStudentIDReader = DBClass.CurrentStudentID(HttpContext.Session.GetString("username"));
            while (currentStudentIDReader.Read())
            {
                currentStudentID = Int32.Parse(currentStudentIDReader["studentID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            //NewMeeting.date = Date;
            //NewMeeting.purpose = Purpose;
            //NewMeeting.startTime = StartTime;
            //NewMeeting.facultyID = SelectedFaculty;
            //NewMeeting.locationID = SelectedLocation;
            //NewMeeting.studentID = currentStudentID;

            //DBClass.CreateMeeting(NewMeeting);
            //DBClass.Lab1DBConnection.Close();

            NewMeetingRequest.date = Date;
            NewMeetingRequest.purpose = Purpose;
            NewMeetingRequest.startTime = StartTime;
            NewMeetingRequest.facultyID = SelectedFaculty;
            NewMeetingRequest.locationID = SelectedLocation;
            NewMeetingRequest.studentID = currentStudentID;

            

            DBClass.CreateMeetingRequest(NewMeetingRequest);
            DBClass.Lab1DBConnection.Close();

            NewNotification.Message = "1-on-1 Meeting Request: I would like to meet with you on " + Date + " at " + StartTime
                + ". The purpose of the visit is: " + Purpose;
            NewNotification.Sender = "Student";
            NewNotification.StudentID = currentStudentID;
            NewNotification.FacultyID = SelectedFaculty;
            NewNotification.Timestamp = DateTime.Now.ToString();
            DBClass.CreateNotification(NewNotification);
            DBClass.Lab1DBConnection.Close();


            return RedirectToPage("/UpcomingAppointments");
        }
    }
}
