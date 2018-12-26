using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Geek.OAuthServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Geek.OAuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private JwtSetting _jwtSetting;

        public AuthorizeController(IOptions<JwtSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }

        // GET api/values
        [HttpPost]
        public IActionResult Get(LoginModule login)
        {
            if (ModelState.IsValid)
            {
                if (!(login.UserName == "geekfm" && login.PassWord == "123qweA"))
                {
                    return BadRequest();
                }

                var claims = new Claim[]{
                    new Claim(ClaimTypes.Name,"geekfm"),
                    //new Claim(ClaimTypes.Role,"admin"),
                    new Claim("Admin","true"),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _jwtSetting.Issuer,
                    _jwtSetting.Audience,
                    claims,
                    null,
                    DateTime.Now.AddMinutes(120),
                    credentials
                );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest();
        }
    }
}