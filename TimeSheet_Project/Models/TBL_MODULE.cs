namespace TimeSheet_Project.Models
{
    public class TBL_MODULE
    {
        public int MOD_ID { get; set; }
        public String FUN_NAME { get; set; }
        public String MOD_CODE { get; set; }
        public String MOD_NAME { get; set; }
        public String CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public String? UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public byte IS_ACTIVE { get; set; }
    }
}
