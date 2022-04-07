using AutoMapper;
using Microservice_Feedback.Data;
using Microservice_Feedback.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice_Feedback.Controllers
{
    [ApiController]
    [Route("api/feedbacks")]
    [Produces("application/json", "application/xml")] 
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IMapper mapper;

        public FeedbackController(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            this.feedbackRepository = feedbackRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns all feedbacks from database.
        /// </summary>
        /// <returns>List of feedbacks</returns>
        /// <response code="200">Returns list of feedbacks</response>
        /// <response code="404">Neither feedback has been found</response>
        [HttpGet]  
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<FeedbackDTO>> GetExamRegistrations()
        {
            var feedbacks = feedbackRepository.GetFeedbacks();

            if (feedbacks == null || feedbacks.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<FeedbackDTO>>(feedbacks));
        }
    }
}
