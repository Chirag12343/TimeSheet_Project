namespace TimeSheet_Project.Models
{
    public class EmployeeDetailscs
    {
        public int ROLE_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public long EMP_MOBILE_NO { get; set; }
        public string EMP_EMAIL_ID { get; set; }
        public string EMP_PASSWORD { get; set; }
    }

    public class EmployeeUpdate
    {
        public int EMP_ID { get; set; }
        public int ROLE_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public long EMP_MOBILE_NO { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
