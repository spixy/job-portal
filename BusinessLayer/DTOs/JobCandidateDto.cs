using System.Collections.Generic;
using BusinessLayer.DTOs.Enums;

namespace BusinessLayer.DTOs
{
    public class JobCandidateDto : UserBaseDto
    {
        public Education Education { get; set; }

        public List<SkillDto> Skills { get; set; }

	    protected bool Equals(JobCandidateDto other)
        {
            return base.Equals(other) && this.Education == other.Education && this.Skills.Equals(other.Skills);
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
                hashCode = (hashCode * 397) ^ this.Skills.GetHashCode();
                return hashCode;
            }
        }
    }
}
