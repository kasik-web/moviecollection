using Microsoft.AspNetCore.Identity;

namespace MyMovieCollection.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        
    }
}
