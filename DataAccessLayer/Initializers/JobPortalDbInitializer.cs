using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace DataAccessLayer.Initializers
{
    internal class JobPortalDbInitializer : DropCreateDatabaseAlways<JobPortalDbContext>
    {
        protected override void Seed(JobPortalDbContext context)
        {
            Skill cSharp = new Skill { Name = "C#" };
            Skill dotNet= new Skill { Name = ".NET" };
            Skill html = new Skill { Name = "HTML" };
            Skill unix = new Skill { Name = "UNIX" };

            context.Skills.Add(cSharp);
            context.Skills.Add(dotNet);
            context.Skills.Add(html);
            context.Skills.Add(unix);

            JobCandidate candidate1 = new JobCandidate
            {
                Name = "Candidate 1",
                Email = "candidate1@jobs.com",
                Education = Education.AssociateDegree,
                Skills = new List<Skill> { html }
            };

            JobCandidate candidate2 = new JobCandidate
            {
                Name = "Candidate 2",
                Email = "candidate2@jobs.com",
                Education = Education.HigherDegree,
                Skills = new List<Skill> { cSharp, dotNet, html }
            };

            context.JobCandidates.Add(candidate1);
            context.JobCandidates.Add(candidate2);

            Employer google = new Employer
            {
                Name = "Google Inc.",
                Email = "google@jobs.com",
                Location = "California, U.S."
            };

            Employer sony = new Employer
            {
                Name = "Sony Corporation",
                Email = "sony@jobs.com",
                Location = "Tokyo, Japan"
            };

            context.Employers.Add(google);
            context.Employers.Add(sony);

            Question q1 = new Question { Text = "How are you?" };
            Question q2 = new Question { Text = "What is the answer?" };

            context.Questions.Add(q1);
            context.Questions.Add(q2);

            JobOffer swEngineerJobOffer = new JobOffer
            {
                Name = "Software Engineer",
                Description = "Work hard, play hard.",
                Employer = google,
                Location = "Munich, Germany",
                Skills = new List<Skill> { unix },
                Questions = new List<Question> { q1, q2 }
            };

            JobOffer frontEndJobOffer = new JobOffer
            {
                Name = "Front-end developer at Sony",
                Description = "Change the world.",
                Employer = sony,
                Location = "Tokyo, Japan",
                Skills = new List<Skill> { cSharp, dotNet, html },
                Questions = new List<Question> { q2 }
            };

            context.JobOffers.Add(swEngineerJobOffer);
            context.JobOffers.Add(frontEndJobOffer);

            JobApplication jobApplication1 = new JobApplication
            {
                JobOffer = swEngineerJobOffer,
                JobCandidate = candidate2,
                Status = Status.Open
            };

            Answer a1 = new Answer
            {
                Question = q1,
                Text = "Great!",
                JobApplication = jobApplication1
            };

            context.JobApplications.Add(jobApplication1);
            context.Answers.Add(a1);

            base.Seed(context);
        }
    }
}