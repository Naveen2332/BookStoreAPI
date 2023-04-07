using Microsoft.AspNetCore.Identity;

namespace BookStore.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
