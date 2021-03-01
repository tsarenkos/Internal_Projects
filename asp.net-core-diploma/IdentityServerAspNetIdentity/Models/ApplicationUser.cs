using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerAspNetIdentity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(250)]
        public string FamilyName { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Patronymic { get; set; }

        [MaxLength(256)]
        public string Photo { get; set; }

        [MaxLength(50)]
        public string PhotoContentType { get; set; }


    }
}
