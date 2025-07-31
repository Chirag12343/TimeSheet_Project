using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Reflection;
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
                timesheet.TIMESHEET_DATE = read.GetDateTime(0);
                timesheet.EMP_NAME = read.GetString(1);
                timesheet.TIMESLOT = read.GetString(2);
                timesheet.HOURS = read.GetInt32(3);
                timesheet.PROJ_NAME = read.GetString(4);
                timesheet.FUN_NAME = read.GetString(5);
                timesheet.MOD_NAME = read.GetString(6);
                timesheet.TIME_FROM = read.GetString(7);
                timesheet.TIME_TO = read.GetString(8);
                timesheet.TIMESHEET_DESC = read.GetString(9);
                timesheet.CREATED_BY = read.GetString(10);
                timesheet.CREATED_DATE = read.GetDateTime(11);
                Timesheets.Add(timesheet);
            }
            conn.Close();
            return Ok(Timesheets);
        }


        [HttpGet]
        [Route("GET_ALL_ROLES")]
        public IActionResult ShowROles()
        {
            string OPERATION_TYPE = "VIEWROLES";
            List<TBL_ROLE> roles = new List<TBL_ROLE>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_ManageRoles", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation_type",OPERATION_TYPE);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TBL_ROLE role = new TBL_ROLE();
                role.ROLE_ID = Convert.ToInt32(reader["ROLE_ID"]);
                role.ROLE_CODE = reader["ROLE_CODE"].ToString();
                role.ROLE_NAME = reader["ROLE_NAME"].ToString();
                role.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
                roles.Add(role);
            }
            return Ok(roles);
        }


        [HttpGet]
        [Route("GET_ALL_EMPLOYEE")]
        public IActionResult ShowEmployees()
        {
            String OPERATION_TYPE = "VIEWALLEMPLOYEE";
            List<TBL_EMPLOYEE> employees = new List<TBL_EMPLOYEE>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_ManageEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation_type", OPERATION_TYPE);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TBL_EMPLOYEE employee = new TBL_EMPLOYEE();
                employee.EMP_ID = Convert.ToInt32(reader["EMP_ID"]);
                employee.ROLE_NAME = reader["ROLE_NAME"].ToString();
                employee.EMP_CODE = reader["EMP_CODE"].ToString();
                employee.EMP_NAME = reader["EMP_NAME"].ToString();
                employee.EMP_EMAIL_ID = reader["EMP_EMAIL_ID"].ToString();
                employee.EMP_MOBILE_NO = Convert.ToInt64(reader["EMP_MOBILE_NO"]);
                employee.EMP_PASSWORD = reader["EMP_PASSWORD"].ToString();
                employee.CREATED_BY = reader["CREATED_BY"].ToString();
                employee.CREATED_DATE = Convert.ToDateTime(reader["CREATED_DATE"]);            
    //            employee.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    //? reader["UPDATED_BY"].ToString()
    //: null;
    //            employee.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    // ? Convert.ToDateTime(reader["UPDATED_DATE"])
    // : (DateTime?)null;
                employee.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
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
            String OPERATION_TYPE = "VIEWCLIENTS";
            List<TBL_CLIENT> clients = new List<TBL_CLIENT>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_ManageClient", con);
            cmd.Parameters.AddWithValue("@operation_Type", OPERATION_TYPE);
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
    //            client.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    //? reader["UPDATED_BY"].ToString()
    //: null;
    //            client.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    //? Convert.ToDateTime(reader["UPDATED_DATE"])
    //: (DateTime?)null;
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
            string operation_type = "VIEWALLPROJECTS";
            List<TBL_PROJECTS> projects = new List<TBL_PROJECTS>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_ManageProjects", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation_type",operation_type);
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
                DateTime dateTime = (DateTime)reader["CREATED_DATE"];
                DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
                project.CREATED_DATE = dateOnly;
    //            project.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    //? reader["UPDATED_BY"].ToString()
    //: null;

    //            project.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    //? Convert.ToDateTime(reader["UPDATED_DATE"])
    //: (DateTime?)null;
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
            string operation_type = "VIEWALLFUNCTION";
            SqlCommand cmd = new SqlCommand("SP_ManageFunctions", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation_type",operation_type);
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

    //            function.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    //? reader["UPDATED_BY"].ToString()
    //: null;
    //            function.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    //? Convert.ToDateTime(reader["UPDATED_DATE"])
    //: (DateTime?)null;
                function.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
                functions.Add(function);
            }
            return Ok(functions);
        }


        [HttpGet]
        [Route("GET_ALL_MODULES")]
        public IActionResult ShowModules()
        {
            string OPERATION_TYPE = "VIEWALLMODULES";
            List<TBL_MODULE> functions = new List<TBL_MODULE>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_ManageModules", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation_type", OPERATION_TYPE);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TBL_MODULE function = new TBL_MODULE();
                function.MOD_ID = Convert.ToInt32(reader["MOD_ID"]);
                function.FUN_NAME = reader["FUN_NAME"].ToString();
                function.MOD_NAME = reader["MOD_NAME"].ToString();
                function.MOD_CODE = reader["MOD_CODE"].ToString();
    //            function.CREATED_BY = reader["CREATED_BY"].ToString();
    //            function.CREATED_DATE = Convert.ToDateTime(reader["CREATED_DATE"]);

    //            function.UPDATED_BY = reader["UPDATED_BY"] != DBNull.Value
    //? reader["UPDATED_BY"].ToString()
    //: null;
    //            function.UPDATED_DATE = reader["UPDATED_DATE"] != DBNull.Value
    //? Convert.ToDateTime(reader["UPDATED_DATE"])
    //: (DateTime?)null;
                function.IS_ACTIVE = Convert.ToByte(reader["IS_ACTIVE"]);
                functions.Add(function);
            }
            return Ok(functions);
        }


        [HttpGet]
        [Route("GET_ALL_TIMESLOT")]
        public IActionResult getAllTimeslot()
        {
            string OPERATION_TYPE = "VIEWALLTIMESLOT";
            List<Timeslot> slots = new List<Timeslot>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_ManageTimeSlot", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation_type", OPERATION_TYPE);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Timeslot timeslot = new Timeslot();
                timeslot.SLOT_ID = Convert.ToInt32(reader["SLOT_ID"]);
                timeslot.TIMESLOT = reader["TIMESLOT"].ToString();

                slots.Add(timeslot);
            }
            return Ok(slots);
        }

        //Update METHODS
        [HttpPut]
        [Route("UPDATE_ROLES")]
        public IActionResult UpdateRoles(ROLEDETAILS role)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();
            try
            {
                string OPERATION_TYPE = "UPDATEROLE";
                SqlCommand cmd = new SqlCommand("SP_ManageRoles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_type", OPERATION_TYPE);
                cmd.Parameters.AddWithValue("@ROLE_ID", role.ROLE_ID);
                cmd.Parameters.AddWithValue("@ROLE_CODE", role.ROLE_CODE);
                cmd.Parameters.AddWithValue("@ROLE_NAME", role.ROLE_NAME);

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
        public IActionResult UpdateEmployee(EmployeeUpdate EMPLOYEE)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                try
                {
                    string operation_type = "UPDATEEMPLOYEE";
                    SqlCommand cmd = new SqlCommand("SP_ManageEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure;  // ✅ REQUIRED
                    cmd.Parameters.AddWithValue("@operation_type", operation_type);
                    cmd.Parameters.AddWithValue("@EMP_ID", EMPLOYEE.EMP_ID);
                    cmd.Parameters.AddWithValue("@ROLE_ID", EMPLOYEE.ROLE_ID);
                    cmd.Parameters.AddWithValue("@EMP_CODE", EMPLOYEE.EMP_CODE);
                    cmd.Parameters.AddWithValue("@EMP_NAME", EMPLOYEE.EMP_NAME);
                    cmd.Parameters.AddWithValue("@EMP_MOBILE_NO", EMPLOYEE.EMP_MOBILE_NO);
                    //cmd.Parameters.AddWithValue("@UPDATED_DATE", DateTime.Now);

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
        public IActionResult UpdateClient(UpdateClient client)
        {
            SqlConnection conn = new SqlConnection(connection);
            String OPERATION_TYPE = "UPDATECLIENT";
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ManageClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_Type",OPERATION_TYPE);
                cmd.Parameters.AddWithValue("@CLIENT_ID", client.CLIENT_ID);
                cmd.Parameters.AddWithValue("@CLIENT_CODE", client.CLIENT_CODE);
                cmd.Parameters.AddWithValue("@CLIENT_NAME", client.CLIENT_NAME);
                cmd.Parameters.AddWithValue("@UPDATED_DATE", DateTime.Now);
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
        public IActionResult UpdateProject(UpdateProject project)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();
            try
            {
                string operation_type = "UPDATEPROJECT";
                SqlCommand cmd = new SqlCommand("SP_ManageProjects", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_type", operation_type);
                cmd.Parameters.AddWithValue("@PROJ_ID", project.PROJ_ID);
                cmd.Parameters.AddWithValue("@CLIENT_ID", project.CLIENT_ID);
                cmd.Parameters.AddWithValue("@PROJ_CODE", project.PROJ_CODE);
                cmd.Parameters.AddWithValue("@PROJ_NAME", project.PROJ_NAME);
                cmd.Parameters.AddWithValue("@PROJ_DESC", project.PROJ_DESC);
                //cmd.Parameters.AddWithValue("@CREATED_BY", project.CREATED_BY);
                //cmd.Parameters.AddWithValue("@CREATED_DATE", project.CREATED_DATE);
                //cmd.Parameters.AddWithValue("@UPDATED_BY", project.UPDATED_BY);
                ////cmd.Parameters.AddWithValue("@UPDATED_DATE", DateTime.Now);
                //cmd.Parameters.AddWithValue("@IS_ACTIVE", project.IS_ACTIVE);
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
        public IActionResult UpdateProject(UpdateFunction function)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                try
                {
                    string operation_type = "UPDATEFUNCTION";
                    SqlCommand cmd = new SqlCommand("SP_ManageFunctions", conn);
                    cmd.CommandType = CommandType.StoredProcedure;  // ✅ REQUIRED
                    cmd.Parameters.AddWithValue("@operation_type", operation_type);
                    cmd.Parameters.AddWithValue("@FUN_ID", function.FUN_ID);
                    cmd.Parameters.AddWithValue("@ROLE_ID", function.ROLE_ID);
                    cmd.Parameters.AddWithValue("@FUN_CODE", function.FUN_CODE);
                    cmd.Parameters.AddWithValue("@FUN_NAME", function.FUN_NAME);
                    cmd.Parameters.AddWithValue("@UPDATED_DATE", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    return Ok("Function Updated Successfully.");
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
        public IActionResult UpdateModule(UpdateModule module)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                try
                {

                    string operation_type = "UPDATEMODULE";
                    SqlCommand cmd = new SqlCommand("SP_ManageModules", conn);
                    cmd.CommandType = CommandType.StoredProcedure;  // ✅ REQUIRED
                    cmd.Parameters.AddWithValue("@MOD_ID", module.MOD_ID);
                    cmd.Parameters.AddWithValue("@operation_type", operation_type);
                    cmd.Parameters.AddWithValue("@FUN_ID", module.FUN_ID);
                    cmd.Parameters.AddWithValue("@MOD_CODE", module.MOD_CODE);
                     cmd.Parameters.AddWithValue("@MOD_NAME", module.MOD_NAME);
                    //cmd.Parameters.AddWithValue("@CREATED_BY", module.CREATED_BY);
                    //cmd.Parameters.AddWithValue("@CREATED_DATE", module.CREATED_DATE);
                    //cmd.Parameters.AddWithValue("@UPDATED_BY", module.UPDATED_BY);
                    //cmd.Parameters.AddWithValue("@UPDATED_DATE", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@IS_ACTIVE", module.IS_ACTIVE);
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


        [HttpPut]
        [Route("UPDATE_TIMESLOT")]
        public IActionResult UpdateTimeslot(Timeslot slot)
        {
            SqlConnection conn = new SqlConnection(connection);

            conn.Open();
            try
            {
                string operation_type = "UPDATETIMESLOT";
                SqlCommand cmd = new SqlCommand("SP_ManageTimeSlot", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_type",operation_type);
                cmd.Parameters.AddWithValue("@SLOT_ID", slot.SLOT_ID);
                cmd.Parameters.AddWithValue("@TIMESLOT", slot.TIMESLOT);

                cmd.ExecuteNonQuery();
                return Ok(new { message = "Timeslot updated successfully" });
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


        [HttpPost]
        [Route("INSERT-ROLE")]
        public IActionResult AddRole(RoleInsert role)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                string OPERATION_TYPE = "INSERTROLE";
                SqlCommand cmd = new SqlCommand("SP_ManageRoles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_type", OPERATION_TYPE);
                cmd.Parameters.AddWithValue("@ROLE_CODE", role.ROLE_CODE);
                cmd.Parameters.AddWithValue("@ROLE_NAME", role.ROLE_NAME);
                //cmd.Parameters.AddWithValue("@UPDATED_DATE", DateTime.Now);
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
        [Route("INSERT-TIMESLOT")]
        public IActionResult AddTimeslot(TIMESLOTDETAILS timeslot)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                string operation_type = "INSERTTIMESLOT";
                SqlCommand cmd = new SqlCommand("SP_ManageTimeSlot", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_type",operation_type);
                cmd.Parameters.AddWithValue("@TIMESLOT", timeslot.TIMESLOT);
                cmd.ExecuteNonQuery();
                return Ok(new { message = "Timeslot saved" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                string operation_type = "INSERTEMPLOYEE";
                SqlCommand cmd = new SqlCommand("SP_ManageEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_type",operation_type);
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
                String OPERATION_TYPE = "INSERTCLIENT";
                SqlCommand cmd = new SqlCommand("SP_ManageClient", conn);
                cmd.Parameters.AddWithValue("@operation_Type",OPERATION_TYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CLIENT_NAME", client.client_name);
                cmd.Parameters.AddWithValue("@CLIENT_CODE", client.client_code);


                cmd.ExecuteNonQuery();
                return Ok(new { message = "Client Added Successfully." });
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
                string operation_type = "INSERTPROJECT";
                SqlCommand cmd = new SqlCommand("SP_ManageProjects", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_type", operation_type);
                cmd.Parameters.AddWithValue("@PROJ_NAME", PROJECT.PROJ_NAME);
                cmd.Parameters.AddWithValue("@PROJ_CODE", PROJECT.PROJ_CODE);
                cmd.Parameters.AddWithValue("@PROJ_DESC", PROJECT.PROJ_DESCRIPTION);
                cmd.Parameters.AddWithValue("@CLIENT_ID", PROJECT.CLIENT_ID);

                cmd.ExecuteNonQuery();
                return Ok(new { message = "Project Added Successfully." });
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
                string operation_type = "INSERTFUNCTION";
                SqlCommand cmd = new SqlCommand("SP_ManageFunctions", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_type", operation_type);
                cmd.Parameters.AddWithValue("@ROLE_ID", FUNCTION.ROLE_ID);
                cmd.Parameters.AddWithValue("@FUN_CODE", FUNCTION.FUN_CODE);
                cmd.Parameters.AddWithValue("@FUN_NAME", FUNCTION.FUN_NAME);



                cmd.ExecuteNonQuery();
                return Ok(new { message = "Function Added Successfully." });
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
                string operation_type = "INSERTMODULE";
                SqlCommand cmd = new SqlCommand("SP_ManageModules", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@operation_type", operation_type);
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


        [HttpDelete]
        [Route("DeleteRole/{RoleId}")]
        public IActionResult DeleteRole(int RoleId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                string OPERATION_TYPE = "DELETEROLE";
                SqlCommand sqlCommand = new SqlCommand("SP_ManageRoles", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@operation_type", OPERATION_TYPE);
                sqlCommand.Parameters.AddWithValue("@ROLE_ID", RoleId);
                sqlCommand.ExecuteNonQuery();
                return Ok("Role Deleted SuccessFully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("SomeThing Wrong Please Try Again..");
            }
        }


        [HttpDelete]
        [Route("DeleteEmployee/{EmployeeID}")]
        public IActionResult DeleteEmployee(int EmployeeID)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                string operation_type = "DELETEEMPLOYEE";
                SqlCommand sqlCommand = new SqlCommand("SP_ManageEmployee", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EMP_ID", EmployeeID);
                sqlCommand.Parameters.AddWithValue("@operation_type",operation_type);
                sqlCommand.ExecuteNonQuery();
                return Ok(new { success = true, message = "Employee deleted" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("SomeThing Wrong Please Try Again..");
            }

        }


        [HttpDelete]
        [Route("DeleteClient/{ClientId}")]
        public IActionResult DeleteClient(int ClientId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                String OPERATION_TYPE = "DELETECLIENT";
                SqlCommand sqlCommand = new SqlCommand("SP_ManageClient", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@operation_Type",OPERATION_TYPE);
                sqlCommand.Parameters.AddWithValue("@CLIENT_ID", ClientId);
                sqlCommand.ExecuteNonQuery();
                return Ok("Client Deleted SuccessFully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("SomeThing Wrong Please Try Again..");
            }

        }


        [HttpDelete]
        [Route("DeleteProject/{ProjectId}")]
        public IActionResult DeleteProject(int ProjectId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                string operation_type = "DELETEPROJECT";
                SqlCommand sqlCommand = new SqlCommand("SP_ManageProjects", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@operation_type", operation_type);
                sqlCommand.Parameters.AddWithValue("@PROJ_ID", ProjectId);
                sqlCommand.ExecuteNonQuery();
                return Ok("Project Deleted SuccessFully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("SomeThing Wrong Please Try Again..");
            }

        }


        [HttpDelete]
        [Route("DeleteFunction/{FunctionId}")]
        public IActionResult DeleteFunction(int FunctionId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                string operation_type = "DELETEFUNCTION";
                SqlCommand sqlCommand = new SqlCommand("SP_ManageFunctions", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@operation_type", operation_type);
                sqlCommand.Parameters.AddWithValue("@FUN_ID", FunctionId);
                sqlCommand.ExecuteNonQuery();
                return Ok("Function Deleted SuccessFully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("SomeThing Wrong Please Try Again..");
            }

        }


        [HttpDelete]
        [Route("DeleteModule/{ModuleId}")]
        public IActionResult DeleteModule(int ModuleId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                string operation_type = "DELETEMODULE";
                SqlCommand sqlCommand = new SqlCommand("SP_ManageModules", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@operation_type", operation_type);
                sqlCommand.Parameters.AddWithValue("@MOD_ID", ModuleId);
                sqlCommand.ExecuteNonQuery();
                return Ok("Module Deleted SuccessFully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("SomeThing Wrong Please Try Again..");
            }

        }



        [HttpGet]
        [Route("get_roles")]
        public IActionResult GetRoles()
        {
            List<GetRoles> roles = new List<GetRoles>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GETROLES", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                GetRoles role = new GetRoles();
                role.ROLE_ID = Convert.ToInt32(reader["ROLE_ID"]);
                role.ROLE_NAME = reader["ROLE_NAME"].ToString();
                roles.Add(role);

            }
            return Ok(roles);
        }


        [HttpGet]
        [Route("get_clients")]
        public IActionResult GetClients()
        {
            List<GETALLCLIENTS> CLIENTS = new List<GETALLCLIENTS>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            String OPERATION_TYPE = "GETCLIENTS";
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operation_Type",OPERATION_TYPE);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                GETALLCLIENTS CLIENT = new GETALLCLIENTS();
                CLIENT.CLIENT_ID = Convert.ToInt32(reader["CLIENT_ID"]);
                CLIENT.CLIENT_NAME = reader["CLIENT_NAME"].ToString();
                CLIENTS.Add(CLIENT);

            }
            return Ok(CLIENTS);
        }



        [HttpGet]
        [Route("get_function")]
        public IActionResult Getallfunctions()
        {
            List<GetAllFunctions> functions = new List<GetAllFunctions>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GETALLFUNCTIONS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                GetAllFunctions function = new GetAllFunctions();
                function.FUN_ID = Convert.ToInt32(reader["FUN_ID"]);
                function.FUN_NAME = reader["FUN_NAME"].ToString();
                functions.Add(function);

            }
            return Ok(functions);
        }

        [HttpGet]
        [Route("Get_All_SlotDetails")]
        public IActionResult Getallfunctionsall()
        {
            List<TimeSloatdetails> timesdetails = new List<TimeSloatdetails>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GetSlot", conn);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                TimeSloatdetails slotdata = new TimeSloatdetails();
                slotdata.SLOT_ID = Convert.ToInt32(read["SLOT_ID"]);
                slotdata.SLOT_NAME = read["TIMESLOT"].ToString();
                timesdetails.Add(slotdata);
            }
            return Ok(timesdetails);
        }
        [HttpDelete]
        [Route("Delete_TimeSlot")]
        public IActionResult DeleteTimeSlot(int TimeSlotId)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            try
            {
                string operation_type = "DELETETIMESLOT";
                SqlCommand sqlCommand = new SqlCommand("SP_ManageTimeSlot", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@operation_type", operation_type);
                sqlCommand.Parameters.AddWithValue("@SLOT_ID", TimeSlotId);
                sqlCommand.ExecuteNonQuery();
                return Ok("TimeSlot Deleted SuccessFully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("SomeThing Wrong Please Try Again..");
            }


        }
    }
}