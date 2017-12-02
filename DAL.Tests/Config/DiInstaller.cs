using System;
using System.Collections.Generic;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Tests.Config
{
    public class DiInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "InMemoryTestDBJobPortal";

        public static Skill CSharp, DotNet, Html, Unix;
        public static Employer Google, Sony;
        public static Office GoogleOffice, SonyOffice;
        public static JobCandidate Candidate1, Candidate2;
        public static RegisteredUser David, Lubos;
        public static JobOffer SwEngineerJobOffer, FrontEndJobOffer;
        public static JobApplication JobApplication1;
        public static Question Q1, Q2;
        public static Answer A1;

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EfUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EfRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EfQuery<>))
                    .LifestyleTransient()
            );
        }

        private static DbContext InitializeDatabase()
        {
            var context = new JobPortalDbContext(Effort.DbConnectionFactory.CreatePersistent(TestDbConnectionString));

            context.JobCandidates.RemoveRange(context.JobCandidates);
            context.RegisteredUsers.RemoveRange(context.RegisteredUsers);
            context.Employers.RemoveRange(context.Employers);
            context.UserBases.RemoveRange(context.UserBases);
            context.JobOffers.RemoveRange(context.JobOffers);
            context.JobApplications.RemoveRange(context.JobApplications);
            context.Questions.RemoveRange(context.Questions);
            context.Answers.RemoveRange(context.Answers);
            context.Skills.RemoveRange(context.Skills);
            context.Offices.RemoveRange(context.Offices);

            context.SaveChanges();

            #region Skills

            CSharp = new Skill { Name = "C#" };
            DotNet = new Skill { Name = ".NET" };
            Html = new Skill { Name = "HTML" };
            Unix = new Skill { Name = "UNIX" };

            context.Skills.Add(CSharp);
            context.Skills.Add(DotNet);
            context.Skills.Add(Html);
            context.Skills.Add(Unix);

            #endregion

            #region Job Candidates

            Candidate1 = new JobCandidate
            {
                Name = "Candidate 1",
                Email = "candidate1@jobs.com",
                Education = Education.AssociateDegree,
                Skills = new List<Skill> { Html }
            };

            Candidate2 = new JobCandidate
            {
                Name = "Candidate 2",
                Email = "candidate2@jobs.com",
                Education = Education.HigherDegree,
                Skills = new List<Skill> { CSharp, DotNet, Html }
            };

            context.JobCandidates.Add(Candidate1);
            context.JobCandidates.Add(Candidate2);

            #endregion

            #region Employers

            Google = new Employer
            {
                Name = "Google Inc.",
                Email = "google@jobs.com",
	            Username = "google",
	            Password = "HsewCti7AW7QvbIMu976KVD0mILylbEoXU+p3sRfxBRL+wfuasxodi0SwuM=dpsI2M0k7vgc68wL" // zahashovane "google"
			};

            GoogleOffice = new Office
            {
                Address = "345 Spear Street",
                City = "San Francisco",
                Country = Country.USA,
                Phone = "+1 415-736-0000"
            };

            Sony = new Employer
            {
                Name = "Sony Corporation",
                Email = "sony@jobs.com",
	            Username = "Sony-SK",
	            Password = "KHfaaRLJ2hXlNp/9WH05K2x2gOx2VrrxQ1EUkeYsTsb0WcIczmlT5vFwe9c=bqMzJSFAepKUm/VV" // zahashovane "sony"
			};

            SonyOffice = new Office
            {
                Address = "345 Spear Street",
                City = "San Francisco",
                Country = Country.USA,
                Phone = "+1 415-736-0000"
            };

            context.Employers.Add(Google);
            context.Employers.Add(Sony);
            context.Offices.Add(GoogleOffice);
            context.Offices.Add(SonyOffice);

            #endregion

            #region Questions

            Q1 = new Question { Text = "How are you?" };
            Q2 = new Question { Text = "What is the answer?" };

            context.Questions.Add(Q1);
            context.Questions.Add(Q2);

            #endregion

            #region Job Offers

            SwEngineerJobOffer = new JobOffer
            {
                Name = "Software Engineer",
                Description = "Work hard, play hard.",
                Employer = Google,
                Office = GoogleOffice,
                Skills = new List<Skill> { Unix },
                Questions = new List<Question> { Q1, Q2 }
            };

            FrontEndJobOffer = new JobOffer
            {
                Name = "Front-end developer at Sony",
                Description = "Change the world.",
                Employer = Sony,
                Office = SonyOffice,
                Skills = new List<Skill> { CSharp, DotNet, Html },
                Questions = new List<Question> { Q2 }
            };

            context.JobOffers.Add(SwEngineerJobOffer);
            context.JobOffers.Add(FrontEndJobOffer);

            #endregion

            #region Job Applications

            JobApplication1 = new JobApplication
            {
                JobOffer = SwEngineerJobOffer,
                JobCandidate = Candidate2,
                Status = Status.Open
            };

            A1 = new Answer
            {
                Question = Q1,
                Text = "Great!",
                JobApplication = JobApplication1
            };

            context.JobApplications.Add(JobApplication1);
            context.Answers.Add(A1);

            #endregion

            #region Registered Users

            David = new RegisteredUser
            {
                Name = "David",
                Email = "david@david.sk",
                Education = Education.BachelorDegree,
                Skills = new List<Skill> { Unix },
                JobApplications = new List<JobApplication> { JobApplication1 },
	            Username = "David",
	            Password = "DuM4dT4M6ozQXl1TB9BG1frBKz+C2vUDW5quhtta3IJ16/vwXsgwBaSouj4=iDzHfQLao/mnlST/" // zahashovane "david"
			};

            Lubos = new RegisteredUser
            {
                Name = "Lubos",
                Email = "lubos@lubos.sk",
                Education = Education.DoctoralDegree,
                Skills = new List<Skill> { Html },
                JobApplications = new List<JobApplication>(),
	            Username = "Lubos",
	            Password = "l0Jy9gqcm18meRjv64WDXuyXhFhyu9Du7GKONBnJsl+aF548sq5GJoqcJOk=rMjTy9mRjGbH3F5Q" // zahashovane "lubos"
			};

            context.RegisteredUsers.Add(David);
            context.RegisteredUsers.Add(Lubos);

            #endregion

            context.SaveChanges();
            return context;
        }
    }
}
