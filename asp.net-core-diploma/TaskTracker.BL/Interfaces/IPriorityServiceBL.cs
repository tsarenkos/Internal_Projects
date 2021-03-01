using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    public interface IPriorityServiceBL
    {
        IEnumerable<PriorityModelBL> GetAll();
        PriorityModelBL GetById(int id);
    }
}
