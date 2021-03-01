using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    public interface ITaskFriendGrants
    {
        IEnumerable<TaskEditGrantsBL> GetAll();
        Task<APIResult> Create(TaskEditGrantsBL taskEditGrantRequest);
        Task<APIResult> Grant(TaskEditGrantsBL taskEditGrantRequest);
        Task<APIResult> Deny(TaskEditGrantsBL taskEditGrantRequest);
    }
}
