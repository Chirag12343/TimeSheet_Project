using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TimeSheet_Project.Models;

namespace TimeSheet_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _connection;

        public AuthController(IConfiguration config)
        {
            _connection = config.GetConnectionString("conn");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            using SqlConnection conn = new SqlConnection(_connection);
            string query = "SELECT EMP_ID, ROLE_ID FROM TBL_EMPLOYEE WHERE EMP_EMAIL_ID = @EMP_EMAIL_ID AND EMP_PASSWORD = @EMP_PASSWORD";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EMP_EMAIL_ID", login.EMP_EMAIL_ID);
            cmd.Parameters.AddWithValue("@EMP_PASSWORD", login.EMP_PASSWORD);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            SqlCommand sqlcmd = new SqlCommand("SP_GetAllModulenames", conn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@Function_Name", functionname);
            SqlDataReader read = sqlcmd.ExecuteReader();
            while (read.Read())
            {
                GetAllModules module = new GetAllModules();
                module.Module = read["MOD_NAME"].ToString();
                getAllModules.Add(module);
            }
            if (reader.Read())
            {
                int empId = reader.GetInt32(0);
                int roleId = reader.GetInt32(1);

                // Store in Session
                HttpContext.Session.SetInt32("EMP_ID", empId);
                HttpContext.Session.SetInt32("ROLE_ID", roleId);


                return Ok(new { Message = "Login successful", EMP_ID = empId, ROLE_ID = roleId, });
            }
            else
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { Message = "Logged out" });
        }
    }
    public class LoginRequest
    {
        public string EMP_EMAIL_ID { get; set; }
        public string EMP_PASSWORD { get; set; }
    }
}
