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
        private readonly StoreCheckFeedbackContext context;
        private readonly IMapper mapper;

        public FeedbackRepository(StoreCheckFeedbackContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Feedback CreateFeedback(Feedback feedback)
        {
            feedback.FeedbackId = Guid.NewGuid();
            context.Feedbacks.Add(feedback);
            return feedback;
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
            return context.SaveChanges() > 0;
        }

        public void UpdateFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }
    }
}
