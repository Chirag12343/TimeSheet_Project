using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using TimeSheet_Project.Models;

namespace TimeSheet_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly string connection;
        AdminController(IConfiguration config)
        {
            connection = config.GetConnectionString("conn");
        }
        [HttpGet]
        [Route("Get_all_TimeSheet")]
        public IActionResult GetAll_TimeSheet()
        {
            List<TimeSheet> Timesheets = new List<TimeSheet>();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("SP_Daily_TimeSheet",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                TimeSheet timesheet = new TimeSheet();
                DateTime dbDate = read.GetDateTime(1); 
                timesheet.TIMESHEET_DATE = DateOnly.FromDateTime(dbDate);
                timesheet.EMP_NAME = read.GetString(2);
                timesheet.TIMESLOT = read.GetString(3);
                timesheet.HOURS=read.GetInt32(4);
                timesheet.PROJ_NAME = read.GetString(5);
                timesheet.FUN_NAME = read.GetString(6);
                timesheet.MOD_NAME = read.GetString(7); 
                timesheet.TIME_FROM = read.GetString(8);
                timesheet.TIME_TO = read.GetString(9);
                timesheet.TIMESHEET_DESC = read.GetString(10);
                timesheet.CREATED_BY = read.GetString(11);
                timesheet.CREATED_DATE = read.GetDateTime(12);
              Timesheets.Add(timesheet);
            }

            return Ok(Timesheets);
        }
        public IActionResult ShowROles()
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader= cmd.ExecuteReader();
            while (reader.Read())
            { 
            
            }
        }

    }
}
