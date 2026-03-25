using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class AppUser : IdentityUser
    {
        public UserRole Role { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
