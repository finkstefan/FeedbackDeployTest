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
    [Route("api/feedback")]
    [Produces("application/json", "application/xml")] 
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkgenerator;

        public FeedbackController(IFeedbackRepository feedbackRepository, IMapper mapper, LinkGenerator linkgenerator)
        {
            this.feedbackRepository = feedbackRepository;
            this.mapper = mapper;
            this.linkgenerator = linkgenerator;
        }

        /// <summary>
        /// Returns all feedbacks from database.
        /// </summary>
        /// <returns>List of feedbacks</returns>
        /// <response code="200">Returns list of feedbacks</response>
        /// <response code="204">Nothing to return</response>
        [HttpGet]  
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<FeedbackDTO>> GetFeedbacks()
        {
            var feedbacks = feedbackRepository.GetFeedbacks();

            if (feedbacks == null || feedbacks.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<FeedbackDTO>>(feedbacks));
        }

        /// <summary>
        /// Returns one feedbacks from database.
        /// </summary>
        /// <returns>Feedback</returns>
        /// <response code="200">Returns one feedback</response>
        /// <response code="404">Neither feedback has been found</response>
        [HttpGet("{feedbackId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FeedbackDTO> GetFeedback(Guid feedbackId)
        {
            Feedback feedback = feedbackRepository.GetFeedbackById(feedbackId);

            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<FeedbackDTO>(feedback));
        }


        /// <summary>
        /// Adds feedback
        /// </summary>
        /// <returns>Adding new feedback</returns>
        /// <response code="200">Creates new feedback</response>
        /// <response code="505">Error in creating new feedback</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FeedbackDTO> CreateFeedback([FromBody] FeedbackDTO feedbackDTO)
        {
            try
            {
                Feedback feedback = mapper.Map<Feedback>(feedbackDTO);
                Feedback helper = feedbackRepository.CreateFeedback(feedback);
                feedbackDTO = mapper.Map<FeedbackDTO>(helper);
                feedbackRepository.SaveChanges();
                string location = linkgenerator.GetPathByAction("GetFeedbacks", "Feedback", new { feedbackId = helper.FeedbackId });
             
                return Created(location, feedbackDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Updates feedback
        /// </summary>
        /// <returns>Updating a feedback</returns>
        /// <response code="200">Updates feedback</response>
        /// <response code="404">Neither feedback has been found</response>
        /// <response code="500">Error in updating new feedback</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FeedbackDTO> UpdateFeedback([FromBody] Feedback feedback)
        {
            try
            {
                Feedback oldFeedback = feedbackRepository.GetFeedbackById(feedback.FeedbackId);
                if (oldFeedback == null)
                {
                    return NotFound();
                }

                oldFeedback.FeedbackCategoryId = feedback.FeedbackCategoryId;
                oldFeedback.ObjectStoreCheckId = feedback.ObjectStoreCheckId;
                oldFeedback.Text = feedback.Text;
                oldFeedback.Date = feedback.Date;
                oldFeedback.Resolved = feedback.Resolved;

                feedbackRepository.SaveChanges();

                return Ok(mapper.Map<FeedbackDTO>(oldFeedback));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        /// <summary>
        /// Deletes feedback
        /// </summary>
        /// <returns>Adding new feedback</returns>
        /// <response code="200">Deletes feedback</response>
        /// <response code="404">Neither feedback has been found</response>
        /// <response code="500">Error in deleting feedback</response>
        [HttpDelete("{feedbackId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteFeedback(Guid feedbackId)
        {
            try
            {
                Feedback feedback = feedbackRepository.GetFeedbackById(feedbackId);
                if (feedback == null)
                {
                    return NotFound();
                }
                feedbackRepository.DeleteFeedback(feedbackId);
                feedbackRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        
    }
}
