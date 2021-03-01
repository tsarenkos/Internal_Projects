using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    // работа со списком сущностей с проверкой принадлежности пользователю
    public interface ITaskService
    {
        IEnumerable<TaskModelBL> GetAll();
        TaskModelBL GetById(int id);
        bool Create(TaskModelBL model);
        Task<bool> Update(int id, TaskModelBL model);
        bool Delete(int id);
    }
}
