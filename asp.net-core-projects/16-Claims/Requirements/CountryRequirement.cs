using Microsoft.AspNetCore.Authorization;

namespace Claims_App.Requirements
{
    public class CountryRequirement:IAuthorizationRequirement
    {
        public readonly string country;
        public CountryRequirement(string country)
        {
            this.country = country;
        }
    }
}
