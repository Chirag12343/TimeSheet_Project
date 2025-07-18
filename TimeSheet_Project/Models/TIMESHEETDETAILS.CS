namespace TimeSheet_Project.Models
{
    public class TIMESHEETDETAILS
    {
       
        public int EMP_ID { get; set; }
        public int SLOT_ID { get; set; }
        public int HOURS { get; set; }
        public int PROJ_ID { get; set; }
        public int FUN_ID { get; set; }
        public int MOD_ID { get; set; }
        public String TIME_FROM {  get; set; }  
        public String TIME_TO { get; set; }
        public String? TIMESHEET_DESC { get; set; }
        public string CREATED_BY { get; set; }
        

    }
}
