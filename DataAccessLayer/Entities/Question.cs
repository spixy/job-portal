using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;
using Infrastructure;

namespace DataAccessLayer.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.Questions);

        [Required]
        public string Text { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }
    }
}
