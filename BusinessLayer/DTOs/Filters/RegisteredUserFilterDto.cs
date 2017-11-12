using System.Collections.Generic;
using DataAccessLayer.Enums;

namespace BusinessLayer.DTOs.Filters
{
    public class RegisteredUserFilterDto : UserBaseFilterDto
    {
        public Education Education { get; set; }

        public List<SkillDto> Skills { get; set; }
    }
}
