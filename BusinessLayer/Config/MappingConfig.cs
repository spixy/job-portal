using AutoMapper;
using BusinessLayer.DTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Answer, AnswerDto>().ReverseMap();
            config.CreateMap<Employer, EmployerDto>().ReverseMap();
            config.CreateMap<JobApplication, JobApplicationDto>().ReverseMap(); // pozri TODO v JobApplicationDto
            config.CreateMap<JobOffer, JobOfferDto>().ReverseMap();
            config.CreateMap<Office, OfficeDto>().ReverseMap();
            config.CreateMap<Question, QuestionDto>().ReverseMap();
            config.CreateMap<RegisteredUser, RegisteredUserDto>().ReverseMap();
            config.CreateMap<Skill, SkillDto>().ReverseMap();
            config.CreateMap<UserBase, UserBaseDto>().ReverseMap();
        }
    }
}
