using Microsoft.AspNetCore.Identity;

namespace Claims_App.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string Country { get; set; }
    }
}
