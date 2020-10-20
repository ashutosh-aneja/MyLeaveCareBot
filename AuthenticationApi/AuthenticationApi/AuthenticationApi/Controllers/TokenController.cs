using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenController));
        private IConfiguration _config;
        private readonly TestLeaveManagementContext _context;
        public TokenController(IConfiguration config, TestLeaveManagementContext context)
        {
            _config = config;
            _context = context;
        }
       
        [HttpPost]
        public IActionResult Login(User login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.email),
                new Claim(ClaimTypes.NameIdentifier, userInfo.email.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            if (userInfo.email == "HR@LeaveCare")
            {
                claims.Add(new Claim(ClaimTypes.Role, "HR"));
            }

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(User login)
        {
            User user = null;

            if (login.email.Equals("HR@LeaveCare") && login.password.Equals("9090"))
            {
                //  user = new Authenticate { Username = "admin", Password = "admin" };
                return login;
            }

           // (login.email)

            var employeeDetail =  _context.EmployeeDetails.FirstOrDefault(c=>c.EmpEmail==login.email);
            if (employeeDetail != null)
            {
                if (login.email.Equals(employeeDetail.EmpEmail) && login.password.Equals(employeeDetail.EmpPass))
                {
                    //  user = new Authenticate { Username = "admin", Password = "admin" };
                    return login;
                }
            }
            else
            {
                return user;
            }


            return user;
        }
    }
}