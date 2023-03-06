namespace Lab1.Pages.DataClasses
{
    public class Student
    {
        public int studentID { get; set; }
        public String? stuFirstName { get; set; }
        public String? stuLastName { get; set; }
        public String? stuEmail { get; set; }
        public String stuUsername { get; set; }
        
        public String stuPhone { get; set; }
        public String? stuMajor { get; set; }
        public String role { get; set; }
        public Student? partnerID { get; set; }
    }
}
