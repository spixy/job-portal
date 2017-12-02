using DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Entities
{
    public class JobCandidate : UserBase
    {
        [NotMapped]
        public override string TableName { get; } = nameof(JobPortalDbContext.JobCandidates);

		[Required]
        public Education Education { get; set; }

        public virtual List<Skill> Skills { get; set; }
    }
}
