using AutoMapper;
using BlocketLiteAPI.Authentications;
using BlocketLiteAPI.Models;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlocketLiteAPI.Controllers
{
    /// <summary>
    /// LoginAndRegister Controller responsible for the POST for managing the LoginAndRegister
    /// </summary>
    [ApiController]
    public class LoginAndRegisterController : Controller
    {
        private readonly IJWTAuthenticationMananger _authenticationMananger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginAndRegisterController(IJWTAuthenticationMananger authenticationMananger,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _authenticationMananger = authenticationMananger ?? throw new ArgumentNullException(nameof(authenticationMananger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        /// <summary>
        /// This POST method takes a <see cref="LoginDto"/> as parameter
        /// If the <see cref="LoginDto.UserName"/> and <see cref="LoginDto.Password"/> correspond with 
        /// <br></br> pairing values in the DBcontext. This methode returns a <see cref="string"/> JWT-Token and an <see cref="AuthenticateForReturnDto"/>
        /// </summary>
        /// <param name="login"></param>
        /// <returns>An <see cref="OkResult"/> and a <see cref="string"/> JWT-Token and an <see cref="AuthenticateForReturnDto"/></returns>
        [Route("token")]
        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginDto login)
        {
            var user = _userRepository.GetFromUserName(login.UserName);
            if (user == null)
            {
                return NotFound();
            }
            var token = _authenticationMananger.Authentication(user, login.Password);

            // TODO Expires_in in the AuthenticateForReturnDto, what to do?
            AuthenticateForReturnDto authenticateForReturnDto = new AuthenticateForReturnDto()
            {
                Access_Token = token,
                UserName = user.UserName,
                Issued = DateTime.Now,
                Expires = DateTime.Now.AddHours(1)
            };
            return Ok(authenticateForReturnDto);
        }


        /// <summary>
        /// This POST method takes a <see cref="UserForCreationDto"/> as parameter.
        /// Creates an <see cref="User"/> and stores it in the DBContext 
        /// <br></br><see cref="UserForCreationDto.UserName"/> can not exist in the DB on aother <see cref="User"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <returns><see cref="OkResult"/></returns>
        [Route("api/account/register")]
        [HttpPost]
        public ActionResult<UserDto> Register(UserForCreationDto user)
        {
            var ifUserExists = _userRepository.GetFromUserName(user.UserName);
            if (ifUserExists != null)
            {
                return BadRequest();
            }

            var userEntity = _mapper.Map<User>(user);
            _userRepository.Add(userEntity);
            _userRepository.Save();

            return NoContent();
        }
    }
}
