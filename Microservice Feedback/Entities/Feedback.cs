using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice_Feedback.Entities
{
    /// <summary>
    /// Feedback on storecheck
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// Feedback ID
        /// </summary>
        public Guid FeedbackID;

        /// <summary>
        /// Feedback category ID
        /// </summary>
        public Guid FeedbackCategoryID;

        /// <summary>
        /// Object storecheck ID
        /// </summary>
        public Guid ObjectStoreCheckID;

        /// <summary>
        /// Feedback text
        /// </summary>
        public string FeedbackText;

        /// <summary>
        /// Feedback creation date
        /// </summary>
        public DateTime FeedbackCreationDate;

        /// <summary>
        /// Is feedback resolved
        /// </summary>
        public bool Resolved;

        /// <summary>
        /// Is feedback response
        /// </summary>
        public bool Response;

        /// <summary>
        /// Feedback due date
        /// </summary>
        public DateTime FeedbackDueDate;

        /// <summary>
        /// Good practice for feedback
        /// </summary>
        public string GoodPractice; 
    }
}
