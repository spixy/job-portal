using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Enums;

namespace BusinessLayer.DTOs
{
    public class JobCandidateDto : UserBaseDto
    {
        [Required]
        public Education Education { get; set; }

	    protected bool Equals(JobCandidateDto other)
        {
            return base.Equals(other) && this.Education == other.Education;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JobCandidateDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) this.Education;
                return hashCode;
            }
        }
    }
}
