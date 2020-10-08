using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteAPI.Models.Advertisment;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BlocketLiteAPI.Controllers
{
    /// <summary>
    /// Advertisment Controller responsible for GET/POST for managing the advertisments
    /// </summary>

    [Produces("application/json")]
    [Route("api/RealEstates")]
    [ApiController]
    public class AdvertismentController : ControllerBase
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public AdvertismentController(IAdvertisementRepository advertisementRepository,
            IMapper mapper,
            IUserRepository userRepository,
            ICommentRepository commentRepository)
        {
            _advertisementRepository = advertisementRepository ?? throw new ArgumentNullException(nameof(advertisementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// This GET method takes an optional <paramref name="take"/> and <paramref name="skip"/> as a search query.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>An <see cref="IEnumerable{T}"/> as a list of <see cref="AdvertismentSimpleDto"/></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<AdvertismentSimpleDto>> GetAdvertisments(int skip = 0, int take = 0)
        {
            if (skip < 0 || take < 0)
            {
                return BadRequest();
            }
            var advertismentsFromRepo = _advertisementRepository.GetAll(skip, take);
            if (advertismentsFromRepo == null)

            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<AdvertismentSimpleDto>>(advertismentsFromRepo);
            return Ok(result);
        }


        /// <summary>
        /// This GET method takes a <paramref name="realestateId"/>" as input.
        /// <br></br> Searching for a specific <see cref="Advertisement"/> in the DB.
        /// <br></br> If <see cref="User"/> is autorized return an <see cref="AdvertismentMoreAdvancedDto"/>
        /// </summary>
        /// <param name="realestateId"></param>
        /// <returns>A <see cref="AdvertismentAdvancedDto"/> or a <see cref="AdvertismentMoreAdvancedDto"/></returns>
        [AllowAnonymous]
        [HttpGet("{realestateId}", Name = "GetRealEstateById")]
        public async Task<ActionResult<AdvertismentAdvancedDto>> GetRealEstateAsync(int realestateId)
        {
            // Secure
            if (User.Identity.IsAuthenticated)
            {
                var advertismentFromRepo = await _advertisementRepository.GetAsync(realestateId);
                if (advertismentFromRepo == null)
                {
                    return NotFound();
                }

                AdvertismentMoreAdvancedDto adv = _mapper.Map<AdvertismentMoreAdvancedDto>(advertismentFromRepo);
                adv.RealEstateType = _advertisementRepository.GetPropertyNameFromPropertyId(advertismentFromRepo.PropertyTypeId);
                adv.UserName = _advertisementRepository.GetUserNameFromUserId(advertismentFromRepo.UserId);
                var comments = _advertisementRepository.GetComments(advertismentFromRepo.Id);
                foreach (Comment comment in comments)
                {
                    adv.Comments.Add(_mapper.Map(comment, new CommentDto()));
                }
                return Ok(adv);
            }
            // Public
            else
            {
                var advertismentFromRepo = await _advertisementRepository.GetAsync(realestateId);
                if (advertismentFromRepo == null)
                {
                    return NotFound();
                }

                AdvertismentAdvancedDto adv = _mapper.Map<AdvertismentAdvancedDto>(advertismentFromRepo);
                adv.RealEstateType = _advertisementRepository.GetPropertyNameFromPropertyId(advertismentFromRepo.PropertyTypeId);
                adv.UserName = _advertisementRepository.GetUserNameFromUserId(advertismentFromRepo.UserId);
                return Ok(adv);
            }
        }

        /// <summary>
        /// This POST method take an <see cref="AdvertisementForCreationDto"/> as input.
        /// <see cref="AdvertisementForCreationDto.RentingPrice"/> and <see cref="AdvertisementForCreationDto.SellingPrice"/> 
        /// <br></br> can't both be null.
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns>If Ok: <see cref="CreatedAtRouteResult"/> and an <see cref="AdvertismentSimpleDto"/></returns>
        [Authorize]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AdvertismentSimpleDto> CreateRealEstate(
            [FromBody] AdvertisementForCreationDto advertisement)
        {
            try
            {
                if (advertisement == null)
                {
                    return BadRequest("Advertisement object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var advertismentEntity = _mapper.Map<Advertisement>(advertisement);
                string userName = User.Identity.Name;
                string userId = _advertisementRepository.GetUserIdFromUserName(userName);


                advertismentEntity.UserId = userId;
                if (advertismentEntity.RentingPrice != null) advertismentEntity.CanBeRented = true;
                if (advertismentEntity.SellingPrice != null) advertismentEntity.CanBeSold = true;
                advertismentEntity.CreatedOn = Helpers.GetCurrentDateUTC.GetDateTimeUTC();

                _advertisementRepository.Add(advertismentEntity);
                // TODO might be problem
                _advertisementRepository.SaveAsync();

                var advertismentToReturn = _mapper.Map<AdvertismentSimpleDto>(advertismentEntity);
                return CreatedAtAction("GetRealEstateById", new { realestateId = advertismentToReturn.Id }, advertismentToReturn);
            }
            catch (Exception ex)
            {
                //TODO - create logging for errors
                //_logger.LogError($"Something went wrong inside the CreateRealEstate action");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{realestateId}")]
        public async Task<ActionResult> DeleteAdvertismentAsync(int realestateId)
        {        
            var adv = await _advertisementRepository.GetAsync(realestateId);
            if(adv == null)
            {
                return BadRequest();
            }
            string userName = User.Identity.Name;
            var user = _userRepository.GetFromUserName(userName);
            if(adv.UserId != user.Id)
            {
                // TODO error message
                return BadRequest();
            }
            var comments = await _commentRepository.GetAllFromRealEstateAsync(realestateId);
            foreach (var c in comments)
            {
                _commentRepository.Remove(c);             
            }
            _commentRepository.SaveAsync();

            _advertisementRepository.Remove(adv);
            _advertisementRepository.SaveAsync();

            // TODO maybe return removed advertisment?
            return Ok();
        }
    }
}
