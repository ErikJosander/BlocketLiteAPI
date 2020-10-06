using BlocketLiteAPI.Models;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlocketLiteAPI.Controllers
{
   
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthenticateController(UserManager<User> userManager,
            IConfiguration configuration,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("token")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Login([FromForm] IFormCollection value)
        {
            var model = new LoginDto();
            model.UserName = value["UserName"];
            model.Password = value["Password"];
            var user = _userRepository.GetFromUserName(model.UserName);
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {             
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                TimeSpan ts = token.ValidTo - DateTimeOffset.UtcNow;
                return Ok(new
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    token_type = "bearer",
                    expires_in = ts.TotalSeconds,
                    userName = model.UserName,
                    issued = DateTimeOffset.UtcNow,
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("api/account/register")]
        [Consumes("application/x-www-form-urlencoded")]                       
        public async Task<IActionResult> Register([FromForm] IFormCollection value)
        {
            var model = new UserForCreationDto();
            model.UserName = value["UserName"];
            model.Email = value["Email"];
            model.Password = value["Password"];
            model.ConfirmPassword = value["ConfirmPassword"];

            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Username allready exists." });

            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email allready exists." });

            User user = new User()
            {              
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Password = model.Password
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
