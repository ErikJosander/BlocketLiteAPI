using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteEFCoreDB.Entities;
using BlocketLiteEFCoreDB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BlocketLiteAPI.Controllers
{
    /// <summary>
    /// Comment Controller responsible for GET/POST for managing the comments
    /// </summary>
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository,
            IAdvertisementRepository advertisementRepository, IMapper mapper)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _advertisementRepository = advertisementRepository ?? throw new ArgumentNullException(nameof(advertisementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

       
        /// <summary>
        /// This Get methode returns all comments in the DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CommentDto>> GetAllComments()
        {
            var commentsFromRepo = _commentRepository.GetAll();           
            return Ok(_mapper.Map<IEnumerable<CommentDto>>(commentsFromRepo));
        }

        /// <summary>
        /// This GET method takes an <see cref="Advertisement.Id"/> as input.
        /// <br></br>And an optional <paramref name="skip"/> and <paramref name="take"/> search query
        /// <br></br>Needs a valid JWT token to be enabled
        /// </summary>
        /// <param name="realEstateId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>an <see cref="OkResult"/> list of <see cref="CommentDto"/> mapped from the DB</returns>
        [Authorize]
        [HttpGet("{realEstateId}", Name = "GetCommentsForARealEstate")]
        public ActionResult<IEnumerable<CommentDto>> GetComments(int realEstateId, int skip = 0, int take = 10)
        {
            if (skip < 0 || take < 0)
            {
                return BadRequest();
            }
            var commentsFromRepo = _commentRepository.GetAllFromRealEstate(realEstateId, skip, take);
            if (commentsFromRepo == null) return NotFound();
            if (commentsFromRepo.Count == 0) return NotFound();
            // Is sorted in repository, no need here/MJ
            //commentsFromRepo = commentsFromRepo.OrderBy(d => d.CreatedOn).ToList();
            // format the given result as Json.
            return Ok(_mapper.Map<IEnumerable<CommentDto>>(commentsFromRepo));
        }


        /// <summary>
        /// This method adds a comment to a specific real estate.
        /// This GET method takes an <see cref="User.UserName"/> as input. 
        /// <br></br>And an optional <paramref name="skip"/> and <paramref name="take"/> search query
        /// </summary>
        /// <param name="USERNAME"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>an <see cref="OkResult"/> list of <see cref="CommentDto"/> mapped from the DB</returns>
        [Authorize]
        [HttpGet("ByUser/{USERNAME}", Name = "GetCommentsByUserName")] 
        public ActionResult<IEnumerable<CommentDto>> GetComments(string USERNAME, int skip = 0, int take = 10)
        {
            if (skip < 0 || take < 0)
            {
                return BadRequest();
            }
            var commentsFromRepo = _commentRepository.GetAllFromUser(USERNAME, skip, take);
            if (commentsFromRepo == null) return NotFound();
            if (commentsFromRepo.Count == 0) return NotFound();

            // Is sorted in repository, no need here/MJ

            //commentsFromRepo = commentsFromRepo.OrderBy(d => d.CreatedOn).ToList();
            // format the given result as Json.
            return Ok(_mapper.Map<IEnumerable<CommentDto>>(commentsFromRepo));
        }


        /// <summary>
        /// This POST method takes a <see cref="CommentForCreationDto"/> as input
        /// <br></br>If possible map to an <see cref="Comment"/> Entity and save it to the DB.
        /// </summary>
        /// <param name="advertisementId"></param>
        /// <param name="comment"></param>
        /// <returns>An <see cref="CreatedAtRouteResult"/> with <see cref="CommentDto"/> attached</returns>
        [Authorize]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> CreateComment(int advertisementId,[FromBody]CommentForCreationDto comment)
        {
            try
            {
                //TODO check if the advertisement exists
                if(comment == null)
                {
                    return BadRequest("Comment object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var commentEntity = _mapper.Map<Comment>(comment);
                commentEntity.CreatedOn = DateTime.Now;
                string userName = User.Identity.Name;
                userName = "Calle"; //Remove! Temporary tests
                int? userId = _commentRepository.GetUserIdFromUserName(userName);
                
                commentEntity.UserId = userId;
                commentEntity.UserName = userName;

                _commentRepository.Add(commentEntity);
                _commentRepository.Save();

                // TODO not returning the correct path (can't find path when i posted)
                var commentToReturn = _mapper.Map<CommentDto>(commentEntity);
                //var x = CreatedAtRoute("GetCommentsForARealEstate", new { commentEntity.Id }, commentToReturn);
                //return (x);
                return CreatedAtAction("GetCommentById", new { commentEntity.Id }, commentToReturn);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
          
        }

        // Only for routing ===> TESTING
        [Authorize]
        [HttpGet("GetComment/{commentId}", Name = "GetCommentById")]
        public ActionResult<IEnumerable<CommentDto>> GetCommentsAction(int commentId)
        {
            var commentsFromRepo = _commentRepository.Get(commentId);
            if (commentsFromRepo == null) return NotFound();

            // format the given result as Json.
            return Ok(_mapper.Map<IEnumerable<CommentDto>>(commentsFromRepo));
        }

    }
}
