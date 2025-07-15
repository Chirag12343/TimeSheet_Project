namespace TimeSheet_Project.Models
{
    public class UpdateProject
    {
        public int PROJ_ID { get; set; }
        // public string CLIENT_NAME { get; set; }
         public int CLIENT_ID { get; set; }
        public string PROJ_CODE { get; set; }
        public string PROJ_NAME { get; set; }
        public string PROJ_DESC { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string? UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public byte IS_ACTIVE { get; set; }
    }
}
