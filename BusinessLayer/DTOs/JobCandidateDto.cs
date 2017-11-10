using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System.Collections.Generic;

namespace BusinessLayer.DTOs
{
    public class JobCandidateDto : UserBaseDto
    {
        public Education Education { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
