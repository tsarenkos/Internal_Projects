using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    public interface IFileService
    {
        IEnumerable<FileModelBL> GetAll(int TaskId);
        FileModelBL GetByFileName(int taskid, string filename);
        bool Create(int taskId, FileModelBL model);
        bool Delete(int id);
        bool DeleteForTask(int taskId);
    }
}
