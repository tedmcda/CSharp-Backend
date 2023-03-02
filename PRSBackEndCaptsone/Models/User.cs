using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRSBackEndCaptsone.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Username { get; set; }

        [StringLength(30)]
        public string Password { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(12)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        
        public bool IsReviewer { get; set; }

        
        public bool IsAdmin { get; set; }
    }
}
