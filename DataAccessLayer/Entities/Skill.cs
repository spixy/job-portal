using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;
using Infrastructure;

namespace DataAccessLayer.Entities
{
	// ja by som Skill dal ako string
	public class Skill : IEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.Skills);

        [Required]
        public string Name { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }

        public virtual List<JobCandidate> JobCandidates { get; set; }

        protected bool Equals(Skill other)
        {
            return this.Id == other.Id && string.Equals(this.Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Skill) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Id * 397) ^ this.Name.GetHashCode();
            }
        }

        public override string ToString() => Name;
    }
}
