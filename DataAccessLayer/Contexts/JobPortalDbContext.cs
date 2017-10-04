using DataAccessLayer.Entities;
using DataAccessLayer.Initializers;
using System.Data.Entity;

namespace DataAccessLayer.Contexts
{
    public class JobPortalDbContext : DbContext
    {
        public JobPortalDbContext() : base("JobPortal")
        {
            Database.SetInitializer(new JobPortalDbInitializer());
        }
        public DbSet<JobCandidate> JobCandidates { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
