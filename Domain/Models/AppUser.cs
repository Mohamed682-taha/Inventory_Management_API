using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
