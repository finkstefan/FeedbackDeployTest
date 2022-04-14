using AutoMapper;
using Microservice_Feedback.Data;
using Microservice_Feedback.Entities;
using Microservice_Feedback.Models;
using Microservice_Feedback.Services;
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
    [Route("api/feedbacks")]
    [Produces("application/json", "application/xml")] 
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkgenerator;
        private readonly IFeedbackCategoryRepository feedbackCategoryRepository;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public FeedbackController(IFeedbackRepository feedbackRepository, IMapper mapper, LinkGenerator linkgenerator,IFeedbackCategoryRepository feedbackCategoryRepository, ILoggerService loggerService)
        {
            this.feedbackRepository = feedbackRepository;
            this.mapper = mapper;
            this.linkgenerator = linkgenerator;
            this.feedbackCategoryRepository = feedbackCategoryRepository;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Feedback";
        }

        /// <summary>
        /// Returns all feedbacks from database.
        /// </summary>
        /// <returns>List of feedbacks</returns>
        /// <response code="200">Returns list of feedbacks</response>
        /// <response code="204">Nothing to return</response>
        /// <response code="505">Error in getting all feedbacks</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<FeedbackDTO>> GetFeedbacks()
        {
            try
            {
                logDto.HttpMethod = "GET";
                logDto.Message = "Return all feedbacks";
                var feedbacks = feedbackRepository.GetFeedbacks();

                if (feedbacks == null || feedbacks.Count == 0)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NoContent();
                }

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<List<FeedbackDTO>>(feedbacks));
            }

             catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Get all Error");
            }
        }

        /// <summary>
        /// Returns one feedbacks from database.
        /// </summary>
        /// <returns>Feedback</returns>
        /// <response code="200">Returns one feedback</response>
        /// <response code="404">Neither feedback has been found</response>
        /// <response code="505">Error in getting a feedback</response>
        [HttpGet("{feedbackId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FeedbackDTO> GetFeedback(Guid feedbackId)
        {
            try
            {
                logDto.HttpMethod = "GET";
                logDto.Message = "Return one feedback by id";
                Feedback feedback = feedbackRepository.GetFeedbackById(feedbackId);

                if (feedback == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                FeedbackCategory feedbackCategory = feedbackCategoryRepository.GetFeedbackCategoryById(feedback.FeedbackCategoryId);

                FeedbackDTO feedbackDTO = mapper.Map<FeedbackDTO>(feedback);

                feedbackDTO.FeedbackCategory = mapper.Map<FeedbackCategoryDTO>(feedbackCategory);

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(feedbackDTO);
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Get by id Error");
            }
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
                logDto.HttpMethod = "POST";
                logDto.Message = "Create new feedback";
                Feedback feedback = mapper.Map<Feedback>(feedbackDTO);
                Feedback helper = feedbackRepository.CreateFeedback(feedback);
                feedbackDTO = mapper.Map<FeedbackDTO>(helper);
                feedbackRepository.SaveChanges();
                string location = linkgenerator.GetPathByAction("GetFeedbacks", "Feedback", new { feedbackId = helper.FeedbackId });

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, feedbackDTO);
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
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
                logDto.HttpMethod = "PUT";
                logDto.Message = "Update feedback";
                Feedback oldFeedback = feedbackRepository.GetFeedbackById(feedback.FeedbackId);
                if (oldFeedback == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }

                oldFeedback.FeedbackCategoryId = feedback.FeedbackCategoryId;
                oldFeedback.ObjectStoreCheckId = feedback.ObjectStoreCheckId;
                oldFeedback.Text = feedback.Text;
                oldFeedback.Date = feedback.Date;
                oldFeedback.Resolved = feedback.Resolved;

                feedbackRepository.SaveChanges();

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<FeedbackDTO>(oldFeedback));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
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
                logDto.HttpMethod = "DELETE";
                logDto.Message = "Delete feedback";
                Feedback feedback = feedbackRepository.GetFeedbackById(feedbackId);
                if (feedback == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                feedbackRepository.DeleteFeedback(feedbackId);
                feedbackRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        
    }
}
