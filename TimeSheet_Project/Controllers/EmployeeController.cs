using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using TimeSheet_Project.Models;

namespace TimeSheet_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly HolidayService _holidayService;
        private readonly IMemoryCache _cache;
        private readonly string _connection;
        public EmployeeController(IConfiguration config, IMemoryCache cache, HolidayService holidayService)
        {

            _connection = config.GetConnectionString("conn");
            _cache = cache;
            _holidayService = holidayService;
        }
        [HttpPost]
        [Route("TimeSheet_Login")]
        public IActionResult Login(LoginDetails details)
        {
            List<Function> functions = new List<Function>();
            SqlConnection con = new SqlConnection(_connection);
            con.Open();
            var ROLE = "";
            var Emp_name = "";
            var Emp_Id = 0;
            try
            {
                // Execute the stored procedure to validate user
                // SqlCommand cmd = new SqlCommand("SP_GetFunctions", con);
                SqlCommand cmd = new SqlCommand("SP_DEMO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", details.Email);
                cmd.Parameters.AddWithValue("@PassWord", details.Password);

                SqlDataReader reader = cmd.ExecuteReader();

                // If user is valid, proceed with fetching functions
                //if (reader.HasRows)
                //{
                while (reader.Read())
                {
                    Function function = new Function();

                    function.Functions = reader["FUN_NAME"].ToString();
                    function.FUN_ID = Convert.ToInt32(reader["FUN_ID"]);
                    ROLE = reader["ROLE_NAME"].ToString().ToUpper();
                    Emp_name = reader["EMP_NAME"].ToString();
                    Emp_Id = Convert.ToInt32(reader["EMP_ID"]);

                    functions.Add(function);
                }


                var sessionId = Guid.NewGuid().ToString();


                _cache.Set(sessionId, details.Email, TimeSpan.FromHours(1));

                // Return the session ID along with functions
                return Ok(new { Functions = functions, SessionId = sessionId, EMP_ID = Emp_Id, EMP_NAME = Emp_name, EMP_ROLE = ROLE } );

            }
            catch (Exception ex)
            {
                // Handle error and log exception if needed
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }





        [HttpGet]
        [Route("Get_Projects")]
        public IActionResult GetAllProjects()
        {
            List<Projects> projects = new List<Projects>();
            SqlConnection conn = new SqlConnection(_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GetAllProjectnames", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                Projects project = new Projects();
                project.PROJ_ID = Convert.ToInt32(read["PROJ_ID"]);
                project.Client_code = read["CLIENT_CODE"].ToString();
                project.Proj_name = read["PROJ_NAME"].ToString();
                projects.Add(project);

            }
            conn.Close();
            return Ok(projects);
        }
        [HttpGet]
        [Route("Get-All_Modules/{F_ID}")]
        public IActionResult GetAllModules(int F_ID)
        {
            List<GetAllModules> getAllModules = new List<GetAllModules>();
            SqlConnection conn = new SqlConnection(_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GetAllModulenames", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FUN_ID",F_ID );
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                GetAllModules module = new GetAllModules();
                module.MOD_ID = Convert.ToInt32(read["MOD_ID"]);
                module.Module = read["MOD_NAME"].ToString();
                getAllModules.Add(module);
            }
            conn.Close();
            return Ok(getAllModules);

        }
        [HttpPost]
        [Route("Insert_daily_timesheet")]
        public IActionResult UploadTimeSheetDetails(TIMESHEETDETAILS DETAILS)
          {
            SqlConnection conn = new SqlConnection(_connection);
            conn.Open();
            try
            {
                DateTime date = DateTime.Now;
                DateTime todayAt10AM = DateTime.Today.AddHours(10);
                DateTime Tomorrow10AM = todayAt10AM.AddDays(1);
                if (date >= todayAt10AM && date < Tomorrow10AM)
                {
                    Console.WriteLine(todayAt10AM);
                }



                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses", "Holidays.json");
                // List<DateTime> holidays = JsonConvert.DeserializeObject<List<DateTime>>(System.IO.File.ReadAllText(filePath));
                List<DateTime> holidays = JsonConvert.DeserializeObject<List<DateTime>>(System.IO.File.ReadAllText(filePath));

                if (holidays.Contains(todayAt10AM) || todayAt10AM.DayOfWeek == DayOfWeek.Sunday)
                {
                    return BadRequest(new { message = "Today is a holiday. Timesheet entries are not allowed." });
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("SP_InsertDailySheet", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TIMESHEET_DATE", todayAt10AM);
                    cmd.Parameters.AddWithValue("@EMP_ID", DETAILS.EMP_ID);
                    cmd.Parameters.AddWithValue("@SLOT_ID", DETAILS.SLOT_ID);
                    cmd.Parameters.AddWithValue("@HOURSE", DETAILS.HOURS);
                    cmd.Parameters.AddWithValue("@PROJ_ID", DETAILS.PROJ_ID);
                    cmd.Parameters.AddWithValue("@FUN_ID", DETAILS.FUN_ID);
                    cmd.Parameters.AddWithValue("@MOD_ID", DETAILS.MOD_ID);
                    cmd.Parameters.AddWithValue("@TIME_FROM", DETAILS.TIME_FROM);
                    cmd.Parameters.AddWithValue("@TIME_TO", DETAILS.TIME_TO);
                    cmd.Parameters.AddWithValue("@TIMESHEET_DESC", DETAILS.TIMESHEET_DESC);
                    cmd.Parameters.AddWithValue("@CREATED_BY", DETAILS.CREATED_BY);

                    //cmd.ExecuteNonQuery();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result == 1)
                    {
                        return Ok(new { message = "Task Added SuccessFully." });
                    }
                    else
                    {
                        return BadRequest(new { message = "The time slot for this date has already been used. Please choose a different time slot." });
                    }
                }

            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                return BadRequest(E.Message);
            }

            finally
            { conn.Close(); }
            ;

        }


        [HttpGet]
        [Route("Employee-Work-Dates")]
        public IActionResult GetEmployeeWorkDate(int employeeId)
        {
            List<EmplolyeeWorkDates> EmployeeWorkDates = new List<EmplolyeeWorkDates>();
            SqlConnection conn = new SqlConnection(_connection);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GETEmployeeWorkDates", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@employeeID", employeeId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = Convert.ToDateTime(reader["TIMESHEET_DATE"]);
                    EmplolyeeWorkDates EmployeeDetails = new EmplolyeeWorkDates();
                    EmployeeDetails.EmployeeWorkDate = date.Date;

                    EmployeeWorkDates.Add(EmployeeDetails);
                }
                return Ok(EmployeeWorkDates);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            finally { conn.Close(); }

        }

        [HttpPost]
        [Route("GetEmployee_Work_details_by_dateAnd_Id")]
        public IActionResult GetEmployeeAllTasks(Show_EmployeeDataBasedOnTime_And_Id empDetails)
        {
            List<Specific_employeeDetails> EmployeeDataList = new List<Specific_employeeDetails>();
            SqlConnection conn = new SqlConnection(_connection);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_EmployeeTimeSheetDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EMPID", empDetails.Emp_ID);
                cmd.Parameters.AddWithValue("@EMPLOYEEwORKdATE", empDetails.Emp_Work_date);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Specific_employeeDetails emp_details = new Specific_employeeDetails();
                    emp_details.TaskTimeSlot = reader["TIMESLOT"].ToString();
                    emp_details.TaskHours = Convert.ToInt32(reader["HOURS"]);
                    emp_details.taskProject = reader["PROJ_NAME"].ToString();
                    emp_details.TaskFunction = reader["FUN_NAME"].ToString();
                    emp_details.TaskModName = reader["MOD_NAME"].ToString();
                    emp_details.TaskTimeFrom = reader["TIME_FROM"].ToString();
                    emp_details.TaskDesc = reader["TIMESHEET_DESC"].ToString();
                    emp_details.TaskTimeTo = reader["TIME_TO"].ToString();
                    EmployeeDataList.Add(emp_details);

                }
                return Ok(EmployeeDataList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            finally { conn.Close(); }

        }
        [HttpPost]
        [Route("GetMinutes")]
        public IActionResult getMin([FromBody] get_min_basedON_slot DETAILS)
        {

            SqlConnection conn = new SqlConnection(_connection);
            
            List<RemainMinutes> minutes = new List<RemainMinutes>();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_DEMOPROC", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TIME",DETAILS.slotDate);
            cmd.Parameters.AddWithValue("@EMP_ID", DETAILS.EMP_ID);
            cmd.Parameters.AddWithValue("@SLOT", DETAILS.SLOT_ID);
            var result = Convert.ToInt32(cmd.ExecuteScalar());          
            int maxMinutes = Convert.ToInt32(result);
            conn.Close();
            return Ok(maxMinutes);



        }
    }
}
