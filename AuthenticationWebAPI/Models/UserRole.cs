using System.ComponentModel.DataAnnotations;

namespace AuthenticationWebAPI.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int LoginId { get; set; }
        public Login login { get; set; }
        public string Role { get; set; }
    }
}
