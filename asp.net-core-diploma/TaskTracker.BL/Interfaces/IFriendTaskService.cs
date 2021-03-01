using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    // работа со списком сущностей с проверкой принадлежности пользователю
    public interface IFriendTaskService
    {
        Task<IEnumerable<TaskModelBL>> GetAll(string FriendId);
        TaskModelBL GetById(int id);
        bool Update(int id, TaskModelBL model);
    }
}
