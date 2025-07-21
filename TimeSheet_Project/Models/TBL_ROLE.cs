namespace TimeSheet_Project.Models
{
    public class TBL_ROLE
    {

        public int ROLE_ID { get; set; }
        public string ROLE_CODE { get; set; }
        public string ROLE_NAME { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string? UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public byte IS_ACTIVE { get; set; }

    }
}
