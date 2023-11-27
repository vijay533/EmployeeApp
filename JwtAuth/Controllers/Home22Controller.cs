using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtAuth.Controllers
{
    [Route("aa")]
    [ApiController]
    public class Home22Controller : ApiControllerAttribute
    {
        IConfiguration _config;
        public Home22Controller(IConfiguration configuration) 
        {
            _config = configuration;
        }
        [Route("aa")]
        public string Index()
        {
            return "gwknfwon";
        }
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = UnauthorizedObjectResult();
            var user = new UserModel { Username = "Jignesh Trivedi", EmailAddress = "test.btest@gmail.com" };
            ;

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = OkObjectResult(new { token = tokenString });
            }

            return response;
           
        }

        private string GenerateJSONWebToken(UserModel userInfo)
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
    }
   
}
