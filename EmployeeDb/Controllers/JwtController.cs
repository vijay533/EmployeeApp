using EmployeeDb.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Exception filter controller level
    [ExceptionFilCtrlLevel]
    public class JwtController : ControllerBase
    {
        IConfiguration _config;

        public JwtController(IConfiguration configuration)
        {
            _config = configuration;
        }
        // GET: api/<JwtController>
        [HttpGet]
        
        public IEnumerable<string> Get()
        {
            // throwing exception
           // throw new Exception();
            return new string[] { "value1", "value2" };
        }

        // GET api/<JwtController>/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            var t = HttpContext.User;
            return "value";
        }

        // POST api/<JwtController>
        [HttpPost]
        public IActionResult Post([FromQuery] int value)
        {
            IActionResult response = Unauthorized();
            if(value==10)
            {
                string token = GenerateJSONWebToken(value);
                response= Ok(new {tokenstring=token});
            }
            return response;
        }
        private string GenerateJSONWebToken(int userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // PUT api/<JwtController>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JwtController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}
