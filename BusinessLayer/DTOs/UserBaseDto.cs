using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class UserBaseDto : DtoBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        protected bool Equals(UserBaseDto other)
        {
            return string.Equals(this.Name, other.Name) && string.Equals(this.Email, other.Email) && string.Equals(this.Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserBaseDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Phone != null ? this.Phone.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
