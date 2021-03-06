﻿using System.Data.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Initializers;
using System.Data.Entity;

namespace DataAccessLayer.Contexts
{
    public class JobPortalDbContext : DbContext
    {
        /// <summary>
        /// Non-parametric constructor used by data access layer
        /// </summary>
        public JobPortalDbContext() : base(Config.DataAccessLayerInstaller.ConnectionString)
        {
            Database.SetInitializer(new JobPortalDbInitializer());
        }

        /// <summary>
        /// Constructor with db connection, required by data access layer tests
        /// </summary>
        /// <param name="connection">The database connection</param>
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
