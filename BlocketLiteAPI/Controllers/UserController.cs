using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteAPI.Models.User;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BlocketLiteAPI.Controllers
{
    /// <summary>
    /// User Controller responsible for GET/POST for managing the users
    /// </summary>
    [EnableCors("AllowEverything")]
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;


        public UserController(IUserRepository userRepository,
            IAdvertisementRepository advertisementRepository,
            IRatingRepository ratingRepository,
            ICommentRepository commentRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

            _advertisementRepository = advertisementRepository ?? throw new ArgumentNullException(nameof(advertisementRepository));

            _ratingRepository = ratingRepository ?? throw new ArgumentNullException(nameof(ratingRepository));

            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        /// <summary>
        /// This GET methode returns a list of <see cref="UserDto"/>
        /// <br></br>mapped from the <see cref="User"/>
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="OkResult"/> and a <see cref="List{T}"/> of <see cref="UserDto"/></returns>
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers(string userName)
        {
            var usersFromRepo = _userRepository.GetAll(userName);
            List<UserDto> userList = new List<UserDto>();
            foreach (User user in usersFromRepo)
            {
                var userDto = (_mapper.Map<UserDto>(user));
                userDto.Comments = _commentRepository.GetNumberOfComments(user.Id);
                userDto.RealEstates = _advertisementRepository.GetNumberOfProperties(user.Id);
                userDto.Rating = _ratingRepository.GetAvarageRating(user.Id);
                userList.Add(userDto);
            }
            return Ok(userList);
        }

        /// <summary>
        /// This GET method returns an <see cref="OkResult"/> and an <see cref="UserDto"/> if the <see cref="User.UserName"/>
        /// <br></br>is equal to the <paramref name="USERNAME"/>
        /// </summary>
        /// <param name="USERNAME"></param>
        /// <returns><see cref="OkResult"/> and an <see cref="UserDto"/></returns>
        [HttpGet("{USERNAME}")]
        public IActionResult GetUser(string USERNAME)
        {
            var userFromRepo = _userRepository.GetFromUserName(USERNAME);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            var userDto = (_mapper.Map<UserDto>(userFromRepo));
            userDto.Comments = _commentRepository.GetNumberOfComments(userFromRepo.Id);
            userDto.RealEstates = _advertisementRepository.GetNumberOfProperties(userFromRepo.Id);
            userDto.Rating = _ratingRepository.GetAvarageRating(userFromRepo.Id);

            return Ok(userDto);
        }

        /// <summary>
        /// This POST method rates an user and adds a new  <see cref="Rating"/> to the DB
        /// </summary>
        /// <param name="rating"></param>
        /// <returns>Returns <see cref="BadRequestResult"/> if the user tries to rate him/herself.
        /// <para></para>Else <see cref="OkResult"/></returns>
        [Authorize]
        [HttpPut("Rate")]
        public IActionResult RateUser(RatingForCreationDto rating)
        {
            var userToRate = _userRepository.Get(rating.UserId);
            if (userToRate == null)
            {
                return NotFound();
            }

            // maps the RatedUserId and value
            var ratingEntity = _mapper.Map<Rating>(rating);

            // maps the RatingUserId
            string userName = User.Identity.Name;
            string userId = _commentRepository.GetUserIdFromUserName(userName);
            ratingEntity.RatingUserId = userId;
            if (userId == rating.UserId)
            {
                return StatusCode(500, new Response { Status = "Error", Message = "Users can not rate themselves" });
            }

            // adds the new entity to the database and saves it
            _ratingRepository.Add(ratingEntity);
            _ratingRepository.Save();

            return Ok();
        }


        /// <summary>
        /// This GET method returns an <see cref="OkResult"/> and an <see cref="UserIdDto"/> if the <see cref="User.UserName"/>
        /// <br></br>is equal to the <paramref name="USERNAME"/>
        /// </summary>
        /// <param name="USERNAME"></param>
        /// <returns><see cref="OkResult"/> and an <see cref="UserIdDto"/></returns>
        [HttpGet("UserId/{USERNAME}")]
        public IActionResult GetUserId(string USERNAME)
        {
            var userFromRepo = _userRepository.GetFromUserName(USERNAME);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            var userIdDto = new UserIdDto();
            userIdDto.UserId = userFromRepo.Id;
            userIdDto.UserName = userFromRepo.UserName;
            return Ok(userIdDto);
        }
    }
}
