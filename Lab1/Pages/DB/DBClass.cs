using Lab1.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;

namespace Lab1.Pages.DB
{
    public class DBClass
    {
        public static SqlConnection Lab1DBConnection = new SqlConnection();
        public static SqlConnection AuthConnection = new SqlConnection();

        // Connection string to the DB
        private static readonly String? Lab1DBConnString = "Server=Localhost;Database=Lab3;Trusted_Connection=true;TrustServerCertificate=True";
        private static readonly String? AuthConnString = "Server=Localhost;Database=AUTH;Trusted_Connection=True;TrustServerCertificate=True";
        public static SqlDataReader ClassReader()
        {
            SqlCommand cmdClassReader = new SqlCommand();
            cmdClassReader.Connection = Lab1DBConnection;
            cmdClassReader.Connection.ConnectionString = Lab1DBConnString;
            cmdClassReader.CommandText = "SELECT * FROM Class;";

            cmdClassReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdClassReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;

        }

        //This allows students to see the specific office hours for a chosen faculty member in the ViewOfficeHours Page
        public static SqlDataReader SpecificOfficeHoursReader(int FacultyID)
        {
            SqlCommand SpecificOfficeHoursReader = new SqlCommand();
            SpecificOfficeHoursReader.Connection = Lab1DBConnection;
            SpecificOfficeHoursReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT f.FacFirstName, oh.OfficeHoursID, f.FacLastName, oh.Date, oh.startTime, oh.endTime, oh.Purpose, l.RoomNumber " +
                "FROM Faculty F JOIN OfficeHours oh ON f.facultyID = oh.facultyID JOIN Location l ON oh.locationID = l.locationID " +
                "WHERE f.FacultyID = " + FacultyID;

            SpecificOfficeHoursReader.CommandText = sqlQuery;
            SpecificOfficeHoursReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = SpecificOfficeHoursReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;
        }

        public static SqlDataReader SelectedOfficeHoursReader(int OfficeHoursID)
        {
            Lab1DBConnection.Close();
            SqlCommand SelectedOfficeHoursReader = new SqlCommand();
            SelectedOfficeHoursReader.Connection = Lab1DBConnection;
            SelectedOfficeHoursReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT f.facultyID, f.FacFirstName, oh.OfficeHoursID, f.FacLastName, oh.Date, oh.startTime, oh.endTime, oh.Purpose, l.RoomNumber " +
                "FROM Faculty F JOIN OfficeHours oh ON f.facultyID = oh.facultyID JOIN Location l ON oh.locationID = l.locationID " +
                "WHERE oh.OfficeHoursID = " + OfficeHoursID;

            SelectedOfficeHoursReader.CommandText = sqlQuery;
            SelectedOfficeHoursReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = SelectedOfficeHoursReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;
        }

        public static SqlDataReader FacultySearchReader(String facFirstName, String facLastName)
        {
            SqlCommand FacultySearchReader = new SqlCommand();
            FacultySearchReader.Connection = Lab1DBConnection;
            FacultySearchReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT f.FacFirstName, oh.OfficeHoursID, f.FacLastName, oh.Date, oh.startTime, oh.endTime, oh.Purpose, l.RoomNumber " +
                "FROM Faculty F JOIN OfficeHours oh ON f.facultyID = oh.facultyID JOIN Location l ON oh.locationID = l.locationID " +
                "WHERE f.FacFirstName = '" + facFirstName + "' AND f.FacLastName = '" + facLastName + "';";

            FacultySearchReader.CommandText = sqlQuery;
            FacultySearchReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = FacultySearchReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;
        }



        public static SqlDataReader FacultyReader()
        {
            Lab1DBConnection.Close();
            SqlCommand cmdFacultyReader = new SqlCommand();
            cmdFacultyReader.Connection = Lab1DBConnection;
            cmdFacultyReader.Connection.ConnectionString = Lab1DBConnString;
            cmdFacultyReader.CommandText = "SELECT * FROM Faculty";

            cmdFacultyReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdFacultyReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;

        }

        public static SqlDataReader LocationReader()
        {
            SqlCommand cmdLocationReader = new SqlCommand();
            cmdLocationReader.Connection = Lab1DBConnection;
            cmdLocationReader.Connection.ConnectionString = Lab1DBConnString;
            cmdLocationReader.CommandText = "SELECT * FROM Location";

            cmdLocationReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdLocationReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;

        }

        public static SqlDataReader CurrentFacultyID(String facUsername)
        {

            SqlCommand cmdCurrentFacultyIDReader = new SqlCommand();
            cmdCurrentFacultyIDReader.Connection = Lab1DBConnection;
            cmdCurrentFacultyIDReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT facultyID FROM Faculty WHERE FacUsername = '" + facUsername + "';";
            cmdCurrentFacultyIDReader.CommandText = sqlQuery;

            cmdCurrentFacultyIDReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdCurrentFacultyIDReader.ExecuteReader();
            //tempReader contains data from the executeReader()


            return tempReader;

        }

        public static SqlDataReader CurrentStudentID(String stuUsername)
        {
            Lab1DBConnection.Close();
            SqlCommand cmdCurrentStudentIDReader = new SqlCommand();
            cmdCurrentStudentIDReader.Connection = Lab1DBConnection;
            cmdCurrentStudentIDReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT studentID FROM Student WHERE StuUsername = '" + stuUsername + "';";
            cmdCurrentStudentIDReader.CommandText = sqlQuery;

            cmdCurrentStudentIDReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdCurrentStudentIDReader.ExecuteReader();
            //tempReader contains data from the executeReader()


            return tempReader;

        }



        public static SqlDataReader LocationIDReader(String roomNumber)
        {
            SqlCommand cmdLocationIDReader = new SqlCommand();
            cmdLocationIDReader.Connection = Lab1DBConnection;
            cmdLocationIDReader.Connection.ConnectionString = Lab1DBConnString;
            cmdLocationIDReader.CommandText = "SELECT locationID FROM location WHERE roomNumber = '" + roomNumber + "';";

            cmdLocationIDReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdLocationIDReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;

        }

        public static SqlDataReader StudentNotificationReader(int studentID)
        {
            Lab1DBConnection.Close();
            SqlCommand cmdNotificationReader = new SqlCommand();
            cmdNotificationReader.Connection = Lab1DBConnection;
            cmdNotificationReader.Connection.ConnectionString = Lab1DBConnString;
            cmdNotificationReader.CommandText = "select n.NotificationID, n.Sender, n.StudentID, f.FacFirstName, f.FacLastName, n.Message, n.Timestamp " +
                "from Notification n JOIN Faculty f on n.FacultyID = f.FacultyID where StudentID = " + studentID +
                " order by n.Timestamp desc";

            cmdNotificationReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdNotificationReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;

        }

        public static SqlDataReader FacultyNotificationReader(int facultyID)
        {
            SqlCommand cmdNotificationReader = new SqlCommand();
            cmdNotificationReader.Connection = Lab1DBConnection;
            cmdNotificationReader.Connection.ConnectionString = Lab1DBConnString;
            cmdNotificationReader.CommandText = "select n.FacultyID, n.NotificationID, n.Sender, n.StudentID, s.stuFirstName, s.stuLastName, n.Message, n.Timestamp " +
                "from Notification n JOIN Student s on n.studentID = s.StudentID where n.FacultyID = " + facultyID +
                " order by n.Timestamp desc";

            cmdNotificationReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdNotificationReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;

        }
        public static SqlDataReader SpecificMeetingReader(int studentID)
        {
            SqlCommand SpecificMeetingReader = new SqlCommand();
            SpecificMeetingReader.Connection = Lab1DBConnection;
            SpecificMeetingReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT f.FacFirstName, m.meetingID, f.FacLastName, m.Date, m.startTime, m.Purpose, l.RoomNumber " +
                "FROM Faculty F JOIN Meeting m ON f.facultyID = m.facultyID JOIN Location l ON m.locationID = l.locationID JOIN Student s ON m.studentID = s.StudentID " +
                "WHERE s.StudentID = " + studentID;

            SpecificMeetingReader.CommandText = sqlQuery;
            SpecificMeetingReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = SpecificMeetingReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;
        }

        public static SqlDataReader SingleMeetingReader(int meetingID)
        {
            SqlCommand SingleMeetingReader = new SqlCommand();
            SingleMeetingReader.Connection = Lab1DBConnection;
            SingleMeetingReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT f.FacFirstName, m.meetingID, f.FacLastName, m.Date, m.startTime, m.Purpose, l.RoomNumber " +
                "FROM Faculty F JOIN Meeting m ON f.facultyID = m.facultyID JOIN Location l ON m.locationID = l.locationID JOIN Student s ON m.studentID = s.StudentID " +
                "WHERE m.meetingID = " + meetingID;

            SingleMeetingReader.CommandText = sqlQuery;
            SingleMeetingReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = SingleMeetingReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;
        }
        public static SqlDataReader SpecificQueueReader(int studentID)
        {
            SqlCommand SpecificQueueReader = new SqlCommand();
            SpecificQueueReader.Connection = Lab1DBConnection;
            SpecificQueueReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT f.FacFirstName, q.QueueID, f.FacLastName, oh.Date, oh.startTime, oh.endTime, oh.Purpose, l.RoomNumber, oh.officeHoursID " +
                "FROM Faculty F JOIN OfficeHours oh ON f.facultyID = oh.facultyID JOIN Queue q ON oh.OfficeHoursID = q.OfficeHoursID " +
                "JOIN Student s ON q.studentID = s.StudentID JOIN Location l ON oh.locationID = l.locationID " +
                "WHERE s.StudentID = " + studentID;

            SpecificQueueReader.CommandText = sqlQuery;
            SpecificQueueReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = SpecificQueueReader.ExecuteReader();
            //tempReader contains data from the executeReader()

            return tempReader;
        }

        public static void CreateFacultyAccount(Faculty F)
        {
            String sqlQuery = "INSERT INTO Faculty (facUsername, facFirstName, facLastName, facEmail, " +
                "Role, locationID) VALUES ('";
            sqlQuery += F.facUsername + "',";
            sqlQuery += "'" + F.facFirstName + "',";
            sqlQuery += "'" + F.facLastName + "',";
            sqlQuery += "'" + F.facEmail + "',";

            sqlQuery += "'Faculty',";
            sqlQuery += F.locationID + ");";

            SqlCommand cmdInsertOfficeHours = new SqlCommand();
            cmdInsertOfficeHours.Connection = Lab1DBConnection;
            cmdInsertOfficeHours.Connection.ConnectionString = Lab1DBConnString;
            cmdInsertOfficeHours.CommandText = sqlQuery;

            cmdInsertOfficeHours.Connection.Open(); //handshake between code and DB

            cmdInsertOfficeHours.ExecuteNonQuery();
            cmdInsertOfficeHours.Connection.Close();
            //use non-query when doing anything other than INSERTING data
            //tempReader contains data from the executeReader()
        }

        public static void CreateQueue(int OfficeHoursID, int StudentID)
        {
            DateTime currentTime = DateTime.Now;
            String sqlQuery = "INSERT INTO Queue (ArrivalTime, OfficeHoursID, StudentID) " +
                "VALUES ('";
            sqlQuery += currentTime + "',";
            sqlQuery += OfficeHoursID + ",";
            sqlQuery += StudentID + ");";

            SqlCommand cmdCreateQueue = new SqlCommand();
            cmdCreateQueue.Connection = Lab1DBConnection;
            cmdCreateQueue.Connection.ConnectionString = Lab1DBConnString;
            cmdCreateQueue.CommandText = sqlQuery;

            cmdCreateQueue.Connection.Open(); //handshake between code and DB

            cmdCreateQueue.ExecuteNonQuery();
            cmdCreateQueue.Connection.Close();
            //use non-query when doing anything other than INSERTING data
            //tempReader contains data from the executeReader()
        }

        public static void CreateMeeting(Meeting M)
        {
            String sqlQuery = "INSERT INTO Meeting (Purpose, Date, StartTime, LocationID, FacultyID, StudentID) " +
                "VALUES ('";
            sqlQuery += M.purpose + "',";
            sqlQuery += "'" + M.date + "',";
            sqlQuery += "'" + M.startTime + "',";
            sqlQuery += M.locationID + ",";
            sqlQuery += M.facultyID + ",";
            sqlQuery += M.studentID + ");";

            SqlCommand cmdInsertOfficeHours = new SqlCommand();
            cmdInsertOfficeHours.Connection = Lab1DBConnection;
            cmdInsertOfficeHours.Connection.ConnectionString = Lab1DBConnString;
            cmdInsertOfficeHours.CommandText = sqlQuery;

            cmdInsertOfficeHours.Connection.Open(); //handshake between code and DB

            cmdInsertOfficeHours.ExecuteNonQuery();
            cmdInsertOfficeHours.Connection.Close();
            //use non-query when doing anything other than INSERTING data
            //tempReader contains data from the executeReader()
        }

        public static void CreateMeetingRequest(MeetingRequest M)
        {
            String sqlQuery = "INSERT INTO MeetingRequest (Purpose, Date, StartTime, LocationID, FacultyID, StudentID) " +
                "VALUES ('";
            sqlQuery += M.purpose + "',";
            sqlQuery += "'" + M.date + "',";
            sqlQuery += "'" + M.startTime + "',";
            sqlQuery += M.locationID + ",";
            sqlQuery += M.facultyID + ",";
            sqlQuery += M.studentID + ");";

            SqlCommand cmdCreateMeetingRequest = new SqlCommand();
            cmdCreateMeetingRequest.Connection = Lab1DBConnection;
            cmdCreateMeetingRequest.Connection.ConnectionString = Lab1DBConnString;
            cmdCreateMeetingRequest.CommandText = sqlQuery;

            cmdCreateMeetingRequest.Connection.Open(); //handshake between code and DB

            cmdCreateMeetingRequest.ExecuteNonQuery();
            cmdCreateMeetingRequest.Connection.Close();
            //use non-query when doing anything other than INSERTING data
            //tempReader contains data from the executeReader()
        }

        public static void CreateNotification(Notification N)
        {
            Lab1DBConnection.Close();
            String sqlQuery = "INSERT INTO Notification (Message, Sender, Timestamp, FacultyID, StudentID) " +
                "VALUES ('";
            sqlQuery += N.Message + "',";
            sqlQuery += "'" + N.Sender + "',";
            sqlQuery += "'" + N.Timestamp + "',";
            sqlQuery += N.FacultyID + ",";
            sqlQuery += N.StudentID + ");";
            


            SqlCommand cmdInsertOfficeHours = new SqlCommand();
            cmdInsertOfficeHours.Connection = Lab1DBConnection;
            cmdInsertOfficeHours.Connection.ConnectionString = Lab1DBConnString;
            cmdInsertOfficeHours.CommandText = sqlQuery;

            cmdInsertOfficeHours.Connection.Open(); //handshake between code and DB

            cmdInsertOfficeHours.ExecuteNonQuery();
            cmdInsertOfficeHours.Connection.Close();
            //use non-query when doing anything other than INSERTING data
            //tempReader contains data from the executeReader()
        }
        public static void CreateLocation(Location L)
        {
            String sqlQuery = "INSERT INTO Location (roomNumber, capacity) VALUES ('";
            sqlQuery += L.roomNumber + "',";
            sqlQuery += L.capacity + ");";

            SqlCommand cmdInsertOfficeHours = new SqlCommand();
            cmdInsertOfficeHours.Connection = Lab1DBConnection;
            cmdInsertOfficeHours.Connection.ConnectionString = Lab1DBConnString;
            cmdInsertOfficeHours.CommandText = sqlQuery;

            cmdInsertOfficeHours.Connection.Open(); //handshake between code and DB

            cmdInsertOfficeHours.ExecuteNonQuery();
            cmdInsertOfficeHours.Connection.Close();
        }

        public static void CreateStudentAccount(Student S)
        {
            String sqlQuery = "INSERT INTO Student (stuUsername, stuFirstName, stuLastName, stuEmail, " +
                "stuPhone, stuMajor, Role) VALUES ('";
            sqlQuery += S.stuUsername + "',";
            sqlQuery += "'" + S.stuFirstName + "',";
            sqlQuery += "'" + S.stuLastName + "',";
            sqlQuery += "'" + S.stuEmail + "',";
            sqlQuery += S.stuPhone + ",";
            sqlQuery += "'" + S.stuMajor + "',";
            sqlQuery += "'Student');";

            SqlCommand cmdInsertOfficeHours = new SqlCommand();
            cmdInsertOfficeHours.Connection = Lab1DBConnection;
            cmdInsertOfficeHours.Connection.ConnectionString = Lab1DBConnString;
            cmdInsertOfficeHours.CommandText = sqlQuery;

            cmdInsertOfficeHours.Connection.Open(); //handshake between code and DB

            cmdInsertOfficeHours.ExecuteNonQuery();
            cmdInsertOfficeHours.Connection.Close();
            //use non-query when doing anything other than INSERTING data
            //tempReader contains data from the executeReader()
        }


        public static void InsertOfficeHours(OfficeHours O)
        {
            String sqlQuery = "INSERT INTO OfficeHours (Date,StartTime,EndTime,Purpose,LocationID,FacultyID,ClassID) VALUES ('";
            sqlQuery += O.date + "',";
            sqlQuery += "'" + O.startTime + "',";
            sqlQuery += "'" + O.endTime + "',";
            sqlQuery += "'" + O.purpose + "',";
            sqlQuery += O.locationID + ",";
            sqlQuery += O.facultyID + ",";
            sqlQuery += O.classID + ");";

            SqlCommand cmdInsertOfficeHours = new SqlCommand();
            cmdInsertOfficeHours.Connection = Lab1DBConnection;
            cmdInsertOfficeHours.Connection.ConnectionString = Lab1DBConnString;
            cmdInsertOfficeHours.CommandText = sqlQuery;

            cmdInsertOfficeHours.Connection.Open(); //handshake between code and DB

            cmdInsertOfficeHours.ExecuteNonQuery();
            cmdInsertOfficeHours.Connection.Close();
            //use non-query when doing anything other than INSERTING data
            //tempReader contains data from the executeReader()


        }



        public static int QueueCount(int officeHoursID)
        {
            string queueCountQuery =
                "SELECT COUNT(*) FROM Queue WHERE officeHoursID = @selectedOfficeHoursID;";

            SqlCommand cmdQueueCount = new SqlCommand();
            cmdQueueCount.Connection = Lab1DBConnection;
            cmdQueueCount.Connection.ConnectionString = Lab1DBConnString;

            cmdQueueCount.CommandText = queueCountQuery;
            cmdQueueCount.Parameters.AddWithValue("@selectedOfficeHoursID", officeHoursID);


            cmdQueueCount.Connection.Open();


            int queueCount = (int)cmdQueueCount.ExecuteScalar();

            return queueCount;
        }

        public static int QueueValidation(int currentStudentID, int selectedOfficeHoursID)
        {
            string QueueValidationQuery =
                "SELECT COUNT(*) FROM Queue where StudentID = @currentStudentID and OfficeHoursID = @selectedOfficeHoursID";

            SqlCommand cmdQueueValidation = new SqlCommand();
            cmdQueueValidation.Connection = Lab1DBConnection;
            cmdQueueValidation.Connection.ConnectionString = Lab1DBConnString;

            cmdQueueValidation.CommandText = QueueValidationQuery;
            cmdQueueValidation.Parameters.AddWithValue("@currentStudentID", currentStudentID);
            cmdQueueValidation.Parameters.AddWithValue("@selectedOfficeHoursID", selectedOfficeHoursID);

            cmdQueueValidation.Connection.Open();


            int rowCount = (int)cmdQueueValidation.ExecuteScalar();

            return rowCount;
        }


        public static SqlDataReader CurrentStudent(String stuUsername)
        {

            SqlCommand cmdCurrentStudentIDReader = new SqlCommand();
            cmdCurrentStudentIDReader.Connection = Lab1DBConnection;
            cmdCurrentStudentIDReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT * FROM Student WHERE StuUsername = '" + stuUsername + "';";
            cmdCurrentStudentIDReader.CommandText = sqlQuery;

            cmdCurrentStudentIDReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdCurrentStudentIDReader.ExecuteReader();
            //tempReader contains data from the executeReader()


            return tempReader;

        }

        public static SqlDataReader CurrentFaculty(String facUsername)
        {

            SqlCommand cmdCurrentFacultyIDReader = new SqlCommand();
            cmdCurrentFacultyIDReader.Connection = Lab1DBConnection;
            cmdCurrentFacultyIDReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT * FROM Faculty WHERE FacUsername = '" + facUsername + "';";
            cmdCurrentFacultyIDReader.CommandText = sqlQuery;

            cmdCurrentFacultyIDReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdCurrentFacultyIDReader.ExecuteReader();
            //tempReader contains data from the executeReader()


            return tempReader;

        }
        public static void EditStudent(Student S, int currentStudentID)
        {
            DBClass.Lab1DBConnection.Close();
            DBClass.Lab1DBConnection.Close();
            String updateQuery = "UPDATE Student SET StuUsername = '";
            updateQuery += S.stuUsername + "', ";
            updateQuery += "StuFirstName = '" + S.stuFirstName + "', ";
            updateQuery += "StuLastName = '" + S.stuLastName + "', ";
            updateQuery += "StuEmail = '" + S.stuEmail + "', ";
            updateQuery += "StuPhone = '" + S.stuPhone + "', ";
            updateQuery += "StuMajor = '" + S.stuMajor + "' WHERE StudentID = '" + currentStudentID + "';";

            SqlCommand cmdEditStudent = new SqlCommand();
            cmdEditStudent.Connection = Lab1DBConnection;
            cmdEditStudent.Connection.ConnectionString = Lab1DBConnString;
            cmdEditStudent.CommandText = updateQuery;
            cmdEditStudent.Connection.Open();
            cmdEditStudent.ExecuteNonQuery();
            cmdEditStudent.Connection.Close();

        }

        public static void EditFaculty(Faculty F, int currentFacultyID)
        {
            DBClass.Lab1DBConnection.Close();

            String updateQuery = "UPDATE Faculty SET FacUsername = '";
            updateQuery += F.facUsername + "', ";
            updateQuery += "FacFirstName = '" + F.facFirstName + "', ";
            updateQuery += "FacLastName = '" + F.facLastName + "', ";
            updateQuery += "FacEmail = '" + F.facEmail + "' ";

            updateQuery += "WHERE FacultyID = " + currentFacultyID + ";";

            SqlCommand cmdEditStudent = new SqlCommand();
            cmdEditStudent.Connection = Lab1DBConnection;
            cmdEditStudent.Connection.ConnectionString = Lab1DBConnString;
            cmdEditStudent.CommandText = updateQuery;
            cmdEditStudent.Connection.Open();
            cmdEditStudent.ExecuteNonQuery();
            cmdEditStudent.Connection.Close();

        }

        public static void LeaveQueue(int officeHoursID, int currentStudentID)
        {
            DBClass.Lab1DBConnection.Close();
            DBClass.Lab1DBConnection.Close();
            String leaveQuery = "DELETE FROM Queue WHERE studentID = " + currentStudentID + " AND officeHoursID = " + officeHoursID;

            SqlCommand cmdLeaveQueue = new SqlCommand();
            cmdLeaveQueue.Connection = Lab1DBConnection;
            cmdLeaveQueue.Connection.ConnectionString = Lab1DBConnString;
            cmdLeaveQueue.CommandText = leaveQuery;
            cmdLeaveQueue.Connection.Open();
            cmdLeaveQueue.ExecuteNonQuery();
            cmdLeaveQueue.Connection.Close();

        }

        public static void DeleteNotification(int NotificationID)
        {
            DBClass.Lab1DBConnection.Close();
            DBClass.Lab1DBConnection.Close();
            String deleteQuery = "DELETE FROM Notification WHERE NotificationID = " + NotificationID;

            SqlCommand cmdDeleteNotification = new SqlCommand();
            cmdDeleteNotification.Connection = Lab1DBConnection;
            cmdDeleteNotification.Connection.ConnectionString = Lab1DBConnString;
            cmdDeleteNotification.CommandText = deleteQuery;
            cmdDeleteNotification.Connection.Open();
            cmdDeleteNotification.ExecuteNonQuery();
            cmdDeleteNotification.Connection.Close();

        }

        public static void CancelOfficeHours(int officeHoursID)
        {
            DBClass.Lab1DBConnection.Close();
            DBClass.Lab1DBConnection.Close();
            DeleteQueue(officeHoursID);
            String leaveQuery = "DELETE FROM OfficeHours WHERE officeHoursID = " + officeHoursID;

            SqlCommand cmdLeaveQueue = new SqlCommand();
            cmdLeaveQueue.Connection = Lab1DBConnection;
            cmdLeaveQueue.Connection.ConnectionString = Lab1DBConnString;
            cmdLeaveQueue.CommandText = leaveQuery;
            cmdLeaveQueue.Connection.Open();
            cmdLeaveQueue.ExecuteNonQuery();
            cmdLeaveQueue.Connection.Close();

        }

        public static void DeleteQueue(int officeHoursID)
        {
            
            DBClass.Lab1DBConnection.Close();
            String leaveQuery = "DELETE FROM queue WHERE officeHoursID = " + officeHoursID;

            SqlCommand cmdLeaveQueue = new SqlCommand();
            cmdLeaveQueue.Connection = Lab1DBConnection;
            cmdLeaveQueue.Connection.ConnectionString = Lab1DBConnString;
            cmdLeaveQueue.CommandText = leaveQuery;
            cmdLeaveQueue.Connection.Open();
            cmdLeaveQueue.ExecuteNonQuery();
            cmdLeaveQueue.Connection.Close();

        }

        public static void CancelMeeting(int meetingID, int currentStudentID)
        {
            DBClass.Lab1DBConnection.Close();
            DBClass.Lab1DBConnection.Close();
            String cancelQuery = "DELETE FROM Meeting WHERE studentID = " + currentStudentID + " AND meetingID = " + meetingID;

            SqlCommand cmdCancelMeeting = new SqlCommand();
            cmdCancelMeeting.Connection = Lab1DBConnection;
            cmdCancelMeeting.Connection.ConnectionString = Lab1DBConnString;
            cmdCancelMeeting.CommandText = cancelQuery;
            cmdCancelMeeting.Connection.Open();
            cmdCancelMeeting.ExecuteNonQuery();
            cmdCancelMeeting.Connection.Close();

        }

        public static SqlDataReader QueueList(int officeHoursID)
        {
            Lab1DBConnection.Close();
            SqlCommand cmdCurrentFacultyIDReader = new SqlCommand();
            cmdCurrentFacultyIDReader.Connection = Lab1DBConnection;
            cmdCurrentFacultyIDReader.Connection.ConnectionString = Lab1DBConnString;
            String sqlQuery = "SELECT s.StuFirstName, s.StuLastName, q.ArrivalTime, s.studentID FROM student s INNER JOIN queue q " +
                "ON s.studentid = q.studentid INNER JOIN officehours o ON q.officehoursid = o.officehoursid " +
                "WHERE o.officehoursid = " + officeHoursID +
                " ORDER BY ArrivalTime ASC";
            cmdCurrentFacultyIDReader.CommandText = sqlQuery;

            cmdCurrentFacultyIDReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdCurrentFacultyIDReader.ExecuteReader();
            //tempReader contains data from the executeReader()


            return tempReader;

        }

        public static bool HashedStuParameterLogin(string Username, string Password)
        {
            AuthConnection.Close();


            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = AuthConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;
            cmdLogin.CommandType = System.Data.CommandType.StoredProcedure;
            cmdLogin.Parameters.AddWithValue("@StuHashUsername", Username);
            cmdLogin.CommandText = "sp_Lab3LoginStu";


            cmdLogin.Connection.Open();

            SqlDataReader hashReader = cmdLogin.ExecuteReader();
            if (hashReader.Read())
            {
                string correctHash = hashReader["StuHashPassword"].ToString();

                if (PasswordHash.ValidatePassword(Password, correctHash))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HashedFacParameterLogin(string Username, string Password)
        {
            AuthConnection.Close();


            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = AuthConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;
            cmdLogin.CommandType = System.Data.CommandType.StoredProcedure;
            cmdLogin.Parameters.AddWithValue("@FacHashUsername", Username);
            cmdLogin.CommandText = "sp_Lab3LoginFac";


            cmdLogin.Connection.Open();

            SqlDataReader hashReader = cmdLogin.ExecuteReader();
            if (hashReader.Read())
            {
                string correctHash = hashReader["FacHashPassword"].ToString();

                if (PasswordHash.ValidatePassword(Password, correctHash))
                {
                    return true;
                }
            }

            return false;
        }

        public static void CreateHashedStudent(string Username, string Password)
        {
            string loginQuery = "INSERT INTO StuHashLogin (StuHashUsername,StuHashPassword) values (@StuHashUsername, @StuHashPassword)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = AuthConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@StuHashUsername", Username);
            cmdLogin.Parameters.AddWithValue("@StuHashPassword", PasswordHash.HashPassword(Password));

            cmdLogin.Connection.Open();

            cmdLogin.ExecuteNonQuery();
        }

        public static void CreateHashedFaculty(string Username, string Password)
        {
            string loginQuery = "INSERT INTO FacHashLogin (FacHashUsername,FacHashPassword) values (@FacHashUsername, @FacHashPassword)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = AuthConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@FacHashUsername", Username);
            cmdLogin.Parameters.AddWithValue("@FacHashPassword", PasswordHash.HashPassword(Password));

            cmdLogin.Connection.Open();

            cmdLogin.ExecuteNonQuery();
        }

        public static void EditStuAuth(StudentAuth S, int stuAuthID)
        {


            String updateQuery = "UPDATE StuHashLogin SET StuHashUsername = @stuAuthUsername, StuHashPassword = @stuAuthPassword WHERE StuHashID = @stuAuthID";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = AuthConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;

            cmdLogin.CommandText = updateQuery;
            cmdLogin.Parameters.AddWithValue("@stuAuthPassword", PasswordHash.HashPassword(S.stuAuthPassword));
            cmdLogin.Parameters.AddWithValue("@stuAuthUsername", S.stuAuthUsername);
            cmdLogin.Parameters.AddWithValue("@stuAuthID", stuAuthID);

            cmdLogin.Connection.Open();

            cmdLogin.ExecuteNonQuery();
        }

        public static void EditFacAuth(FacultyAuth F, int facAuthID)
        {


            String updateQuery = "UPDATE FacHashLogin SET FacHashUsername = @facAuthUsername, FacHashPassword = @facAuthPassword WHERE FacHashID = @facAuthID";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = AuthConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;

            cmdLogin.CommandText = updateQuery;
            cmdLogin.Parameters.AddWithValue("@facAuthPassword", PasswordHash.HashPassword(F.facAuthPassword));
            cmdLogin.Parameters.AddWithValue("@facAuthUsername", F.facAuthUsername);
            cmdLogin.Parameters.AddWithValue("@facAuthID", facAuthID);

            cmdLogin.Connection.Open();

            cmdLogin.ExecuteNonQuery();
        }


        public static SqlDataReader CurrentAuthStudentID(String stuUsername)
        {
            Lab1DBConnection.Close();
            AuthConnection.Close();
            SqlCommand cmdCurrentAuthStudentIDReader = new SqlCommand();
            cmdCurrentAuthStudentIDReader.Connection = AuthConnection;
            cmdCurrentAuthStudentIDReader.Connection.ConnectionString = AuthConnString;
            String sqlQuery = "SELECT StuHashID FROM StuHashLogin WHERE StuHashUsername = '" + stuUsername + "';";
            cmdCurrentAuthStudentIDReader.CommandText = sqlQuery;

            cmdCurrentAuthStudentIDReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdCurrentAuthStudentIDReader.ExecuteReader();
            //tempReader contains data from the executeReader()


            return tempReader;

        }

        public static SqlDataReader CurrentAuthFacultyID(String FacUsername)
        {
            Lab1DBConnection.Close();
            AuthConnection.Close();
            SqlCommand cmdCurrentAuthFacultyIDReader = new SqlCommand();
            cmdCurrentAuthFacultyIDReader.Connection = AuthConnection;
            cmdCurrentAuthFacultyIDReader.Connection.ConnectionString = AuthConnString;
            String sqlQuery = "SELECT FacHashID FROM FacHashLogin WHERE FacHashUsername = '" + FacUsername + "';";
            cmdCurrentAuthFacultyIDReader.CommandText = sqlQuery;

            cmdCurrentAuthFacultyIDReader.Connection.Open(); //handshake between code and DB

            SqlDataReader tempReader = cmdCurrentAuthFacultyIDReader.ExecuteReader();
            //tempReader contains data from the executeReader()


            return tempReader;

        }

        public static int QueuePosition(int officeHoursID, int currentStudentID)
        {
            Lab1DBConnection.Close();
            string queuePositionQuery = "SELECT COUNT(*) as QueuePosition FROM Queue q1 WHERE q1.ArrivalTime < " +
                "(SELECT ArrivalTime FROM Queue q2 WHERE q2.StudentID = @CurrentStudentID AND " +
                "q2.OfficeHoursID = @selectedOfficeHoursID)";

            SqlCommand cmdQueuePosition = new SqlCommand();
            cmdQueuePosition.Connection = Lab1DBConnection;
            cmdQueuePosition.Connection.ConnectionString = Lab1DBConnString;
            cmdQueuePosition.CommandText = queuePositionQuery;
            cmdQueuePosition.Parameters.AddWithValue("selectedOfficeHoursID", officeHoursID);
            cmdQueuePosition.Parameters.AddWithValue("CurrentStudentID", currentStudentID);

            cmdQueuePosition.Connection.Open();
            int queuePosition = (int)cmdQueuePosition.ExecuteScalar();
            return queuePosition;
        }

        public static int StudentNotificationCount(int currentStudentID)
        {
            string StuNotiCount = "SELECT COUNT(*) from Notification where studentID = @CurrentStudentID and Sender = 'Faculty'";
            SqlCommand cmdStuNotiCount = new SqlCommand();
            cmdStuNotiCount.Connection = Lab1DBConnection;
            cmdStuNotiCount.Connection.ConnectionString = Lab1DBConnString;
            cmdStuNotiCount.CommandText = StuNotiCount;
            
            cmdStuNotiCount.Parameters.AddWithValue("CurrentStudentID", currentStudentID);

            cmdStuNotiCount.Connection.Open();
            int NotiCount = (int)cmdStuNotiCount.ExecuteScalar();
            return NotiCount;
        }

        public static int FacultyNotificationCount(int currentFacultyID)
        {
            string FacNotiCount = "SELECT COUNT(*) from Notification where facultyID = @CurrentFacultyID and Sender = 'Student'";
            SqlCommand cmdFacNotiCount = new SqlCommand();
            cmdFacNotiCount.Connection = Lab1DBConnection;
            cmdFacNotiCount.Connection.ConnectionString = Lab1DBConnString;
            cmdFacNotiCount.CommandText = FacNotiCount;

            cmdFacNotiCount.Parameters.AddWithValue("CurrentFacultyID", currentFacultyID);

            cmdFacNotiCount.Connection.Open();
            int NotiCount = (int)cmdFacNotiCount.ExecuteScalar();
            return NotiCount;
        }
    }


}