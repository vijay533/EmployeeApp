using EmployeeDb.Context;
using EmployeeDb.Filters;
using EmployeeDb.Migrations;
using EmployeeDb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtClaimsController : ControllerBase
    {
        IConfiguration _config;
        CompanyContext context;

        public JwtClaimsController(IConfiguration configuration,CompanyContext context)
        {
            _config = configuration;
            this.context = context;
        }
        // GET: api/<JwtController>

        [HttpGet]
        [Authorize]
        public IEnumerable<Employee> Get()
        {
            //throw exception method level
            //throw new Exception("this is error");
            return context.employees.ToList();
        }

        // GET api/<JwtController>/5
        [HttpGet("{id}")]
        [Authorize]

        public IActionResult Get([FromQuery] int id)
        {

            var t = HttpContext.User;
            var t1 = HttpContext.User.Identity as ClaimsIdentity;
            var t2 = HttpContext.User.Claims.ToList();
            foreach (var j in t2)
            {
                Debug.WriteLine(j.Value);
            }


            var i = (from x in context.employees where x.Id == id select x).FirstOrDefault();
            if (i == null)
            {
                throw new Exception("User Not found");
                // return NotFound("User Not Found");
            }
            return Ok(i);

        }

        // POST api/<JwtController>
        [HttpPost]
        public IActionResult Post([FromQuery] string name,string password)
        {
            IActionResult response = Unauthorized();
            if (name == "vijay")
            {
                string token = GenerateJSONWebToken(name,password);
                response = Ok(new { tokenstring = token });
            }
            return response;
        }
        private string GenerateJSONWebToken(string name,string password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,name),
             //   new Claim(JwtRegisteredClaimNames.NameId,obj.id.ToString()),
                new Claim("password",password),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        // PUT api/<JwtClaimsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public string Put(int id, [FromBody] Employee emp)
        {
           
                var i = context.employees.Where(x => x.Id == id).FirstOrDefault();
                if (i == null)
                    throw new Exception();
                i.Name = emp.Name;
                i.age = emp.age;
                i.Nationality = emp.Nationality;
                i.city = emp.city;
                context.SaveChanges();
                return "updated succesfully";
            
        }

        // DELETE api/<JwtClaimsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public string Delete(int id)
        {
            var i = context.employees.Where(x => x.Id == id).FirstOrDefault();
            context.employees.Remove(i);
            context.SaveChanges();
            return "Deleted";
        }
    }
}
