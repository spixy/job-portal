using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLayer.DTOs;
using BusinessLayer.Facades;
using BusinessLayer.Facades.JobOffer;

namespace WebApi.Controllers
{
    public class JobOfferController : ApiController
    {
        public JobOfferFacade JobOfferFacade { get; set; }

        // GET api/joboffer
        public async Task<IEnumerable<JobOfferDto>> Get()
        {
            return (await JobOfferFacade.GetAllOffers()).Items;
        }

        // GET api/joboffer/1
        public async Task<JobOfferDto> Get(int id)
        {
            return (await JobOfferFacade.Get(id));
        }
    }
}
