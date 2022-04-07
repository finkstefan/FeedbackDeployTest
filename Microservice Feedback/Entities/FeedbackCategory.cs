using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice_Feedback.Entities
{
    /// <summary>
    /// Category of feedback
    /// </summary>
    public class FeedbackCategory
    {
        /// <summary>
        /// Feedback category ID
        /// </summary>
        public Guid FeedbackCategoryID;

        /// <summary>
        /// Feedback category name
        /// </summary>
        public string FeedbackCategoryName;
    }
}
