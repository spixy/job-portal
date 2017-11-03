using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;
using Infrastructure;

namespace DataAccessLayer.Entities
{
    public class Answer : IEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.Answers);

        [Required]
        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public virtual Question Question { get; set; }

        [Required]
        public int JobApplicationId { get; set; }

        [Required]
        public virtual JobApplication JobApplication { get; set; }
    }
}
