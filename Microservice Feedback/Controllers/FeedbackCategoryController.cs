using AutoMapper;
using Microservice_Feedback.Data;
using Microservice_Feedback.Entities;
using Microservice_Feedback.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice_Feedback.Controllers
{
    [ApiController]
    [Route("api/feedbackCategory")]
    [Produces("application/json", "application/xml")]
    public class FeedbackCategoryController : ControllerBase
    {
        private readonly IFeedbackCategoryRepository feedbackCategoryRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkgenerator;

        public FeedbackCategoryController(IFeedbackCategoryRepository feedbackCategoryRepository, IMapper mapper, LinkGenerator linkgenerator)
        {
            this.feedbackCategoryRepository = feedbackCategoryRepository;
            this.mapper = mapper;
            this.linkgenerator = linkgenerator;
        }

        /// <summary>
        /// Returns all feedback categories from database.
        /// </summary>
        /// <returns>List of feedback categories</returns>
        /// <response code="200">Returns list of feedback categories</response>
        /// <response code="204">Nothing to return</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<FeedbackDTO>> GetFeedbackCategories()
        {
            var feedbackCategory = feedbackCategoryRepository.GetFeedbackCategories();

            if (feedbackCategory == null || feedbackCategory.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<FeedbackCategoryDTO>>(feedbackCategory));
        }

        /// <summary>
        /// Adds feedback categories
        /// </summary>
        /// <returns>Adding new feedback categories</returns>
        /// <response code="200">Creates new feedback categories</response>
        /// <response code="500">Error in creationg new feedback categories</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FeedbackCategoryDTO> CreateFeedbackCategory([FromBody] FeedbackCategoryDTO feedbackCategoryDTO)
        {
            try
            {
                FeedbackCategory feedbackCategory = mapper.Map<FeedbackCategory>(feedbackCategoryDTO);
                FeedbackCategory helper = feedbackCategoryRepository.CreateFeedbackCategory(feedbackCategory);
                feedbackCategoryDTO = mapper.Map<FeedbackCategoryDTO>(helper);
                feedbackCategoryRepository.SaveChanges();
                string location = linkgenerator.GetPathByAction("GetFeedbackCategories", "FeedbackCategory", new { feedbackCategoryId = helper.FeedbackCategoryId });

                return Created(location, feedbackCategoryDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Returns one feedback category from database.
        /// </summary>
        /// <returns>Feedback category</returns>
        /// <response code="200">Returns one feedback category</response>
        /// <response code="404">Neither feedback category has been found</response>
        [HttpGet("{feedbackCategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FeedbackCategoryDTO> GetFeedbackCategory(Guid feedbackCategoryId)
        {
            FeedbackCategory feedbackCategory = feedbackCategoryRepository.GetFeedbackCategoryById(feedbackCategoryId);

            if (feedbackCategory == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<FeedbackCategoryDTO >(feedbackCategory));
        }



        /// <summary>
        /// Updates feedback category
        /// </summary>
        /// <returns>Updating a feedback category</returns>
        /// <response code="200">Updates feedback category</response>
        /// <response code="404">Neither feedback category has been found</response>
        /// <response code="500">Error in updating a feedback category</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FeedbackCategoryDTO> UpdateFeedbackCategory([FromBody] FeedbackCategory feedbackCategory)
        {
            try
            {
                FeedbackCategory oldFeedbackCategory = feedbackCategoryRepository.GetFeedbackCategoryById(feedbackCategory.FeedbackCategoryId);
                if (oldFeedbackCategory == null)
                {
                    return NotFound();
                }

                oldFeedbackCategory.FeedbackCategoryName = feedbackCategory.FeedbackCategoryName;

                feedbackCategoryRepository.SaveChanges();

                return Ok(mapper.Map<FeedbackCategoryDTO>(oldFeedbackCategory));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        /// <summary>
        /// Deletes feedback category
        /// </summary>
        /// <returns>Adding new feedback category</returns>
        /// <response code="200">Deletes feedback category</response>
        /// <response code="404">Neither feedback category has been found</response>
        /// <response code="500">Error in deleting feedback category</response>
        [HttpDelete("{feedbackCategoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteFeedbackCategory(Guid feedbackCategoryId)
        {
            try
            {
                FeedbackCategory feedbackCategory = feedbackCategoryRepository.GetFeedbackCategoryById(feedbackCategoryId);
                if (feedbackCategory == null)
                {
                    return NotFound();
                }
                feedbackCategoryRepository.DeleteFeedbackCategory(feedbackCategoryId);
                feedbackCategoryRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

    }

}

