﻿using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteAPI.Models.Advertisment;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;

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

        public AdvertismentController(IAdvertisementRepository advertisementRepository,
            IMapper mapper)
        {
            _advertisementRepository = advertisementRepository ?? throw new ArgumentNullException(nameof(advertisementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        /// </summary>
        /// <param name="realestateId"></param>
        /// <returns>A <see cref="AdvertismentAdvancedDto"/></returns>
        [AllowAnonymous]
        [HttpGet("{realestateId}", Name = "GetRealEstateById")]
        public ActionResult<AdvertismentAdvancedDto> GetRealEstate(int realestateId)
        {
            var advertismentFromRepo = _advertisementRepository.Get(realestateId);
            if (advertismentFromRepo == null)
            {
                return NotFound();
            }

            AdvertismentAdvancedDto adv = _mapper.Map<AdvertismentAdvancedDto>(advertismentFromRepo);
            adv.RealEstateType = _advertisementRepository.GetPropertyNameFromPropertyId(advertismentFromRepo.PropertyTypeId);
            return Ok(adv);
        }


        /// <summary>
        /// This GET method take a <paramref name="realEstateId"/> as input. 
        /// If found it map a <see cref="Advertisement"/> and the connected <see cref="Comment"/> 
        /// <br></br> to an <see cref="AdvertismentMoreAdvancedDto"/>
        /// </summary>
        /// <param name="realEstateId"></param>
        /// <returns>An <see cref="AdvertismentMoreAdvancedDto"/></returns>
        [Authorize]
        [HttpGet("{realEstateId}/secure")] //Remove /secure - when user identity is implemented
        public ActionResult<AdvertismentMoreAdvancedDto> GetRealEstateSecure(int realEstateId)
        {
            // TODO fix the multiple method route probelm.
            var advertismentFromRepo = _advertisementRepository.Get(realEstateId);
            if (advertismentFromRepo == null)
            {
                return NotFound();
            }

            AdvertismentMoreAdvancedDto adv = _mapper.Map<AdvertismentMoreAdvancedDto>(advertismentFromRepo);
            adv.RealEstateType = _advertisementRepository.GetPropertyNameFromPropertyId(advertismentFromRepo.PropertyTypeId);

            var comments = _advertisementRepository.GetComments(advertismentFromRepo.Id);
            foreach (Comment comment in comments)
            {
                adv.Comments.Add(_mapper.Map(comment, new CommentDto()));
            }
            return Ok(adv);
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
                _advertisementRepository.Save();

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
    }
}
