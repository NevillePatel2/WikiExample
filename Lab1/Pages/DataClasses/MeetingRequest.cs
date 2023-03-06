namespace Lab1.Pages.DataClasses
{
    public class MeetingRequest
    {
        public int meetingRequestID { get; set; }
        public String purpose { get; set; }
        public String date { get; set; }
        public String startTime { get; set; }
        public int locationID { get; set; }
        public int facultyID { get; set; }
        public int studentID { get; set; }
    }
}
