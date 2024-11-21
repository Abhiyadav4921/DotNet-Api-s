using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public RegController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Register(RegistrationModel registration)
        {

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string query = "INSERT INTO [dbo].[Register] ([firstName], [lastName], [email], [password], [gender], [maritalStatus], [dob], [address], [city], [state], [country], [pincode]) " +
                                   "VALUES (@FirstName, @LastName, @Email, @Password, @Gender, @MaritalStatus, @Dob, @Address, @City, @State, @Country, @Pincode)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FirstName", registration.firstName);
            cmd.Parameters.AddWithValue("@LastName", registration.lastName);
            cmd.Parameters.AddWithValue("@Email", registration.email);
            cmd.Parameters.AddWithValue("@Password", registration.password);
            cmd.Parameters.AddWithValue("@Gender", registration.gender);
            cmd.Parameters.AddWithValue("@MaritalStatus", registration.maritalStatus);
            cmd.Parameters.AddWithValue("@Dob", registration.dob);
            cmd.Parameters.AddWithValue("@Address", registration.address);
            cmd.Parameters.AddWithValue("@City", registration.city);
            cmd.Parameters.AddWithValue("@State", registration.state);
            cmd.Parameters.AddWithValue("@Country", registration.country);
            cmd.Parameters.AddWithValue("@Pincode", registration.pincode);

            con.Open();
           
            
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return Ok("Registration successful");
            }
            else
            {
                return BadRequest("Registration failed");
            }
            




        }
    }
    
}
