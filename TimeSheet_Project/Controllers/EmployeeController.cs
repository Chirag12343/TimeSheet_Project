using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TimeSheet_Project.Models;

namespace TimeSheet_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly string _connection;
        public EmployeeController(IConfiguration config)
        {

            _connection = config.GetConnectionString("conn");

        }


        [HttpPost]
        [Route("TimeSheet_Login")]
        public IActionResult Login(LoginDetails details)
        {
            List<Function> functions = new List<Function>();
            SqlConnection con = new SqlConnection(_connection);
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("SP_GetFunctions", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", details.Email);
                cmd.Parameters.AddWithValue("@PassWord", details.Password);
                //cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Function function = new Function();
                    function.Functions = reader["FUN_NAME"].ToString();
                    functions.Add(function);

                }

                return Ok(functions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("SomeThing Wrong Please Try Again");
            }
            finally
            {
                con.Close();
            }
            //var result=  _tBL_EMPLOYEE.Login(details);
            //if (result != null)
            //{
            //    return Ok(result);
            //}

            //else
            //{
            //    return BadRequest("Something Wrong Please Try Again ");
            //}
        }
        [HttpPost]
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
                project.Client_code = read["CLIENT_CODE"].ToString();
                project.Proj_name = read["PROJ_NAME"].ToString();
                projects.Add(project);
              
            }
            conn.Close();
            return Ok(projects);
        }
        [HttpPost]
        [Route("Get-All_Modules")]
        public IActionResult GetAllModules(string functionname)
        { 
          List<GetAllModules> getAllModules = new List<GetAllModules>();
            SqlConnection conn = new SqlConnection(_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GetAllModulenames", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Function_Name", functionname);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read()) 
            {
             GetAllModules module=new GetAllModules();
                module.Module = read["MOD_NAME"].ToString();
                getAllModules.Add(module);
            }
            conn.Close();
            return Ok(getAllModules);

        }
    }
}
