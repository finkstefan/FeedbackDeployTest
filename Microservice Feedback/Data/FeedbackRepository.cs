using AutoMapper;
using Microservice_Feedback.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice_Feedback.Data
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackContext context;
        private readonly IMapper mapper;

        public FeedbackRepository(FeedbackContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public FeedbackConfirmation CreateFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public void DeleteFeedback(Guid feedbackId)
        {
            throw new NotImplementedException();
        }

        public Feedback GetFeedbackById(Guid feedbackId)
        {
            throw new NotImplementedException();
        }

        public List<Feedback> GetFeedbacks()
        {
            return context.Feedbacks.ToList();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }
    }
}
