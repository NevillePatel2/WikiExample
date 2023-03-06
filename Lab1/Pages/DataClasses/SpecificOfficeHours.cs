namespace Lab1.Pages.DataClasses
{
    public class SpecificOfficeHours
    {
        public int officeHoursID { get; set; }
        public String? startTime { get; set; }
        public String? endTime { get; set; }
        public String? date { get; set; }
        public String? purpose { get; set; }
        public int locationID { get; set; }
        public int facultyID { get; set; }
        public int classID { get; set; }
        public String? facFirstName { get; set; }
        public String? facLastName { get; set; }
        public String? facEmail { get; set; }
        public String? facUsername { get; set; }
        public String? facPassword { get; set; }
        
        public String roomNumber { get; set; }
        public int capacity { get; set; }

    }
}
