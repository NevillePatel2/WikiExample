using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace Lab1.Pages
{
    public class EditOfficeHoursModel : PageModel
    {
        public List<SelectedOfficeHours> SelectedOfficeHoursList;
        public List<QueueList> CurrentQueueList;
        public Notification NewNotification { get; set; }
        [BindProperty]public int selectedOfficeHoursID { get; set; }
        public int queueCount { get; set; }
        public String currentFacultyUsername { get; set; }
        public int currentFacultyID { get; set; }
        [BindProperty]public int selectedStudentID { get; set; }
        public bool isPostExecuted = false;

        public EditOfficeHoursModel()
        {
            SelectedOfficeHoursList = new List<SelectedOfficeHours>();
            CurrentQueueList = new List<QueueList>();
            NewNotification = new Notification();
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Faculty")
            {
                return RedirectToPage("Index");
            }
            selectedOfficeHoursID = Int32.Parse(Request.Query["officehoursid"]);
            TempData["selectedOfficeHoursID"] = selectedOfficeHoursID;
            HttpContext.Session.SetString("selectedOfficeHoursID", selectedOfficeHoursID.ToString());

            selectedOfficeHoursID = Int32.Parse(HttpContext.Session.GetString("selectedOfficeHoursID"));
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

            SqlDataReader QueueList = DBClass.QueueList(selectedOfficeHoursID);
            while (QueueList.Read())
            {
                CurrentQueueList.Add(new QueueList
                {
                    studentID = Int32.Parse(QueueList["studentID"].ToString()),
                    stuFirstName = QueueList["stuFirstName"].ToString(),
                    stuLastName = QueueList["stuLastName"].ToString(),
                    arrivalTime = QueueList["arrivalTime"].ToString()

                }); ;
            }

            DBClass.Lab1DBConnection.Close();

            return Page();
        }

        public void OnPost()
        {
            isPostExecuted = true;
            
            selectedOfficeHoursID = Int32.Parse(HttpContext.Session.GetString("selectedOfficeHoursID"));


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
            SqlDataReader QueueList = DBClass.QueueList(selectedOfficeHoursID);
            while (QueueList.Read())
            {
                CurrentQueueList.Add(new QueueList
                {
                    studentID = Int32.Parse(QueueList["studentID"].ToString()),
                    stuFirstName = QueueList["stuFirstName"].ToString(),
                    stuLastName = QueueList["stuLastName"].ToString(),
                    arrivalTime = QueueList["arrivalTime"].ToString()

                }); ;
            }
            HttpContext.Session.SetString("selectedStudentID", selectedStudentID.ToString());
            selectedStudentID = Int32.Parse(HttpContext.Session.GetString("selectedStudentID"));
            DBClass.Lab1DBConnection.Close();
            
            
        }

        public IActionResult OnPostNotifyStudent()
        {

            isPostExecuted = false;
            
            NewNotification.Message = "I am ready to see you!";
            NewNotification.Timestamp = DateTime.Now.ToString();

            SqlDataReader currentFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
            while (currentFacultyIDReader.Read())
            {
                currentFacultyID = Int32.Parse(currentFacultyIDReader["facultyID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();
            NewNotification.FacultyID = currentFacultyID;
            NewNotification.StudentID = Int32.Parse(HttpContext.Session.GetString("selectedStudentID"));
            NewNotification.Sender = "Faculty";
            DBClass.CreateNotification(NewNotification);
            DBClass.Lab1DBConnection.Close();

            selectedOfficeHoursID = Int32.Parse(HttpContext.Session.GetString("selectedOfficeHoursID"));
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

            SqlDataReader QueueList = DBClass.QueueList(selectedOfficeHoursID);
            while (QueueList.Read())
            {
                CurrentQueueList.Add(new QueueList
                {
                    studentID = Int32.Parse(QueueList["studentID"].ToString()),
                    stuFirstName = QueueList["stuFirstName"].ToString(),
                    stuLastName = QueueList["stuLastName"].ToString(),
                    arrivalTime = QueueList["arrivalTime"].ToString()

                }); ;
            }

            DBClass.Lab1DBConnection.Close();



            return Page();
        }

        //faculty user cannot select a new student to remove from the queue without reloading the page
        //either need to redirect back to ViewOfficeHours or figure out a way to reset the session string for
        //selectedStudentID and repopulate the session string with the newly selected user after removing someone
        public IActionResult OnPostDeleteFromQueue()
        {

            
            selectedOfficeHoursID = Int32.Parse(HttpContext.Session.GetString("selectedOfficeHoursID"));
            //studentID returns null when you try to select a second student (the second student selected is not getting
            //their ID passed into the variable
            selectedStudentID = Int32.Parse(HttpContext.Session.GetString("selectedStudentID"));

            //notification to let student know they have been removed from the queue
            NewNotification.Message = "You were removed from queue!";
            NewNotification.Timestamp = DateTime.Now.ToString();

            SqlDataReader currentFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
            while (currentFacultyIDReader.Read())
            {
                currentFacultyID = Int32.Parse(currentFacultyIDReader["facultyID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            NewNotification.FacultyID = currentFacultyID;
            NewNotification.StudentID = Int32.Parse(HttpContext.Session.GetString("selectedStudentID"));
            NewNotification.Sender = "Faculty";
            
            DBClass.CreateNotification(NewNotification);
            DBClass.Lab1DBConnection.Close();

            DBClass.LeaveQueue(selectedOfficeHoursID, selectedStudentID);
            DBClass.Lab1DBConnection.Close();
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
            SqlDataReader QueueList = DBClass.QueueList(selectedOfficeHoursID);
            while (QueueList.Read())
            {
                CurrentQueueList.Add(new QueueList
                {
                    studentID = Int32.Parse(QueueList["studentID"].ToString()),
                    stuFirstName = QueueList["stuFirstName"].ToString(),
                    stuLastName = QueueList["stuLastName"].ToString(),
                    arrivalTime = QueueList["arrivalTime"].ToString()

                }); ;
            }
            DBClass.Lab1DBConnection.Close();

            

            //HttpContext.Session.Remove("selectedStudentID");
            isPostExecuted = false;
            return Page();
        }

        public IActionResult OnPostCancelOfficeHours()
        {
            int studentID = 0;

            if (TempData.ContainsKey("selectedOfficeHoursID"))
            {
                selectedOfficeHoursID = (int)TempData["selectedOfficeHoursID"];
            }
            selectedOfficeHoursID = Int32.Parse(HttpContext.Session.GetString("selectedOfficeHoursID"));
            DBClass.Lab1DBConnection.Close();

            SqlDataReader currentFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
            while (currentFacultyIDReader.Read())
            {
                currentFacultyID = Int32.Parse(currentFacultyIDReader["facultyID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();

            SqlDataReader QueueList = DBClass.QueueList(selectedOfficeHoursID);
            while (QueueList.Read())
            {
                studentID = Int32.Parse(QueueList["studentID"].ToString());
                CurrentQueueList.Add(new QueueList
                {
                    studentID = studentID,
                    stuFirstName = QueueList["stuFirstName"].ToString(),
                    stuLastName = QueueList["stuLastName"].ToString(),
                    arrivalTime = QueueList["arrivalTime"].ToString()

                }); ;
            }


            foreach (var Student in CurrentQueueList)
            {
                NewNotification.Message = "Sorry! Office hours have been cancelled, please check my upcoming office hours" +
                    " to sign up for a different slot.";
                NewNotification.Timestamp = DateTime.Now.ToString();
                NewNotification.FacultyID = currentFacultyID;
                NewNotification.StudentID = Student.studentID;
                NewNotification.Sender = "Faculty";
                DBClass.CreateNotification(NewNotification);
                DBClass.LeaveQueue(selectedOfficeHoursID, Student.studentID);
            }
            DBClass.Lab1DBConnection.Close();
            DBClass.DeleteQueue(selectedOfficeHoursID);
            DBClass.Lab1DBConnection.Close();
            DBClass.CancelOfficeHours(selectedOfficeHoursID);
            DBClass.Lab1DBConnection.Close();
            
            return RedirectToPage("ViewOfficeHours");
        }

    }
}
