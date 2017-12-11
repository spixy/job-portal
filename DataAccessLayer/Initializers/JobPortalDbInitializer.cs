using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System.Collections.Generic;
using System.Data.Entity;

namespace DataAccessLayer.Initializers
{
    internal class JobPortalDbInitializer : DropCreateDatabaseAlways<JobPortalDbContext>
    {
        protected override void Seed(JobPortalDbContext context)
        {
            #region Skills

            Skill c = new Skill { Name = "C" };
            Skill cobol = new Skill { Name = "COBOL" };
            Skill cMinusMinus = new Skill { Name = "C--" };
            Skill cPlusPlus = new Skill { Name = "C++" };
            Skill cSharp = new Skill { Name = "C#" };
            Skill dotNet = new Skill { Name = ".NET" };
            Skill hacking = new Skill { Name = "Hacking" };
            Skill haskell = new Skill { Name = "Haskell" };
            Skill html = new Skill { Name = "HTML" };
            Skill unix = new Skill { Name = "UNIX" };
            Skill php = new Skill { Name = "PHP" };
            Skill java = new Skill { Name = "Java" };
            Skill ruby = new Skill { Name = "Ruby" };
            Skill ror = new Skill { Name = "Ruby on Rails" };
            Skill swift = new Skill { Name = "Swift" };

            context.Skills.Add(c);
            context.Skills.Add(cobol);
            context.Skills.Add(cMinusMinus);
            context.Skills.Add(cPlusPlus);
            context.Skills.Add(cSharp);
            context.Skills.Add(dotNet);
            context.Skills.Add(hacking);
            context.Skills.Add(haskell);
            context.Skills.Add(html);
            context.Skills.Add(unix);
            context.Skills.Add(php);
            context.Skills.Add(java);
            context.Skills.Add(ruby);
            context.Skills.Add(ror);
            context.Skills.Add(swift);

            #endregion

            #region Job Candidates

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

			#endregion

			#region Employers & Offices

			Employer google = new Employer
            {
                Name = "Google Inc.",
                Email = "google@jobs.com",
	            Phone = "12 345 678",
				Username = "google",
				Password = "HsewCti7AW7QvbIMu976KVD0mILylbEoXU+p3sRfxBRL+wfuasxodi0SwuM=dpsI2M0k7vgc68wL" // zahashovane "google"
			};

            Office googleOffice = new Office
            {
                Address = "345 Spear Street",
                City = "San Francisco",
                Country = Country.USA,
                Phone = "+1 415-736-0000"
            };

            Office zurichOffice = new Office
            {
                Address = "Brandschenkestrasse 110",
                City = "Zürich",
                Country = Country.Switzerland,
                Phone = "+41 44 668 18 00"
            };

            Employer sony = new Employer
            {
                Name = "Sony Corporation",
                Email = "sony@jobs.com",
	            Username = "sony",
	            Password = "KHfaaRLJ2hXlNp/9WH05K2x2gOx2VrrxQ1EUkeYsTsb0WcIczmlT5vFwe9c=bqMzJSFAepKUm/VV" // zahashovane "sony"
			};

            Office sonyOffice = new Office
            {
                Address = "345 Spear Street",
                City = "San Francisco",
                Country = Country.USA,
                Phone = "+1 415-736-0000"
            };

            Employer netsuite = new Employer
            {
                Name = "NetSuite Inc.",
                Email = "netsuite@jobs.com",
				Username = "netsuite",
                Password = "zUSheS1WDjMwbhEJF1FEuncnMAdBxH98iBwSwdpVN3alg+EMYgzU0k3C0e0=t7H1Psjy6PzJlH+E" // zahashovane "netsuite"
            };

            Office aOffice = new Office
            {
                Address = "Trnitá 491/3",
                City = "Brno",
                Country = Country.CzechRepublic,
                Phone = "511 187 100"
            };

            Office bOffice = new Office
            {
                Address = "Dornych 510/38",
                City = "Brno",
                Country = Country.CzechRepublic,
                Phone = "511 187 100"
            };

            Employer facebook = new Employer
            {
                Name = "Facebook Inc.",
                Email = "facebook@jobs.com",
	            Phone = "666 666 666",
				Username = "facebook",
                Password = "/sqw1+W+VhO+RbbuPVn3VI4a8hUbZBNQA85jwKSEwmubixZqexwYBew0sAo=TTK/uaA68EDa5TLw" // zahashovane "facebook"
            };

            Office facebookOffice = new Office
            {
                Address = "1 Hacker Way",
                City = "Menlo Park, California",
                Country = Country.USA,
                Phone = "+1 650-543-4800"
            };

            Employer apple = new Employer
            {
                Name = "Apple Inc.",
                Email = "apple@jobs.com",
	            Phone = "44 555 666",
				Username = "apple",
                Password = "wOKnSoVqFkys/gC0OnvT4jYKPSnupDb8+35ExzXRF8QtHBg1QgtrmULdTxs=I2U595O75nahPYWi" // zahashovane "apple"
            };

            Office appleOffice = new Office
            {
                Address = "1 Infinite Loop",
                City = "Cupertino, California",
                Country = Country.USA,
                Phone = "+1 408-996-1010"
            };

            context.Employers.Add(google);
            context.Employers.Add(sony);
            context.Employers.Add(netsuite);
            context.Employers.Add(facebook);
            context.Employers.Add(apple);

            context.Offices.Add(googleOffice);
            context.Offices.Add(zurichOffice);
            context.Offices.Add(sonyOffice);
            context.Offices.Add(aOffice);
            context.Offices.Add(bOffice);
            context.Offices.Add(facebookOffice);
            context.Offices.Add(appleOffice);

            #endregion

            #region Questions

            Question q1 = new Question { Text = "How are you?" };
            Question q2 = new Question { Text = "What is the answer?" };
            Question q3 = new Question { Text = "What is your favourite programming language?" };
            Question q4 = new Question { Text = "How much cost a trip to Mars?" };
            Question q5 = new Question { Text = "How much is one plus three?" };
            Question q6 = new Question { Text = "What is your best achievement so far?" };
            Question q7 = new Question { Text = "What is your granfather's name?" };

            context.Questions.Add(q1);
            context.Questions.Add(q2);
            context.Questions.Add(q3);
            context.Questions.Add(q4);
            context.Questions.Add(q5);
            context.Questions.Add(q6);
            context.Questions.Add(q7);

            #endregion

            #region Job Offers

            JobOffer swEngineerJobOffer = new JobOffer
            {
                Name = "Software Engineer",
                Description = "Work hard, play hard.",
                Employer = google,
                Office = googleOffice,
                Skills = new List<Skill> { unix },
                Questions = new List<Question> { q1, q2, q4 }
            };

            JobOffer specialistJobOffer = new JobOffer
            {
                Name = "Technical Specialist",
                Description = "Escalate internal solutions.",
                Employer = google,
                Office = googleOffice,
                Skills = new List<Skill> { html, php, java },
                Questions = new List<Question> { q2, q4, q6 }
            };

            JobOffer scientistJobOffer = new JobOffer
            {
                Name = "Research Scientist",
                Description = "Different research at Google.",
                Employer = google,
                Office = zurichOffice,
                Skills = new List<Skill> { cSharp, dotNet },
                Questions = new List<Question> { q2, q7 }
            };

            JobOffer frontEndJobOffer = new JobOffer
            {
                Name = "Front-end developer at Sony",
                Description = "Change the world.",
                Employer = sony,
                Office = sonyOffice,
                Skills = new List<Skill> { cSharp, dotNet, html },
                Questions = new List<Question> { q1, q3, q5, q7 }
            };

            JobOffer programmerJobOffer = new JobOffer
            {
                Name = "Programmer Head",
                Description = "Let's hack the world.",
                Employer = netsuite,
                Office = bOffice,
                Skills = new List<Skill> { cSharp, dotNet, php, java, ruby, swift },
                Questions = new List<Question> { q3, q6 }
            };

            JobOffer geekJobOffer = new JobOffer
            {
                Name = "Geek Job",
                Description = "It's up to you..",
                Employer = facebook,
                Office = facebookOffice,
                Skills = new List<Skill> { php, unix },
                Questions = new List<Question> { q2 }
            };

            JobOffer designerJobOffer = new JobOffer
            {
                Name = "Apple Designer",
                Description = "Design new Apple products.",
                Employer = apple,
                Office = appleOffice,
                Skills = new List<Skill> { swift, ruby, ror },
                Questions = new List<Question> { q1, q2, q3, q4, q5, q6, q7 }
            };

            JobOffer swiftJobOffer = new JobOffer
            {
                Name = "Swift Developer",
                Description = "Develop further.",
                Employer = apple,
                Office = appleOffice,
                Skills = new List<Skill> { swift },
                Questions = new List<Question> { q1, q7 }
            };

            context.JobOffers.Add(swEngineerJobOffer);
            context.JobOffers.Add(specialistJobOffer);
            context.JobOffers.Add(scientistJobOffer);
            context.JobOffers.Add(frontEndJobOffer);
            context.JobOffers.Add(programmerJobOffer);
            context.JobOffers.Add(geekJobOffer);
            context.JobOffers.Add(designerJobOffer);
            context.JobOffers.Add(swiftJobOffer);

            #endregion

            #region Job Applications

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

			#endregion

	        #region Job Candidates -- Registered Users

			RegisteredUser David = new RegisteredUser
	        {
		        Name = "David",
		        Email = "david@david.sk",
		        Education = Education.BachelorDegree,
		        Skills = new List<Skill> { unix },
		        JobApplications = new List<JobApplication> { jobApplication1 },
		        Username = "David",
		        Password = "DuM4dT4M6ozQXl1TB9BG1frBKz+C2vUDW5quhtta3IJ16/vwXsgwBaSouj4=iDzHfQLao/mnlST/" // zahashovane "david"
	        };

	        RegisteredUser Lubos = new RegisteredUser
	        {
		        Name = "Lubos",
		        Email = "lubos@lubos.sk",
		        Education = Education.DoctoralDegree,
		        Skills = new List<Skill> { html },
		        JobApplications = new List<JobApplication>(),
		        Username = "Lubos",
		        Password = "l0Jy9gqcm18meRjv64WDXuyXhFhyu9Du7GKONBnJsl+aF548sq5GJoqcJOk=rMjTy9mRjGbH3F5Q" // zahashovane "lubos"
	        };

	        context.RegisteredUsers.Add(David);
	        context.RegisteredUsers.Add(Lubos);

	        #endregion

			base.Seed(context);
        }
    }
}