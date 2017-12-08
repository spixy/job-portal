using System;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.JobOffer;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.JobOffers;
using BusinessLayer.Services.Questions;
using BusinessLayer.Services.Skills;
using DataAccessLayer.Repositories;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWorkProvider unitOfWorkProvider = new EfUnitOfWorkProvider(() => new JobPortalDbContext());
            IMapper mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            TestJobOffers(mapper, unitOfWorkProvider).Wait();

            System.Console.ReadLine();

        }

        private static async Task TestJobOffers(IMapper mapper, IUnitOfWorkProvider provider)
        {
            ISkillRepository skillRepository = new SkillRepository(provider);
            EfQuery<Skill> skillEfQuery = new EfQuery<Skill>(provider);
            SkillQueryObject skillQueryObject = new SkillQueryObject(mapper, skillEfQuery);
            ISkillService skillService = new SkillService(mapper,
                                                          skillRepository,
                                                          skillQueryObject);

            EfRepository<Question> questionRepository = new EfRepository<Question>(provider);
            EfQuery<Question> questionEfQuery = new EfQuery<Question>(provider);
            QuestionQueryObject questionQueryObject = new QuestionQueryObject(mapper, questionEfQuery);
            IQuestionService questionService = new QuestionService(mapper,
                                                                   questionRepository,
                                                                   questionQueryObject);

            IJobOfferRepository jobOfferRepository = new JobOfferRepository(provider);
            EfQuery<JobOffer> jobOfferQuery = new EfQuery<JobOffer>(provider);
            JobOfferQueryObject jobOfferQueryObject = new JobOfferQueryObject(mapper, jobOfferQuery);
            IJobOfferService jobOfferService = new JobOfferService(mapper,
                                                                   jobOfferRepository,
                                                                   skillRepository,
                                                                   questionRepository,
                                                                   jobOfferQueryObject);

            IJobOfferFacade jobOfferFacade = new JobOfferFacade(provider,
                                                                jobOfferService,
                                                                questionService,
                                                                skillService);

            jobOfferFacade.Create(new JobOfferCreateDto
            {
                Name = "New job offer by Google Inc.",
                Description = "New description for this job",
                EmployerId = 1,
                OfficeId = 1,
                Questions = new List<QuestionDto>
                {
                    new QuestionDto() {Text = "q1"},
                    new QuestionDto() {Text = "q2"},
                    new QuestionDto() {Text = "q3"}
                },
                Skills = new List<SkillDto>
                {
                    new SkillDto() {Name = "Java"},
                    new SkillDto() {Name = "Php"}
                }
            });

            var results = await jobOfferFacade.GetByEmployer(1);
            foreach (var item in results)
            {
                Console.WriteLine("ID: " + item.Id);
                Console.WriteLine("Name: " + item.Name);
            }

        }
    }
}
