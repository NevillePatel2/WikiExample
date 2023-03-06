using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection;

namespace Lab1.Pages
{
    public class ScheduleOfficeHoursModel : PageModel
    {
        [BindProperty] public OfficeHours NewOfficeHours { get; set; }
        
        // Properties for Handling Dates and Times
        [BindProperty] public String date { get; set; }
        [BindProperty] public String startTime { get; set; }
        [BindProperty] public String endTime { get; set; }
        [BindProperty] public String purpose { get; set; }


        // Properties for Single Select Dropdown
        [BindProperty] public int SelectedClass { get; set; }
        

        [BindProperty] public int SelectedLocation { get; set; }

        public int currentFacultyID { get; set; }


        public List<OfficeHours> OfficeHoursList;
        public List<Class> ClassList;
        public List<Location> LocationList;


        public ScheduleOfficeHoursModel()
        {
            OfficeHoursList = new List<OfficeHours>();
            ClassList = new List<Class>();
            LocationList = new List<Location>();
        }
        

      
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "Faculty")
            {
                return RedirectToPage("Index");
            }

            //OnGet method loads in data for class and location drop down menus
            SqlDataReader classReader = DBClass.ClassReader();

            while (classReader.Read())
            {
                ClassList.Add(new Class
                {
                    classID = Int32.Parse(classReader["ClassID"].ToString()),
                    className = classReader["ClassName"].ToString(),
                    sectionNumber = Int32.Parse(classReader["SectionNumber"].ToString()),
                    credits = Int32.Parse(classReader["Credits"].ToString()),
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
            //OnPost method assigns binded variables to a NewOfficeHours object then calls the
            //SQL insert method from DBClass
            SqlDataReader currentFacultyIDReader = DBClass.CurrentFacultyID(HttpContext.Session.GetString("username"));
            while (currentFacultyIDReader.Read())
            {
                currentFacultyID = Int32.Parse(currentFacultyIDReader["facultyID"].ToString());
            }
            DBClass.Lab1DBConnection.Close();
            NewOfficeHours.date = date;
            NewOfficeHours.startTime = startTime;
            NewOfficeHours.endTime = endTime;
            NewOfficeHours.purpose = purpose;
            NewOfficeHours.classID = SelectedClass;
            NewOfficeHours.locationID = SelectedLocation;
            NewOfficeHours.facultyID = currentFacultyID;


            DBClass.InsertOfficeHours(NewOfficeHours);
            return RedirectToPage("ViewOfficeHours");
        }
    }
}
