using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages
{
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Role { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty] public string Major { get; set; }

        [BindProperty] public Student EditStu { get; set; }

        [BindProperty] public Faculty EditFac { get; set; }
        public StudentAuth EditAuthStudent { get; set; } = new StudentAuth();
        public FacultyAuth EditAuthFaculty { get; set; } = new FacultyAuth();
        public int currentAuthUserID { get; set; }
        public int currentUserID { get; set; }
        public String saveMessage;
        public bool isPostExecuted = false;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") == null)
            {
                return RedirectToPage("Index");
            }
            if (HttpContext.Session.GetString("role").Equals("Student"))
            {
                SqlDataReader CurrentStudent = DBClass.CurrentStudent(HttpContext.Session.GetString("username"));
                while (CurrentStudent.Read())
                {
                    FirstName = CurrentStudent["StuFirstName"].ToString();
                    LastName = CurrentStudent["StuLastName"].ToString();
                    Username = CurrentStudent["StuUsername"].ToString();

                    Email = CurrentStudent["StuEmail"].ToString();
                    Major = CurrentStudent["StuMajor"].ToString();
                    PhoneNumber = CurrentStudent["StuPhone"].ToString();
                }
                DBClass.Lab1DBConnection.Close();

            }
            else if (HttpContext.Session.GetString("role").Equals("Faculty"))
            {
                SqlDataReader CurrentFaculty = DBClass.CurrentFaculty(HttpContext.Session.GetString("username"));
                while (CurrentFaculty.Read())
                {
                    FirstName = CurrentFaculty["FacFirstName"].ToString();
                    LastName = CurrentFaculty["FacLastName"].ToString();
                    Username = CurrentFaculty["FacUsername"].ToString();

                    Email = CurrentFaculty["FacEmail"].ToString();
                }
                DBClass.Lab1DBConnection.Close();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("role").Equals("Student"))
            {
                SqlDataReader currentAuthStudentIDReader = DBClass.CurrentAuthStudentID(HttpContext.Session.GetString("username"));
                while (currentAuthStudentIDReader.Read())
                {
                    currentAuthUserID = Int32.Parse(currentAuthStudentIDReader["StuHashID"].ToString());
                }
                DBClass.AuthConnection.Close();
                EditAuthStudent.StuAuthID = currentAuthUserID;
                EditAuthStudent.stuAuthUsername = Username;
                EditAuthStudent.stuAuthPassword = Password;
                DBClass.EditStuAuth(EditAuthStudent, currentAuthUserID);
                DBClass.AuthConnection.Close();

                EditStu.stuFirstName = FirstName;
                EditStu.stuLastName = LastName;
                EditStu.stuUsername = Username;


                EditStu.stuEmail = Email;
                EditStu.stuMajor = Major;
                EditStu.stuPhone = PhoneNumber;
                SqlDataReader currentStudentIDReader = DBClass.CurrentStudentID(HttpContext.Session.GetString("username"));
                while (currentStudentIDReader.Read())
                {
                    currentUserID = Int32.Parse(currentStudentIDReader["StudentID"].ToString());
                }
                DBClass.Lab1DBConnection.Close();
                DBClass.EditStudent(EditStu, currentUserID);
                HttpContext.Session.SetString("username", EditStu.stuUsername);

                isPostExecuted = true;
                saveMessage = "Changes Saved Successfully!";
                return Page();

            }
            else if (HttpContext.Session.GetString("role").Equals("Faculty"))
            {
                SqlDataReader currentAuthFacultyIDReader = DBClass.CurrentAuthFacultyID(HttpContext.Session.GetString("username"));
                while (currentAuthFacultyIDReader.Read())
                {
                    currentAuthUserID = Int32.Parse(currentAuthFacultyIDReader["FacHashID"].ToString());
                }
                DBClass.AuthConnection.Close();
                EditAuthFaculty.facAuthID = currentAuthUserID;
                EditAuthFaculty.facAuthUsername = Username;
                EditAuthFaculty.facAuthPassword = Password;
                DBClass.EditFacAuth(EditAuthFaculty, currentAuthUserID);
                DBClass.AuthConnection.Close();

                EditFac.facFirstName = FirstName;
                EditFac.facLastName = LastName;
                EditFac.facUsername = Username;
                EditFac.facEmail = Email;
                SqlDataReader currentFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
                while (currentFacultyIDReader.Read())
                {
                    currentUserID = Int32.Parse(currentFacultyIDReader["FacultyID"].ToString());
                }
                DBClass.Lab1DBConnection.Close();
                DBClass.EditFaculty(EditFac, currentUserID);
                HttpContext.Session.SetString("username", EditFac.facUsername);
                isPostExecuted = true;
                saveMessage = "Changes Saved Successfully!";
                return Page();

            }
            return Page();
        }
    }
}