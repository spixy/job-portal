using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Facades.JobOffer;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class JobOffersController : ApiController
    {
        public JobOfferFacade JobOfferFacade { get; set; }
        public IMapper Mapper { get; set; }

		/// <summary>
		/// Retrieve all job offers or filtered job offers by skill parameter.
		/// 
		/// Example:
		/// GET api/joboffers
		/// GET api/joboffers?skill=html
		/// </summary>
		/// <param name="skill">Skill name</param>
		/// <returns>Collection of all job offers or the filtered ones by skill</returns>
		/// TODO: nemali by sme zoznam ponuk limitovat filtrom? GetAllOffers je v praxi asi celkom zataz na server a siet, ak tam bude milion jobov :D
		public async Task<IEnumerable<JobOfferDto>> Get(string skill = null)
		{
			IEnumerable<BusinessLayer.DTOs.JobOfferDto> jobs;
			if (skill == null)
            {
				jobs = (await JobOfferFacade.GetAllOffers()).Items;
            }
			else
			{
				jobs = await JobOfferFacade.GetBySkillName(skill);
			}

			var result = new List<JobOfferDto>();
			foreach (BusinessLayer.DTOs.JobOfferDto jobOffer in jobs)
			{
				//var item = Mapper.Map<WebApi.Models.JobOfferDto>(jobDto);
				var item = new JobOfferDto
				{
					Name = jobOffer.Name,
					Description = jobOffer.Description,
					Questions = jobOffer.Questions.Select(x => x.Text),
					JobApplicationIds = jobOffer.JobApplications.Select(x => x.Id),
					Skills = jobOffer.Skills.Select(x => x.Name)
				};
				result.Add(item);
			}

			return result;
		}

        /// <summary>
        /// Retrieve job offer of given id.
        /// 
        /// Syntax:
        /// GET api/joboffer/{id}
        /// </summary>
        /// <param name="id">Job offer id</param>
        /// <returns>Job offer of given id</returns>
        public async Task<JobOfferDto> Get(int id)
        {
            var jobOffer = await JobOfferFacade.Get(id, false);
            if (jobOffer == null)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("The resource is not found.")
                };
                throw new HttpResponseException(message);
            }

			var result = new JobOfferDto
	        {
		        Name = jobOffer.Name,
		        Description = jobOffer.Description,
		        Questions = jobOffer.Questions.Select(x => x.Text),
				JobApplicationIds = jobOffer.JobApplications.Select(x => x.Id),
				Skills = jobOffer.Skills.Select(x => x.Name)
			};
			return result;
        }
    }
}
