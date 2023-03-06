using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Text;

namespace Lab1.Pages
{
    public class StudentNotificationsModel : PageModel
    {
        public List<SpecificNotifications> SpecificNotificationsList;
        public int currentStudentID { get; set; }
        [BindProperty] public int NotificationID { get; set; }

        public StudentNotificationsModel()
        {
            SpecificNotificationsList = new List<SpecificNotifications>();
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

            HttpContext.Session.SetString("stuNotificationCount", DBClass.StudentNotificationCount(currentStudentID).ToString());

            DBClass.Lab1DBConnection.Close();



            SqlDataReader NotificationReader = DBClass.StudentNotificationReader(currentStudentID);
            while (NotificationReader.Read())
            {
                SpecificNotificationsList.Add(new SpecificNotifications
                {
                    NotificationID = Int32.Parse(NotificationReader["NotificationID"].ToString()),
                    StudentID = Int32.Parse(NotificationReader["StudentID"].ToString()),
                    Sender = NotificationReader["Sender"].ToString(),
                    FacFirstName = NotificationReader["FacFirstName"].ToString(),
                    FacLastName = NotificationReader["FacLastName"].ToString(),
                    Message = NotificationReader["Message"].ToString(),
                    Timestamp = NotificationReader["Timestamp"].ToString()

                });
            }

            DBClass.Lab1DBConnection.Close();
            return Page();
        }

        public IActionResult OnPost()
        {
            DBClass.DeleteNotification(NotificationID);
            DBClass.Lab1DBConnection.Close();
            SqlDataReader currentStudentIDReader = DBClass.CurrentStudentID(HttpContext.Session.GetString("username"));
            while (currentStudentIDReader.Read())
            {
                currentStudentID = Int32.Parse(currentStudentIDReader["StudentID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            HttpContext.Session.SetString("stuNotificationCount", DBClass.StudentNotificationCount(currentStudentID).ToString());
            DBClass.Lab1DBConnection.Close();
            

            SqlDataReader NotificationReader = DBClass.StudentNotificationReader(currentStudentID);
            while (NotificationReader.Read())
            {
                SpecificNotificationsList.Add(new SpecificNotifications
                {
                    NotificationID = Int32.Parse(NotificationReader["NotificationID"].ToString()),
                    StudentID = Int32.Parse(NotificationReader["StudentID"].ToString()),
                    Sender = NotificationReader["Sender"].ToString(),
                    FacFirstName = NotificationReader["FacFirstName"].ToString(),
                    FacLastName = NotificationReader["FacLastName"].ToString(),
                    Message = NotificationReader["Message"].ToString(),
                    Timestamp = NotificationReader["Timestamp"].ToString()

                }); ;
            }

            DBClass.Lab1DBConnection.Close();
            return Page();
        }
    }
}
