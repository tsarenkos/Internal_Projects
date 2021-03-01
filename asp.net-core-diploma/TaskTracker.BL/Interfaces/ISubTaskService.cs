using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    // работа со списком сущностей с проверкой принадлежности пользователю
    public interface ISubTaskService
    {
        IEnumerable<TaskModelBL> GetNonSubTask(int TaskId);
    }
}
