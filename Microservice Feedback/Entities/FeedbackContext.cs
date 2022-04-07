using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice_Feedback.Entities
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options)
        {

        }

        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Feedback>()
                .HasData(new
                {
                    FeedbackID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    FeedbackCategoryID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    ObjectStoreCheckID = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    FeedbackText = "Some feedback",
                    FeedbackCreationDate = DateTime.Now,
                    Resolved = false,
                    Response = false,
                    FeedbackDueDate = DateTime.Now,
                    GoodPractice = "This is an example of good practice"
                });
        }
    }
}
