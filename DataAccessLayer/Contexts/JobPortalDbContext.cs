using System.Data.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Initializers;
using System.Data.Entity;

namespace DataAccessLayer.Contexts
{
    public class JobPortalDbContext : DbContext
    {
        /// <summary>
        /// Na netesty
        /// </summary>
        public JobPortalDbContext() : base(Config.DiInstaller.ConnectionString)
        {
            Database.SetInitializer(new JobPortalDbInitializer());
        }

        /// <summary>
        /// Na testy
        /// </summary>
        public JobPortalDbContext(DbConnection connection) : base(connection, true)
        {
            Database.CreateIfNotExists();
        }

        public DbSet<UserBase> UserBases { get; set; }
        public DbSet<JobCandidate> JobCandidates { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Office> Offices { get; set; }
    }
}
