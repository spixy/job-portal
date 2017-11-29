using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.JobOffer;

namespace WebApi.Controllers
{
    public class JobOffersController : ApiController
    {
        public JobOfferFacade JobOfferFacade { get; set; }

        /// <summary>
        /// Retrieve all job offers or filtered job offers by skill parameter.
        /// 
        /// Example:
        /// GET api/joboffers
        /// GET api/joboffers?skill=html
        /// </summary>
        /// <param name="skill">Skill name</param>
        /// <returns>Collection of all job offers or the filtered ones by skill</returns>
        public async Task<IEnumerable<JobOfferDto>> Get(string skill = null)
        {
            if (skill == null)
            {
                return (await JobOfferFacade.GetAllOffers()).Items;
            }

            return await JobOfferFacade.GetBySkillName(skill);
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
            var jobOffer = await JobOfferFacade.Get(id);

            if (jobOffer == null)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("The resource is not found.")
                };
                throw new HttpResponseException(message);
            }

            return jobOffer;
        }
    }
}
