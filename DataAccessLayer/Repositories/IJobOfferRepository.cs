﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Infrastructure.Repository;

namespace DataAccessLayer.Repositories
{
    public interface IJobOfferRepository : IRepository<JobOffer>
    {
        Task<List<JobOffer>> GetBySkill(Skill skill);
    }
}
