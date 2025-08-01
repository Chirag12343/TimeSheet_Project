﻿namespace TimeSheet_Project.Models
{
    public class ShowEmployee
    {
        public string ROLE_NAME { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public long EMP_MOBILE_NO { get; set; }
        public string EMP_EMAIL_ID { get; set; }
        public string EMP_PASSWORD { get; set; }
        public string CREATED_BY {  get; set; } 
        public string CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATED_DATE { get;set; }
        public byte IS_ACTIVE { get; set; }
        public int LINE_MANAGER_ID { get; set; }
       public string LINE_MANAGER_EMAIL_ID { get; set; }
        public DateTime EMP_LAST_LOGIN { get; set; }
       
    }
}
