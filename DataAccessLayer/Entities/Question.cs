using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
