using DataAccessLayer.Entities;
using System.Collections.Generic;

namespace BusinessLayer.DTOs
{
    public class RegisteredUserDto : JobCandidateDto
    {
        public List<JobApplication> JobApplications { get; set; }

        protected bool Equals(RegisteredUserDto other)
        {
            return base.Equals(other) && this.JobApplications.Equals(other.JobApplications);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RegisteredUserDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ this.JobApplications.GetHashCode();
            }
        }
    }
}
