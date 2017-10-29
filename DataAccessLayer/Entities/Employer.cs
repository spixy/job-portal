using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Employer : UserBase
    {
        public virtual List<JobOffer> JobOffers { get; set; }
    }
}
