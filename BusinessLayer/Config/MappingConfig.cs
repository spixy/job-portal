﻿using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using DataAccessLayer.Entities;
using Infrastructure.Query;

namespace BusinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Answer, AnswerDto>();
	        config.CreateMap<AnswerDto, Answer>()
		        .ForMember(dest => dest.Question, opt => opt.Ignore());

	        config.CreateMap<JobApplication, JobApplicationDto>().ReverseMap();
			config.CreateMap<JobOffer, JobOfferDto>().ReverseMap();
            config.CreateMap<JobOffer, JobOfferCreateDto>().ReverseMap();
            config.CreateMap<Office, OfficeDto>().ReverseMap();
            config.CreateMap<Question, QuestionDto>().ReverseMap();
            config.CreateMap<RegisteredUser, RegisteredUserDto>().ReverseMap();
            config.CreateMap<Skill, SkillDto>().ReverseMap();
            config.CreateMap<UserBase, UserBaseDto>().ReverseMap();
            config.CreateMap<JobCandidate, JobCandidateDto>().ReverseMap();
	        config.CreateMap<Employer, EmployerDto>().ReverseMap();

			config.CreateMap<RegisteredUserCreateDto, RegisteredUserDto>();
	        config.CreateMap<EmployerCreateDto, EmployerDto>();
	        config.CreateMap<JobApplicationCreateDto, JobApplicationDto>();

	        config.CreateMap<JobApplicationDto, JobApplicationUpdateDto>();
	        config.CreateMap<JobApplicationUpdateDto, JobApplication>()
		        .ForMember(dest => dest.JobOffer, opt => opt.Ignore())
		        .ForMember(dest => dest.Answers, opt => opt.Ignore())
		        .ForMember(dest => dest.JobCandidate, opt => opt.Ignore())
		        .ForMember(dest => dest.JobCandidateId, opt => opt.Ignore())
		        .ForMember(dest => dest.JobOfferId, opt => opt.Ignore());

	        config.CreateMap<JobOfferDto, JobOfferUpdateDto>();
	        config.CreateMap<JobOfferUpdateDto, JobOffer>()
		        .ForMember(dest => dest.Employer, opt => opt.Ignore())
		        .ForMember(dest => dest.JobApplications, opt => opt.Ignore())
		        .ForMember(dest => dest.Office, opt => opt.Ignore())
		        .ForMember(dest => dest.OfficeId, opt => opt.Ignore());

			config.CreateMap<QueryResult<Answer>, QueryResultDto<AnswerDto, AnswerFilterDto>>();
            config.CreateMap<QueryResult<Employer>, QueryResultDto<EmployerDto, EmployerFilterDto>>();
            config.CreateMap<QueryResult<JobApplication>, QueryResultDto<JobApplicationDto, JobApplicationFilterDto>>();
            config.CreateMap<QueryResult<JobOffer>, QueryResultDto<JobOfferDto, JobOfferFilterDto>>();
            config.CreateMap<QueryResult<Office>, QueryResultDto<OfficeDto, OfficeFilterDto>>();
            config.CreateMap<QueryResult<Question>, QueryResultDto<QuestionDto, QuestionFilterDto>>();
            config.CreateMap<QueryResult<RegisteredUser>, QueryResultDto<RegisteredUserDto, RegisteredUserFilterDto>>();
            config.CreateMap<QueryResult<Skill>, QueryResultDto<SkillDto, SkillFilterDto>>();
            config.CreateMap<QueryResult<UserBase>, QueryResultDto<UserBaseDto, UserBaseFilterDto>>();
            config.CreateMap<QueryResult<JobCandidate>, QueryResultDto<JobCandidateDto, JobCandidateFilterDto>>();
		}
    }
}
