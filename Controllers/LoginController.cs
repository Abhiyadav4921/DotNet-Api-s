using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class LoginController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;// ?? throw new ArgumentNullException(nameof(configuration));

        }
        [HttpGet]
        [Route("Login")]
        public JsonResult GetEmp()
        {
            var STR = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlDataAdapter da = new SqlDataAdapter("SELECT EMAIL,PASSWORD FROM [REGISTER]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Login> empList = new List<Login>();
            Responce res = new Responce();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Login check = new Login();
                    check.userName = dt.Rows[i]["email"].ToString();
                    check.password = dt.Rows[i]["password"].ToString();
                   
                    empList.Add(check);
                }
            }

            if (empList.Count > 0)
                return new JsonResult(empList);
            else
            {
                res.StatusCode = 404;
                res.ErrorMessage = "No Record Found";
                return new JsonResult(res);
            }

        }
    }
}
