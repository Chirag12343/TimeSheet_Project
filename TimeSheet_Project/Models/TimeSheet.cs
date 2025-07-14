namespace TimeSheet_Project.Models
{
    public class TimeSheet
    {
        public DateOnly TIMESHEET_DATE { get; set; }
        public string EMP_NAME { get; set; }
        public string TIMESLOT { get; set; }
        public int HOURS { get; set; }
        public string PROJ_NAME { get; set; }
        public string FUN_NAME { get; set; }
        public string MOD_NAME { get; set; }
        public string TIME_FROM { get; set; }
        public string TIME_TO { get; set; }
        public string TIMESHEET_DESC { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
}
