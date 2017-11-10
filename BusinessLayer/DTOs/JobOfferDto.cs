﻿using DataAccessLayer.Entities;
using System.Collections.Generic;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    class JobOfferDto : DtoBase
    {
        public string Name { get; set; }

        public int EmployerId { get; set; }

        public int OfficeId { get; set; }

        public string Description { get; set; }

        public virtual List<Question> Questions { get; set; }

        public virtual List<JobApplication> JobApplications { get; set; }

        public virtual List<Skill> Skills { get; set; }
    }
}
