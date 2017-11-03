using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Entities
{
    public class Employer : UserBase
    {
        [NotMapped]
        public override string TableName { get; } = nameof(JobPortalDbContext.Employers);

        public virtual List<JobOffer> JobOffers { get; set; }
    }
}
