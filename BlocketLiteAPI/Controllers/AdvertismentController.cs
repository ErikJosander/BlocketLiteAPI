﻿using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteAPI.Models.Advertisment;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BlocketLiteAPI.Controllers
{
    /// <summary>
    /// Advertisment Controller responsible for GET/POST for managing the advertisments
    /// </summary>
    [Route("api/RealEstates")]
    [ApiController]
    public class AdvertismentController : ControllerBase
    {
        private readonly IAdvertismentRepository _advertismentRepository;
        private readonly IMapper _mapper;

        public AdvertismentController(IAdvertismentRepository advertismentRepository,
            IMapper mapper)
        {
            _advertismentRepository = advertismentRepository ?? throw new ArgumentNullException(nameof(advertismentRepository));
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
        public ActionResult<IEnumerable<AdvertismentSimpleDto>> GetAdvertisments(
           int skip = 0, int take = 10)
        {
            var advertismentsFromRepo = _advertismentRepository.GetAll(skip, take);
            // format the given result as Json.
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
        [HttpGet("{realestateId}")]
        public ActionResult<AdvertismentAdvancedDto> GetRealEstate(int realestateId)
        {
            var advertismentFromRepo = _advertismentRepository.Get(realestateId);
            if (advertismentFromRepo == null)
            {
                return NotFound();
            }

            AdvertismentAdvancedDto adv = _mapper.Map<AdvertismentAdvancedDto>(advertismentFromRepo);

            adv.RealEstateType = _advertismentRepository.GetPropertyNameFromPropertyId(advertismentFromRepo.PropertyTypeId);
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
        [HttpGet("{realEstateId}/secure")]
        public ActionResult<AdvertismentMoreAdvancedDto> GetRealEstateSecure(int realEstateId)
        {
            /// TODO fix the multiple method route probelm.
            var advertismentFromRepo = _advertismentRepository.Get(realEstateId);
            if (advertismentFromRepo == null)
            {
                return NotFound();
            }

            AdvertismentMoreAdvancedDto adv = _mapper.Map<AdvertismentMoreAdvancedDto>(advertismentFromRepo);
            adv.RealEstateType = _advertismentRepository.GetPropertyNameFromPropertyId(advertismentFromRepo.PropertyTypeId);

            var comments = _advertismentRepository.GetComments(advertismentFromRepo.Id);
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
        public ActionResult<AdvertismentSimpleDto> CreateRealEstate(AdvertisementForCreationDto advertisement)
        {
            var advertismentEntity = _mapper.Map<Advertisement>(advertisement);

            string userName = User.Identity.Name;
            int userId = _advertismentRepository.GetUserIdFromUserName(userName);
            advertismentEntity.UserId = userId;


            if (advertismentEntity.RentingPrice != null) advertismentEntity.CanBeRented = true;
            if (advertismentEntity.SellingPrice != null) advertismentEntity.CanBeSold = true;
            advertismentEntity.CreatedOn = Helpers.GetCurrentDateUTC.GetDateTimeUTC();

            _advertismentRepository.Add(advertismentEntity);
            _advertismentRepository.Save();

            // TODO not returning the correct path (can't find path when i posted)
            var advertismentToReturn = _mapper.Map<AdvertismentSimpleDto>(advertismentEntity);
            return CreatedAtRoute(nameof(GetRealEstate), new { id = advertismentToReturn.Id }, advertismentToReturn);
        }
    }
}