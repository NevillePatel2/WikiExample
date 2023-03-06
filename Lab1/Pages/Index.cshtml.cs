using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class IndexModel : PageModel
    {
        public int currentStudentID { get; set; }
        public int currentFacultyID { get; set; }


        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                if (HttpContext.Session.GetString("role") == "Student")
                {
                    SqlDataReader currentStudentIDReader = DBClass.CurrentStudentID(HttpContext.Session.GetString("username"));
                    while (currentStudentIDReader.Read())
                    {
                        currentStudentID = Int32.Parse(currentStudentIDReader["StudentID"].ToString());
                    }
                    DBClass.Lab1DBConnection.Close();

                    HttpContext.Session.SetString("stuNotificationCount", DBClass.StudentNotificationCount(currentStudentID).ToString());
                    DBClass.Lab1DBConnection.Close();

                }
                else
                {
                    SqlDataReader currentFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
                    while (currentFacultyIDReader.Read())
                    {
                        currentFacultyID = Int32.Parse(currentFacultyIDReader["FacultyID"].ToString());
                    }
                    DBClass.Lab1DBConnection.Close();

                    HttpContext.Session.SetString("facNotificationCount", DBClass.FacultyNotificationCount(currentFacultyID).ToString());
                    DBClass.Lab1DBConnection.Close();
                }
            }


        }

        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
       
    }
}



