using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Employer : UserBase
    {
        public List<Office> Locations;

        public Country Country { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }
    }
}
