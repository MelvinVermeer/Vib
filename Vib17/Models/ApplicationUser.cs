using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Vib17
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        public List<UserSession> Sessions { get; set; }

    }
}
