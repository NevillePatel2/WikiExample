using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class FacultyNotificationsModel : PageModel
    {
        public List<SpecificNotifications> SpecificNotificationsList;
        [BindProperty] public int StudentID { get; set; }
        public int currentFacultyID { get; set; }
        [BindProperty] public int NotificationID { get; set; }
        public Notification NewNotification { get; set; } = new Notification();

        public FacultyNotificationsModel()
        {
            SpecificNotificationsList = new List<SpecificNotifications>();
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
                currentFacultyID = Int32.Parse(currentFacultyIDReader["FacultyID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            HttpContext.Session.SetString("facNotificationCount", DBClass.FacultyNotificationCount(currentFacultyID).ToString());
            DBClass.Lab1DBConnection.Close();

            SqlDataReader NotificationReader = DBClass.FacultyNotificationReader(currentFacultyID);
            while (NotificationReader.Read())
            {
                SpecificNotificationsList.Add(new SpecificNotifications
                {
                    NotificationID = Int32.Parse(NotificationReader["NotificationID"].ToString()),
                    FacultyID = Int32.Parse(NotificationReader["FacultyID"].ToString()),
                    StudentID = Int32.Parse(NotificationReader["StudentID"].ToString()),
                    Sender = NotificationReader["Sender"].ToString(),
                    StuFirstName = NotificationReader["StuFirstName"].ToString(),
                    StuLastName = NotificationReader["StuLastName"].ToString(),
                    Message = NotificationReader["Message"].ToString(),
                    Timestamp = NotificationReader["Timestamp"].ToString()

                }); ;
            }

            DBClass.Lab1DBConnection.Close();
            return Page();
        }

        public IActionResult OnPost()
        {
            DBClass.DeleteNotification(NotificationID);
            DBClass.Lab1DBConnection.Close();

            SqlDataReader currentFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
            while (currentFacultyIDReader.Read())
            {
                currentFacultyID = Int32.Parse(currentFacultyIDReader["FacultyID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            HttpContext.Session.SetInt32("currenFacultyID", currentFacultyID);
            HttpContext.Session.SetString("facNotificationCount", DBClass.FacultyNotificationCount(currentFacultyID).ToString());
            DBClass.Lab1DBConnection.Close();

            SqlDataReader NotificationReader = DBClass.FacultyNotificationReader(currentFacultyID);
            while (NotificationReader.Read())
            {
                SpecificNotificationsList.Add(new SpecificNotifications
                {
                    NotificationID = Int32.Parse(NotificationReader["NotificationID"].ToString()),
                    StudentID = Int32.Parse(NotificationReader["StudentID"].ToString()),
                    Sender = NotificationReader["Sender"].ToString(),
                    FacultyID = Int32.Parse(NotificationReader["FacultyID"].ToString()),
                    StuFirstName = NotificationReader["StuFirstName"].ToString(),
                    StuLastName = NotificationReader["StuLastName"].ToString(),
                    Message = NotificationReader["Message"].ToString(),
                    Timestamp = NotificationReader["Timestamp"].ToString()

                }); 
            }

            DBClass.Lab1DBConnection.Close();
            return Page();
        }

        public IActionResult OnPostAcceptMeeting()
        {
            //need to figure out how to get the associated meetingrequestID to get the right values
            //open a specific meetingrequest reader then assign the values from that to the actual meeting
            //create meeting meeting, notify student of acceptance, delete meeting request and meeting request notification
            //redirect to upcoming office hours and display the 1-on-1 meetings
            return RedirectToPage("/UpcomingOfficeHours");
        }

        public IActionResult OnPostDeclineMeeting()
        {
            SqlDataReader currentFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
            while (currentFacultyIDReader.Read())
            {
                currentFacultyID = Int32.Parse(currentFacultyIDReader["FacultyID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            HttpContext.Session.SetInt32("currenFacultyID", currentFacultyID);
            HttpContext.Session.SetString("facNotificationCount", DBClass.FacultyNotificationCount(currentFacultyID).ToString());
            DBClass.Lab1DBConnection.Close();

            SqlDataReader NotificationReader = DBClass.FacultyNotificationReader(currentFacultyID);
            while (NotificationReader.Read())
            {
                SpecificNotificationsList.Add(new SpecificNotifications
                {
                    NotificationID = Int32.Parse(NotificationReader["NotificationID"].ToString()),
                    StudentID = Int32.Parse(NotificationReader["StudentID"].ToString()),
                    Sender = NotificationReader["Sender"].ToString(),
                    FacultyID = Int32.Parse(NotificationReader["FacultyID"].ToString()),
                    StuFirstName = NotificationReader["StuFirstName"].ToString(),
                    StuLastName = NotificationReader["StuLastName"].ToString(),
                    Message = NotificationReader["Message"].ToString(),
                    Timestamp = NotificationReader["Timestamp"].ToString()

                });

            }

            DBClass.Lab1DBConnection.Close();

            NewNotification.Timestamp = DateTime.Now.ToString();
            NewNotification.StudentID = StudentID;
            NewNotification.Message = "Unfortunately your 1-on-1 meeting request has been declined. Please signup " +
                "for an open office hours slot or request a meeting for a different time";
            NewNotification.Sender = "Faculty";
            NewNotification.FacultyID = currentFacultyID;
            DBClass.CreateNotification(NewNotification);
            DBClass.Lab1DBConnection.Close();

            DBClass.DeleteNotification(NotificationID);
            DBClass.Lab1DBConnection.Close();

            HttpContext.Session.SetString("facNotificationCount", DBClass.FacultyNotificationCount(currentFacultyID).ToString());
            DBClass.Lab1DBConnection.Close();



            //when clicking decline meeting it updates the noti counter correctly but doesnt remove the noti from the screen
            //also need to delete the MeetingRequestID
            SqlDataReader newFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
            while (newFacultyIDReader.Read())
            {
                currentFacultyID = Int32.Parse(newFacultyIDReader["FacultyID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            SqlDataReader newNotificationReader = DBClass.FacultyNotificationReader(currentFacultyID);
            while (newNotificationReader.Read())
            {
                SpecificNotificationsList.Add(new SpecificNotifications
                {
                    NotificationID = Int32.Parse(newNotificationReader["NotificationID"].ToString()),
                    StudentID = Int32.Parse(newNotificationReader["StudentID"].ToString()),
                    Sender = newNotificationReader["Sender"].ToString(),
                    FacultyID = Int32.Parse(newNotificationReader["FacultyID"].ToString()),
                    StuFirstName = newNotificationReader["StuFirstName"].ToString(),
                    StuLastName = newNotificationReader["StuLastName"].ToString(),
                    Message = newNotificationReader["Message"].ToString(),
                    Timestamp = newNotificationReader["Timestamp"].ToString()

                });

            }
            DBClass.Lab1DBConnection.Close();
            return Page();
        }
       
    }
}
