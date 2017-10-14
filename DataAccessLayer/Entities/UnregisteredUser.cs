
namespace DataAccessLayer.Entities
{
    public class UnregisteredUser : JobCandidate
    {
        public JobApplication JobApplication { get; set; }
    }
}
