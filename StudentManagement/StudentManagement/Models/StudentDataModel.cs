namespace StudentManagement.Models
{
    public class LoginData
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class StudentDetail
    {
        public string rollNo { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string classVal {get; set; }

    }
}