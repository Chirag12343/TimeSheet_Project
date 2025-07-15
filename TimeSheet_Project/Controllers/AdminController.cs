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
        public AdminController(IConfiguration config)
        {
            connection = config.GetConnectionString("conn");
        }
        //[HttpGet]
        //[Route("Get_all_TimeSheet")]
        //public IActionResult GetAll_TimeSheet()
        //{
        //    List<TimeSheet> Timesheets = new List<TimeSheet>();
        //    SqlConnection conn = new SqlConnection(connection);
        //    SqlCommand cmd = new SqlCommand("SP_Daily_TimeSheet",conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlDataReader read = cmd.ExecuteReader();
        //    while (read.Read())
        //    {
        //        TimeSheet timesheet = new TimeSheet();
        //        DateTime dbDate = read.GetDateTime(1); 
        //        timesheet.TIMESHEET_DATE = DateOnly.FromDateTime(dbDate);
        //        timesheet.EMP_NAME = read.GetString(2);
        //        timesheet.TIMESLOT = read.GetString(3);
        //        timesheet.HOURS=read.GetInt32(4);
        //        timesheet.PROJ_NAME = read.GetString(5);
        //        timesheet.FUN_NAME = read.GetString(6);
        //        timesheet.MOD_NAME = read.GetString(7); 
        //        timesheet.TIME_FROM = read.GetString(8);
        //        timesheet.TIME_TO = read.GetString(9);
        //        timesheet.TIMESHEET_DESC = read.GetString(10);
        //        timesheet.CREATED_BY = read.GetString(11);
        //        timesheet.CREATED_DATE = read.GetDateTime(12);
        //      Timesheets.Add(timesheet);
        //    }

        //    return Ok(Timesheets);
        //}
        [HttpGet]
        [Route("Get_all_TimeSheet")]
        public IActionResult GetAll_TimeSheet()
        {
            List<TimeSheet> Timesheets = new List<TimeSheet>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_Daily_TimeSheet", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                TimeSheet timesheet = new TimeSheet();
                DateTime dbDate = read.GetDateTime(1);
                timesheet.TIMESHEET_DATE = DateOnly.FromDateTime(dbDate);
                timesheet.EMP_NAME = read.GetString(2);
                timesheet.TIMESLOT = read.GetString(3);
                timesheet.HOURS = read.GetInt32(4);
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
            conn.Close();
            return Ok(Timesheets);
        }
        [HttpGet]
        [Route("GET_ALL_ROLES")]
        public IActionResult ShowROles()
        {
            List<TBL_ROLE> roles = new List<TBL_ROLE>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GETALLROLES", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TBL_ROLE role = new TBL_ROLE();
                role.ROLE_ID = Convert.ToInt32(reader["ROLE_ID"]);
                role.ROLE_CODE = reader["ROLE_CODE"].ToString();
                role.ROLE_NAME = reader["ROLE_NAME"].ToString();
                role.CREATED_BY = reader["CREATED_BY"].ToString();
                role.UPDATED_DATE = Convert.ToDateTime(reader["CREATED_DATE"]);
                //role.UPDATED_BY = reader["UPDATED_BY"].ToString();
                role.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    ? reader["UPDATED_BY"].ToString()
    : null;
                role.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    ? (DateTime?)reader["UPDATED_DATE"]
    : null;
                // role.UPDATED_DATE = Convert.ToDateTime(reader["UPDATED_DATE"]);
                role.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
                roles.Add(role);
            }
            return Ok(roles);
        }
        [HttpGet]
        [Route("GET_ALL_EMPLOYEE")]
        public IActionResult ShowEmployees()
        {
            List<TBL_EMPLOYEE> employees = new List<TBL_EMPLOYEE>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GETALLEMPLOYEE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TBL_EMPLOYEE employee = new TBL_EMPLOYEE();
                employee.EMP_ID = Convert.ToInt32(reader["EMP_ID"]);
                employee.ROLE_NAME = reader["ROLE_NAME"].ToString();
                employee.EMP_CODE = reader["EMP_CODE"].ToString();
                employee.EMP_NAME = reader["EMP_NAME"].ToString();
                employee.EMP_MOBILE_NO = Convert.ToInt64(reader["EMP_MOBILE_NO"]);
                employee.EMP_PASSWORD = reader["EMP_PASSWORD"].ToString();
                employee.CREATED_BY = reader["CREATED_BY"].ToString();
                employee.CREATED_DATE = Convert.ToDateTime(reader["CREATED_DATE"]);
                //employee.UPDATED_BY = reader["UPDATED_BY"].ToString();
                employee.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    ? reader["UPDATED_BY"].ToString()
    : null;
                // employee.UPDATED_DATE = Convert.ToDateTime(reader["UPDATED_DATE"]);
                employee.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
     ? Convert.ToDateTime(reader["UPDATED_DATE"])
     : (DateTime?)null;
                employee.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
                //employee.LINE_MANAGER_ID = Convert.ToInt32(reader["LINE_MANAGER_ID"]);
                //employee.LINE_MANAGER_EMAIL_ID = reader["LINE_MANAGER_EMAIL_ID"].ToString();
                //employee.EMP_LAST_LOGIN = Convert.ToDateTime(reader["EMP_LAST_LOGIN"]);
                employee.LINE_MANAGER_ID = reader["LINE_MANAGER_ID"] != DBNull.Value
    ? Convert.ToInt32(reader["LINE_MANAGER_ID"])
    : (int?)null;
                employee.LINE_MANAGER_EMAIL_ID = reader["LINE_MANAGER_EMAIL_ID"] != DBNull.Value
    ? reader["LINE_MANAGER_EMAIL_ID"].ToString()
    : null;
                employee.EMP_LAST_LOGIN = reader["EMP_LAST_LOGIN"] != DBNull.Value
    ? Convert.ToDateTime(reader["EMP_LAST_LOGIN"])
    : (DateTime?)null;
                employees.Add(employee);
            }
            con.Close();
            return Ok(employees);
        }
        [HttpGet]
        [Route("GET_ALL_CLIENT")]
        public IActionResult ShowClients()
        {
            List<TBL_CLIENT> clients = new List<TBL_CLIENT>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GETALLCLIENTS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TBL_CLIENT client = new TBL_CLIENT();
                client.CLIENT_ID = Convert.ToInt32(reader["CLIENT_ID"]);
                client.CLIENT_CODE = reader["CLIENT_CODE"].ToString();
                client.CLIENT_NAME = reader["CLIENT_NAME"].ToString();
                client.CREATED_BY = reader["CREATED_BY"].ToString();
                client.CREATED_DATE = Convert.ToDateTime(reader["CREATED_DATE"]);
                //client.UPDATED_BY = reader["UPDATED_BY"].ToString();
                //     client.UPDATED_DATE = Convert.ToDateTime(reader["UPDATED_DATE"]);
                client.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    ? reader["UPDATED_BY"].ToString()
    : null;
                client.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    ? Convert.ToDateTime(reader["UPDATED_DATE"])
    : (DateTime?)null;
                client.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
                clients.Add(client);
            }
            return Ok(clients
                );

        }

        [HttpGet]
        [Route("GET_ALL_PROJECTS")]
        public IActionResult ShowProjects()
        {
            List<TBL_PROJECTS> projects = new List<TBL_PROJECTS>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GETALLPROJECTS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TBL_PROJECTS project = new TBL_PROJECTS();
                project.PROJ_ID = Convert.ToInt32(reader["PROJ_ID"]);
                project.CLIENT_NAME = reader["CLIENT_NAME"].ToString();
                project.PROJ_CODE = reader["PROJ_CODE"].ToString();
                project.PROJ_NAME = reader["PROJ_NAME"].ToString();
                project.PROJ_DESC = reader["PROJ_DESC"].ToString();
                project.CREATED_BY = reader["CREATED_BY"].ToString();
                project.CREATED_DATE = Convert.ToDateTime(reader["CREATED_DATE"]);

                project.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    ? reader["UPDATED_BY"].ToString()
    : null;
                project.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    ? Convert.ToDateTime(reader["UPDATED_DATE"])
    : (DateTime?)null;
                project.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
                projects.Add(project);
            }
            return Ok(projects);

        }
        [HttpGet]
        [Route("GET_ALL_FUNCTIONS")]
        public IActionResult ShowFunctions()
        {
            List<TBL_FUNCTION> functions = new List<TBL_FUNCTION>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GETALLFUNNCTIONS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TBL_FUNCTION function = new TBL_FUNCTION();
                function.FUN_ID = Convert.ToInt32(reader["FUN_ID"]);
                function.ROLE_NAME = reader["ROLE_NAME"].ToString();
                function.FUN_CODE = reader["FUN_CODE"].ToString();
                function.FUN_NAME = reader["FUN_NAME"].ToString();
                // project.PROJ_DESC = reader["PROJ_DESC"].ToString();
                function.CREATED_BY = reader["CREATED_BY"].ToString();
                function.CREATED_DATE = Convert.ToDateTime(reader["CREATED_DATE"]);

                function.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    ? reader["UPDATED_BY"].ToString()
    : null;
                function.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    ? Convert.ToDateTime(reader["UPDATED_DATE"])
    : (DateTime?)null;
                function.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
                functions.Add(function);
            }
            return Ok(functions);
        }
        [HttpGet]
        [Route("GET_ALL_MODULES")]
        public IActionResult ShowModules()
        {
            List<TBL_MODULE> functions = new List<TBL_MODULE>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GETALLMODULES", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TBL_MODULE function = new TBL_MODULE();
                function.MOD_ID = Convert.ToInt32(reader["MOD_ID"]);
                function.FUN_NAME = reader["FUN_NAME"].ToString();
                function.MOD_NAME = reader["MOD_NAME"].ToString();
                // project.PROJ_DESC = reader["PROJ_DESC"].ToString();
                function.CREATED_BY = reader["CREATED_BY"].ToString();
                function.CREATED_DATE = Convert.ToDateTime(reader["CREATED_DATE"]);

                function.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    ? reader["UPDATED_BY"].ToString()
    : null;
                function.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    ? Convert.ToDateTime(reader["UPDATED_DATE"])
    : (DateTime?)null;
                function.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
                functions.Add(function);
            }
            return Ok(functions);
        }

        //Update METHODS
        [HttpPut]
        [Route("UPDATE_ROLES")]
        public IActionResult UpdateRoles(TBL_ROLE role)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpDateRole", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ROLE_ID", role.ROLE_ID);
                cmd.Parameters.AddWithValue("@ROLE_CODE", role.ROLE_CODE);
                cmd.Parameters.AddWithValue("@ROLE_NAME", role.ROLE_NAME);
                cmd.Parameters.AddWithValue("@CREATED_BY", role.CREATED_BY);
                cmd.Parameters.AddWithValue("@CREATED_DATE", role.CREATED_DATE);
                cmd.Parameters.AddWithValue("@UPDATED_BY", role.UPDATED_BY);
                cmd.Parameters.AddWithValue("@UPDATED_DATE", role.UPDATED_DATE);
                cmd.Parameters.AddWithValue("@IS_ACTIVE", role.IS_ACTIVE);
                cmd.ExecuteNonQuery();
                return Ok("Role Updated Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        [HttpPut]
        [Route("UPDATE_EMPLOYEE")]
        public IActionResult UpdateEmployee(TBL_EMPLOYEE EMPLOYEE)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure;  // ✅ REQUIRED

                    cmd.Parameters.AddWithValue("@EMP_ID", EMPLOYEE.EMP_ID);
                    cmd.Parameters.AddWithValue("@ROLE_NAME", EMPLOYEE.ROLE_NAME);
                    cmd.Parameters.AddWithValue("@EMP_CODE", EMPLOYEE.EMP_CODE);
                    cmd.Parameters.AddWithValue("@EMP_NAME", EMPLOYEE.EMP_NAME);
                    cmd.Parameters.AddWithValue("@EMP_MOBILE_NO", EMPLOYEE.EMP_MOBILE_NO);
                    cmd.Parameters.AddWithValue("@EMP_EMAIL_ID", EMPLOYEE.EMP_EMAIL_ID);
                    cmd.Parameters.AddWithValue("@EMP_PASSWORD", EMPLOYEE.EMP_PASSWORD);
                    cmd.Parameters.AddWithValue("@CREATED_BY", EMPLOYEE.CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", EMPLOYEE.CREATED_DATE);
                    cmd.Parameters.AddWithValue("@UPDATED_BY", EMPLOYEE.UPDATED_BY);
                    cmd.Parameters.AddWithValue("@UPDATED_DATE", EMPLOYEE.UPDATED_DATE);
                    cmd.Parameters.AddWithValue("@IS_ACTIVE", EMPLOYEE.IS_ACTIVE);
                    cmd.Parameters.AddWithValue("@LINE_MANAGER_ID", EMPLOYEE.LINE_MANAGER_ID);
                    cmd.Parameters.AddWithValue("@LINE_MANAGER_EMAIL_ID", EMPLOYEE.LINE_MANAGER_EMAIL_ID);
                    cmd.Parameters.AddWithValue("@EMP_LAST_LOGIN", EMPLOYEE.EMP_LAST_LOGIN);

                    cmd.ExecuteNonQuery();
                    return Ok(new { message = "Employee updated successfully" });
                }
                catch (Exception e)
                {
                    return BadRequest(new { error = e.Message }); // ✅ Safe serialization
                }
            }
        }
        [HttpPut]
        [Route("UPDATE_CLIENT")]
        public IActionResult UpdateClient(TBL_CLIENT client)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CLIENT_ID", client.CLIENT_ID);
                cmd.Parameters.AddWithValue("@CLIENT_CODE", client.CLIENT_CODE);
                cmd.Parameters.AddWithValue("@CLIENT_NAME", client.CLIENT_NAME);
                cmd.Parameters.AddWithValue("@CREATED_BY", client.CREATED_BY);
                cmd.Parameters.AddWithValue("@CREATED_DATE", client.CREATED_DATE);
                cmd.Parameters.AddWithValue("@UPDATED_BY", client.UPDATED_BY);
                cmd.Parameters.AddWithValue("@UPDATED_DATE", client.UPDATED_DATE);
                cmd.Parameters.AddWithValue("@IS_ACTIVE", client.IS_ACTIVE);
                cmd.ExecuteNonQuery();
                return Ok("Client Updated Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        [HttpPut]
        [Route("UPDATE_PROJECT")]
        public IActionResult UpdateProject(TBL_PROJECTS project)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateProject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PROJ_ID", project.PROJ_ID);
                cmd.Parameters.AddWithValue("@CLIENT_NAME", project.PROJ_NAME);
                cmd.Parameters.AddWithValue("@PROJ_CODE", project.PROJ_CODE);
                cmd.Parameters.AddWithValue("@PROJ_NAME", project.PROJ_NAME);
                cmd.Parameters.AddWithValue("@PROJ_DESC", project.PROJ_DESC);
                cmd.Parameters.AddWithValue("@CREATED_BY", project.CREATED_BY);
                cmd.Parameters.AddWithValue("@CREATED_DATE", project.CREATED_DATE);
                cmd.Parameters.AddWithValue("@UPDATED_BY", project.UPDATED_BY);
                cmd.Parameters.AddWithValue("@UPDATED_DATE", project.UPDATED_DATE);
                cmd.Parameters.AddWithValue("@IS_ACTIVE", project.IS_ACTIVE);
                cmd.ExecuteNonQuery();

                return Ok("Project Updated Successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something Wrong Please Try Again..");
            }
        }
        [HttpPut]
        [Route("UPDATE_FUNCTIONS")]
        public IActionResult UpdateProject(TBL_FUNCTION function)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_UpdateFunction", conn);
                    cmd.CommandType = CommandType.StoredProcedure;  // ✅ REQUIRED
                    cmd.Parameters.AddWithValue("@FUN_ID", function.FUN_ID);
                    cmd.Parameters.AddWithValue("@ROLE_NAME", function.ROLE_NAME);
                    cmd.Parameters.AddWithValue("@FUN_CODE", function.FUN_CODE);
                    cmd.Parameters.AddWithValue("@FUN_NAME", function.FUN_NAME);
                    cmd.Parameters.AddWithValue("@CREATED_BY", function.CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", function.CREATED_DATE);
                    cmd.Parameters.AddWithValue("@UPDATED_BY", function.UPDATED_BY);
                    cmd.Parameters.AddWithValue("@UPDATED_DATE", function.UPDATED_DATE);
                    cmd.Parameters.AddWithValue("@IS_ACTIVE", function.IS_ACTIVE);
                    cmd.ExecuteNonQuery();
                    return Ok("Project Updated Successfully.");
                }
                catch (Exception E)
                {
                    Console.WriteLine(E.Message);
                    return BadRequest("Something Wrong Please Try Again..");
                }
            }
        }

        [HttpPut]
        [Route("UPDATE_MODULES")]
        public IActionResult UpdateModule(TBL_MODULE module)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                try
                {

                    SqlCommand cmd = new SqlCommand("SP_UpdateModule", conn);
                    cmd.CommandType = CommandType.StoredProcedure;  // ✅ REQUIRED
                    cmd.Parameters.AddWithValue("@MOD_ID", module.MOD_ID);
                    cmd.Parameters.AddWithValue("@FUN_NAME", module.FUN_NAME);
                    cmd.Parameters.AddWithValue("@MOD_CODE", module.MOD_CODE);
                    cmd.Parameters.AddWithValue("@MOD_NAME", module.FUN_NAME);
                    cmd.Parameters.AddWithValue("@CREATED_BY", module.CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", module.CREATED_DATE);
                    cmd.Parameters.AddWithValue("@UPDATED_BY", module.UPDATED_BY);
                    cmd.Parameters.AddWithValue("@UPDATED_DATE", module.UPDATED_DATE);
                    cmd.Parameters.AddWithValue("@IS_ACTIVE", module.IS_ACTIVE);
                    cmd.ExecuteNonQuery();
                    return Ok("Module Updated Successfully.");
                }
                catch (Exception E)
                {
                    Console.WriteLine(E.Message);
                    return BadRequest("Something Wrong Please Try Again..");
                }
            }
        }
        [HttpPost]
        [Route("INSERT-ROLE")]
        public IActionResult AddRole(ROLEDETAILS role)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try 
            {
                SqlCommand cmd = new SqlCommand("SP_INSERTROLE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ROLE_CODE", role.ROLE_CODE);
                cmd.Parameters.AddWithValue("@ROLE_NAME", role.ROLE_NAME);
                cmd.ExecuteNonQuery();
                return Ok("Role Added Successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("INSERT-EMPLOYEE")]
        public IActionResult AddEmployee(EmployeeDetailscs employee)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_InsertEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ROLE_ID", employee.ROLE_ID);
                cmd.Parameters.AddWithValue("@EMP_CODE", employee.EMP_CODE);
                cmd.Parameters.AddWithValue("@EMP_NAME", employee.EMP_NAME);
                cmd.Parameters.AddWithValue("@EMP_MOBILE_NO", employee.EMP_MOBILE_NO);
                cmd.Parameters.AddWithValue("@EMP_EMAIL_ID", employee.EMP_EMAIL_ID);
                cmd.Parameters.AddWithValue("@EMP_PASSWORD", employee.EMP_PASSWORD);
                cmd.ExecuteNonQuery();
                return Ok("EMPLOYEE Added Successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("INSERT-CLIENT")]
        public IActionResult AddClient(ClientDetails client)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_InsertClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CLIENT_NAME", client.client_code);
                cmd.Parameters.AddWithValue("@CLIENT_CODE", client.client_name);
               
           
                cmd.ExecuteNonQuery();
                return Ok("Client Added Successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("INSERT-PROJECT")]
        public IActionResult AddProject(ProjectDetails PROJECT)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_InsertProject", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CLIENT_ID", PROJECT.CLIENT_ID);
                cmd.Parameters.AddWithValue("@PROJ_CODE", PROJECT.PROJ_CODE);
                cmd.Parameters.AddWithValue("@PROJ_NAME", PROJECT.PROJ_NAME);
                cmd.Parameters.AddWithValue("@PROJ_DESC", PROJECT.PROJ_DESCRIPTION);


                cmd.ExecuteNonQuery();
                return Ok("Project Added Successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("INSERT-FUNCTION")]
        public IActionResult AddFunction(FunctionDetails FUNCTION)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_InsertFunction", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ROLE_ID", FUNCTION.ROLE_ID);
                cmd.Parameters.AddWithValue("@FUN_CODE", FUNCTION.FUN_CODE);
                cmd.Parameters.AddWithValue("@FUN_NAME", FUNCTION.FUN_NAME);
            


                cmd.ExecuteNonQuery();
                return Ok("Function Added Successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("INSERT-MODULES")]
        public IActionResult AddModules(ModulesDetails Module)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_InsertModule", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FUN_ID", Module.FUN_ID);
                cmd.Parameters.AddWithValue("@MOD_CODE", Module.MOD_CODE);
                cmd.Parameters.AddWithValue("@MOD_NAME", Module.MOD_NAME);



                cmd.ExecuteNonQuery();
                return Ok("Module  Added Successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}