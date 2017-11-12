using System.Collections.Generic;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;

namespace BusinessLayer.DTOs.Filters
{
    public class RegisteredUserFilterDto : UserBaseFilterDto
    {
        public Education Education { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
