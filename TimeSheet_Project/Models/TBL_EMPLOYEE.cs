using System.ComponentModel.DataAnnotations;

namespace TimeSheet_Project.Models
{
    public class TBL_EMPLOYEE
    {
        public int EMP_ID { get; set; }
        public string ROLE_NAME { get; set; }
       // public int  ROLE_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public long EMP_MOBILE_NO { get; set; }
        public string EMP_EMAIL_ID { get; set; }
        public string EMP_PASSWORD { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        //public string? UPDATED_BY { get; set; }
        //public DateTime? UPDATED_DATE { get; set; }
        public byte IS_ACTIVE { get; set; }
        public int? LINE_MANAGER_ID { get; set; }
        public string? LINE_MANAGER_EMAIL_ID { get; set; }
        public DateTime? EMP_LAST_LOGIN { get; set; }

    }



    public class Login
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
