using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Answer : IEntity
    {
        public int Id { get; set; }
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
