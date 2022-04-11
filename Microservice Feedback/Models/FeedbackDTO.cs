﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice_Feedback.Models
{
    public class FeedbackDTO
    {
        /// <summary>
        /// Feedback category ID
        /// </summary>
        public Guid FeedbackCategoryID { get; set; }

        /// <summary>
        /// Object storecheck ID
        /// </summary>
        public Guid ObjectStoreCheckID { get; set; }

        /// <summary>
        /// Feedback text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Feedback creation date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Is feedback resolved
        /// </summary>
        public bool Resolved { get; set; }

    }
}
