
using Microsoft.AspNetCore.Identity;

namespace Task.Web.Entites
{
    public class ApplicationUser: IdentityUser
    {
      
        public decimal Balance { get; set; }
    }
}
