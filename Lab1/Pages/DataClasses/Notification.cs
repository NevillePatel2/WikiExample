using System.Reflection.Metadata.Ecma335;

namespace Lab1.Pages.DataClasses
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public String Message { get; set; }
        public String Sender { get; set; }
        public String Timestamp { get; set; }
        public int FacultyID { get; set; }
        public int StudentID { get; set; }
        public int OfficeHoursID { get; set; }
        public int QueueID { get; set; }
        public int MeetingID { get; set; }
    }
}
