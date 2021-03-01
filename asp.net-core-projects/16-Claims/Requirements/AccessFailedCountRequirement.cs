using Microsoft.AspNetCore.Authorization;

namespace Claims_App.Requirements
{
    public class AccessFailedCountRequirement:IAuthorizationRequirement
    {        
        public readonly int accessFailedCount;
        public AccessFailedCountRequirement(int accessFailedCount)
        {
            this.accessFailedCount = accessFailedCount;
        }
    }
}
