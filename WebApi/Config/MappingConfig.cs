using AutoMapper;
using BusinessLayer.DTOs;

namespace WebApi.Config
{
	public class MappingConfig
	{
		public static void ConfigureMapping(IMapperConfigurationExpression config)
		{
			config.CreateMap<QuestionDto, string>()
				.ForMember(dest => dest, opt => opt.MapFrom(src => src.Text));

			config.CreateMap<SkillDto, string>()
				.ForMember(dest => dest, opt => opt.MapFrom(src => src.Name));

			config.CreateMap<JobApplicationDto, int>()
				.ForMember(dest => dest, opt => opt.MapFrom(src => src.Id));

			config.CreateMap<BusinessLayer.DTOs.JobOfferDto, WebApi.Models.JobOfferDto>()
				.ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
				.ForMember(dest => dest.JobApplicationIds, opt => opt.MapFrom(src => src.JobApplications))
				.ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills));
		}
	}
}