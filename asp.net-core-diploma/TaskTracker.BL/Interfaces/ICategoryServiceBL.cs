using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    public interface ICategoryServiceBL : IGenericListBL<TaskCategoryModelBL>
    {
        TaskCategoryModelBL GetById(int id);
        void Add(string categoryName);
        void Edit(int categoryId, string categoryName);
        void UpdateCategoryForTask(int taskId, string UserId, bool IsAdmin, TaskCategoryModelBL taskCategory);
        void Delete(int categoryId);
    }
}
