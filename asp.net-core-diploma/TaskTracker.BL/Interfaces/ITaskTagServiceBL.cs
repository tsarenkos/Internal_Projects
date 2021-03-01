using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    public interface ITaskTagServiceBL : IGenericListBL<TaskTagModelBL>
    {
        TaskTagModelBL GetTagById(int id);
        IEnumerable<TaskTagModelBL> GetTagByTaskId(int id);
        void Add(int tagId, string tagName);
        void Edit(int tagId, string tagName);
        void Delete(int id);
    }
}
